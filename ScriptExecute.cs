using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;



/*
    SBRT Lung BED Calculator - ESAPI 16.0 version (7/6/2021)
    This is the start-up file which launches the GUI, where the rest of the program takes place. A more detailed description can be found their.
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


namespace VMS.TPS
{
    public class Script
    {
        public Script() { }

        public void Execute(ScriptContext context)
        {
            //Variable declaration space

            IEnumerable<PlanSetup> Plans = context.PlansInScope;

            if (context.Patient == null)
            {
                System.Windows.Forms.MessageBox.Show("Please load a patient with a treatment plan before running this script!");
                return;
            }

            System.Windows.Forms.Application.EnableVisualStyles();

            System.Windows.Forms.Application.Run(new SBRT_Lung_BED_Calculator.GUI(Plans));
        }

    }

}
