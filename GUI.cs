using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;



/*
    SBRT Lung BED Calculator - ESAPI 16.0 version (7/6/2021)
    This is the GUI, which is called from the ScriptExecute start-up file. Most of the program takes place here.
    This program does specific BED calculations on Lung structures which I made to help automate calculations for a research project we were doing in the department.
    The User selects the plan they want to run on and that's it; no other input. The BED values are displayed in the GUI.
    The calculations have been manually verified for accuracy.

    This program is expressely written as a plug-in script for use with Varian's Eclipse Treatment Planning System, and requires Varian's API files to run properly.
    This program requires .NET Framework 4.6.1
    Copyright (C) 2021 Zackary Thomas Ricci Morelli
    
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
    I can be contacted at: zackmorelli@gmail.com
*/




namespace SBRT_Lung_BED_Calculator
{
    public partial class GUI : Form
    {
        public string pl = null;
        public string strctid = null;

        //this is the alpha/beta radiation sensitivity ratio for the lungs
        public double ab = 3.0;

        public GUI(IEnumerable<PlanSetup> Plans)
        {
            InitializeComponent();

            foreach (PlanSetup aplan in Plans)
            {
                Planlist.Items.Add(aplan.Id);
               // plannames.Add(aplan.Id);
                // MessageBox.Show("Trig 8");
            }

            //The startup file passes us the plan list, which is used to populate the drop down. Once the user has chosen a plan and clicked the execute button, the program starts and we move to the Execute method in my normal style.

            ExecuteButton.Click += (sender, EventArgs) => { buttonNext_Click(sender, EventArgs, Plans); };
        }


        private void EXECUTE(IEnumerable<PlanSetup> Plans)
        {
            bool lunglock = false;
            bool lungGTVlock = false;
            double fracs = 0.0;
            double LungsGTVVol = 0.0;
            double VBED70 = 0.0;
            double BEDVdiff = 0.0;
            double BEDVdiffsum = 0.0;
            double BEDGTVVdiffsum = 0.0;
            double lungsmeanBED = 0.0;
            double lungsGTVmeanBED = 0.0;
            double VBED70per = 0.0;

            //First some protections
            if (pl == null)
            {
                MessageBox.Show("You must select a plan for the program to run on before starting! The list of plans in the course currently loaded into Eclipse is in the upper left.");
                return;
            }

            List<PlanSetup> templistPlan = Plans.Where(a => a.Id.Equals(pl)).ToList();
            PlanSetup Plan = templistPlan.First();

            try
            {
                strctid = Plan.StructureSet.Id;
            }
            catch (NullReferenceException e)
            {
                System.Windows.Forms.MessageBox.Show("The plan " + Plan.Id + " does not have a structure set! Ending program.");
                // no structure set, skip
                return;
            }

            //the code below finds the Lungs and Lungs-GTV structures we want to perform BED calculations on.
            IEnumerator ZK = Plan.StructureSet.Structures.GetEnumerator();
            ZK.MoveNext();
            Structure Lungs = (Structure)ZK.Current;
            Structure LungsGTV = (Structure)ZK.Current;

            foreach (Structure S in Plan.StructureSet.Structures)
            {
                if (S.Id == "Lungs")
                {
                    //  MessageBox.Show("S.Id is: " + S.Id.ToString());
                    Lungs = S;
                    lunglock = true;
                    // MessageBox.Show("Trig EXE - f3.4");
                }

                if (S.Id == "Lungs-GTV")
                {
                    //  MessageBox.Show("S.Id is: " + S.Id.ToString());
                    LungsGTV = S;
                    lungGTVlock = true;
                    // MessageBox.Show("Trig EXE - f3.4");
                }
            }

            //We at least need the Lungs-GTV structure for this program to be able to do anything. If the Lungs are present as well we'll do some more stuff.
            if (lungGTVlock == false)
            {
                MessageBox.Show("Warning: No Lungs-GTV structure was found in this plan! Unable to perform calculation.");
                return;
            }

            if (lunglock == false)
            {
                MessageBox.Show("Warning: No Lungs structure was found in this plan! The program will only report on the Lungs-GTV structure.");
                BEDdisplay.Text = "NA";
            }

            try
            {
                //So first we calculate the mean of the Lungs-GTV BED by looping through all the points in the DVH curve and summing the BED difference between each point.
                //The BED is calculated at each point (which is dependent on the number of fractions, which we get from ESAPI).
                //The BED difference between each point is then calculated by finding the volume difference between each point, and then mulitplying by the BED at that point.
                // for Lungs-GTV, we also want to know the percentage of the total volume that BED = 70 occurs at
                fracs = Convert.ToDouble(Plan.NumberOfFractions);
                double BED = 0.0;
               
                DVHData lungGTVDVH = Plan.GetDVHCumulativeData(LungsGTV, DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.1);

                for (int i = 0; i <= lungGTVDVH.CurveData.Count() - 1; i++)
                {
                    BED = (lungGTVDVH.CurveData[i].DoseValue.Dose / 100.0) * (1 + (lungGTVDVH.CurveData[i].DoseValue.Dose / 100.0 / fracs / ab));

                    if (BED > 69.98 && BED < 70.02)
                    {
                        VBED70 = lungGTVDVH.CurveData[i].Volume;
                        VBED70per = (VBED70 / LungsGTV.Volume) * 100.0;
                    }

                    if (i > 1)
                    {
                        BEDVdiff = (lungGTVDVH.CurveData[i].Volume - lungGTVDVH.CurveData[i - 1].Volume) * BED;
                    }

                    BEDGTVVdiffsum = BEDGTVVdiffsum + BEDVdiff;
                }

                //If the Lungs exist, we calculate the mean BED by doing the same thing as the Lungs-GTV. With the Lungs, we aren't interested with the BED = 70.
                if (lunglock == true)
                {
                    DVHData lungDVH = Plan.GetDVHCumulativeData(Lungs, DoseValuePresentation.Absolute, VolumePresentation.AbsoluteCm3, 0.1);
                    for (int i = 0; i <= lungDVH.CurveData.Count() - 1; i++)
                    {

                        BED = (lungDVH.CurveData[i].DoseValue.Dose / 100.0) * (1 + (lungDVH.CurveData[i].DoseValue.Dose / 100.0 / fracs / ab));

                        if (i > 1)
                        {
                            BEDVdiff = (lungDVH.CurveData[i].Volume - lungDVH.CurveData[i - 1].Volume) * BED;
                        }

                        BEDVdiffsum = BEDVdiffsum + BEDVdiff;
                    }

                    lungsmeanBED = (Math.Abs(BEDVdiffsum)) / Lungs.Volume;
                    lungsmeanBED = Math.Round(lungsmeanBED, 2, MidpointRounding.AwayFromZero);
                    BEDdisplay.Text = lungsmeanBED.ToString();
                }

                lungsGTVmeanBED = (Math.Abs(BEDGTVVdiffsum)) / LungsGTV.Volume;
                lungsGTVmeanBED = Math.Round(lungsGTVmeanBED, 2, MidpointRounding.AwayFromZero);

                VBED70per = Math.Round(VBED70per, 2, MidpointRounding.AwayFromZero);

                Volumedisplay.Text = VBED70per.ToString();

                BED2Display.Text = lungsGTVmeanBED.ToString();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace + e.Source);
            }
        }

        void PlanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pl = Planlist.SelectedItem.ToString();
            
            // This is to make sure the program doesn't run on a motion assesment plan if the user selects it by accident
            if (pl == "Motion Assess" || pl == "motion assess" || pl == "Mot Assess" || pl == "mot assess")
            {
                MessageBox.Show("This script is not compatible with Motion Assess plans!");
                return;
            }
            //  MessageBox.Show("Trig 10");

        }

        private void ExecuteButton_Click(object sender, EventArgs args)
        {

            //  MessageBox.Show("Organ: " + org.ToString());
            //  MessageBox.Show("Trig 12 - First Click");
        }

        void buttonNext_Click(object sender, EventArgs e, IEnumerable<PlanSetup> PLN)
        {
            EXECUTE(PLN);
        }



    }
}
