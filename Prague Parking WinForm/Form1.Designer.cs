namespace Prague_Parking_WinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.add_vehicle_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // add_vehicle_button
            // 
            this.add_vehicle_button.Location = new System.Drawing.Point(12, 641);
            this.add_vehicle_button.Name = "add_vehicle_button";
            this.add_vehicle_button.Size = new System.Drawing.Size(103, 23);
            this.add_vehicle_button.TabIndex = 0;
            this.add_vehicle_button.Text = "Add Vehicle";
            this.add_vehicle_button.UseVisualStyleBackColor = true;
            this.add_vehicle_button.Click += new System.EventHandler(this.add_vehicle_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 692);
            this.Controls.Add(this.add_vehicle_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button add_vehicle_button;
       
    }
}