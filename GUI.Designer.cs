namespace SBRT_Lung_BED_Calculator
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Planlist = new System.Windows.Forms.ListBox();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.BEDdisplay = new System.Windows.Forms.TextBox();
            this.Volumedisplay = new System.Windows.Forms.TextBox();
            this.BEDMeandoselabel = new System.Windows.Forms.Label();
            this.Volumelabel = new System.Windows.Forms.Label();
            this.BED2Label = new System.Windows.Forms.Label();
            this.BED2Display = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Planlist
            // 
            this.Planlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Planlist.FormattingEnabled = true;
            this.Planlist.ItemHeight = 20;
            this.Planlist.Location = new System.Drawing.Point(12, 12);
            this.Planlist.Name = "Planlist";
            this.Planlist.Size = new System.Drawing.Size(304, 124);
            this.Planlist.TabIndex = 0;
            this.Planlist.SelectedIndexChanged += new System.EventHandler(this.PlanList_SelectedIndexChanged);
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteButton.Location = new System.Drawing.Point(353, 12);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(119, 53);
            this.ExecuteButton.TabIndex = 1;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // BEDdisplay
            // 
            this.BEDdisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BEDdisplay.Location = new System.Drawing.Point(336, 110);
            this.BEDdisplay.Name = "BEDdisplay";
            this.BEDdisplay.Size = new System.Drawing.Size(100, 26);
            this.BEDdisplay.TabIndex = 2;
            // 
            // Volumedisplay
            // 
            this.Volumedisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Volumedisplay.Location = new System.Drawing.Point(336, 179);
            this.Volumedisplay.Name = "Volumedisplay";
            this.Volumedisplay.Size = new System.Drawing.Size(101, 26);
            this.Volumedisplay.TabIndex = 3;
            // 
            // BEDMeandoselabel
            // 
            this.BEDMeandoselabel.AutoSize = true;
            this.BEDMeandoselabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BEDMeandoselabel.Location = new System.Drawing.Point(332, 87);
            this.BEDMeandoselabel.Name = "BEDMeandoselabel";
            this.BEDMeandoselabel.Size = new System.Drawing.Size(211, 20);
            this.BEDMeandoselabel.TabIndex = 4;
            this.BEDMeandoselabel.Text = "Lungs BED Mean Dose (Gy)";
            // 
            // Volumelabel
            // 
            this.Volumelabel.AutoSize = true;
            this.Volumelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Volumelabel.Location = new System.Drawing.Point(332, 156);
            this.Volumelabel.Name = "Volumelabel";
            this.Volumelabel.Size = new System.Drawing.Size(268, 20);
            this.Volumelabel.TabIndex = 5;
            this.Volumelabel.Text = "Lungs-GTV Volume at BED = 70 (%)";
            // 
            // BED2Label
            // 
            this.BED2Label.AutoSize = true;
            this.BED2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BED2Label.Location = new System.Drawing.Point(12, 156);
            this.BED2Label.Name = "BED2Label";
            this.BED2Label.Size = new System.Drawing.Size(249, 20);
            this.BED2Label.TabIndex = 6;
            this.BED2Label.Text = "Lungs-GTV BED Mean Dose (Gy)";
            // 
            // BED2Display
            // 
            this.BED2Display.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BED2Display.Location = new System.Drawing.Point(16, 179);
            this.BED2Display.Name = "BED2Display";
            this.BED2Display.Size = new System.Drawing.Size(113, 26);
            this.BED2Display.TabIndex = 7;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 233);
            this.Controls.Add(this.BED2Display);
            this.Controls.Add(this.BED2Label);
            this.Controls.Add(this.Volumelabel);
            this.Controls.Add(this.BEDMeandoselabel);
            this.Controls.Add(this.Volumedisplay);
            this.Controls.Add(this.BEDdisplay);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.Planlist);
            this.Name = "GUI";
            this.Text = "SBRT Lung BED Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Planlist;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.TextBox BEDdisplay;
        private System.Windows.Forms.TextBox Volumedisplay;
        private System.Windows.Forms.Label BEDMeandoselabel;
        private System.Windows.Forms.Label Volumelabel;
        private System.Windows.Forms.Label BED2Label;
        private System.Windows.Forms.TextBox BED2Display;
    }
}