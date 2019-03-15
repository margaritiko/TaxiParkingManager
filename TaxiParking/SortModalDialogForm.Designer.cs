namespace TaxiParking
{
    partial class SortModalDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortModalDialogForm));
            this.groupBoxForSortingButton = new System.Windows.Forms.GroupBox();
            this.numberOfParkingsSortButton = new System.Windows.Forms.RadioButton();
            this.global_idSortButton = new System.Windows.Forms.RadioButton();
            this.carCapacitySortButton = new System.Windows.Forms.RadioButton();
            this.defaultSortButton = new System.Windows.Forms.RadioButton();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBoxForSortingButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxForSortingButton
            // 
            this.groupBoxForSortingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBoxForSortingButton.Controls.Add(this.numberOfParkingsSortButton);
            this.groupBoxForSortingButton.Controls.Add(this.global_idSortButton);
            this.groupBoxForSortingButton.Controls.Add(this.carCapacitySortButton);
            this.groupBoxForSortingButton.Controls.Add(this.defaultSortButton);
            this.groupBoxForSortingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxForSortingButton.Location = new System.Drawing.Point(35, 56);
            this.groupBoxForSortingButton.Name = "groupBoxForSortingButton";
            this.groupBoxForSortingButton.Size = new System.Drawing.Size(904, 208);
            this.groupBoxForSortingButton.TabIndex = 1;
            this.groupBoxForSortingButton.TabStop = false;
            this.groupBoxForSortingButton.Text = "Отсортировать:";
            // 
            // numberOfParkingsSortButton
            // 
            this.numberOfParkingsSortButton.AutoSize = true;
            this.numberOfParkingsSortButton.Location = new System.Drawing.Point(7, 151);
            this.numberOfParkingsSortButton.Name = "numberOfParkingsSortButton";
            this.numberOfParkingsSortButton.Size = new System.Drawing.Size(649, 35);
            this.numberOfParkingsSortButton.TabIndex = 3;
            this.numberOfParkingsSortButton.TabStop = true;
            this.numberOfParkingsSortButton.Text = "по количеству парковок в выбранном округе";
            this.numberOfParkingsSortButton.UseVisualStyleBackColor = true;
            this.numberOfParkingsSortButton.CheckedChanged += new System.EventHandler(this.SortButton_CheckedChanged);
            // 
            // global_idSortButton
            // 
            this.global_idSortButton.AutoSize = true;
            this.global_idSortButton.Location = new System.Drawing.Point(7, 115);
            this.global_idSortButton.Name = "global_idSortButton";
            this.global_idSortButton.Size = new System.Drawing.Size(599, 35);
            this.global_idSortButton.TabIndex = 2;
            this.global_idSortButton.TabStop = true;
            this.global_idSortButton.Text = "по возрастанию значения поля  global_id";
            this.global_idSortButton.UseVisualStyleBackColor = true;
            this.global_idSortButton.CheckedChanged += new System.EventHandler(this.SortButton_CheckedChanged);
            // 
            // carCapacitySortButton
            // 
            this.carCapacitySortButton.AutoSize = true;
            this.carCapacitySortButton.Location = new System.Drawing.Point(7, 80);
            this.carCapacitySortButton.Name = "carCapacitySortButton";
            this.carCapacitySortButton.Size = new System.Drawing.Size(636, 35);
            this.carCapacitySortButton.TabIndex = 1;
            this.carCapacitySortButton.TabStop = true;
            this.carCapacitySortButton.Text = "по возрастанию значения поля CarCapacity";
            this.carCapacitySortButton.UseVisualStyleBackColor = true;
            this.carCapacitySortButton.CheckedChanged += new System.EventHandler(this.SortButton_CheckedChanged);
            // 
            // defaultSortButton
            // 
            this.defaultSortButton.AutoSize = true;
            this.defaultSortButton.Location = new System.Drawing.Point(7, 44);
            this.defaultSortButton.Name = "defaultSortButton";
            this.defaultSortButton.Size = new System.Drawing.Size(252, 35);
            this.defaultSortButton.TabIndex = 0;
            this.defaultSortButton.TabStop = true;
            this.defaultSortButton.Text = "не сортировать";
            this.defaultSortButton.UseVisualStyleBackColor = true;
            this.defaultSortButton.CheckedChanged += new System.EventHandler(this.SortButton_CheckedChanged);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.okButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("okButton.BackgroundImage")));
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.okButton.Location = new System.Drawing.Point(319, 286);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(340, 131);
            this.okButton.TabIndex = 7;
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // SortModalDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(974, 429);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBoxForSortingButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 500);
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "SortModalDialogForm";
            this.Text = "Сортировка";
            this.groupBoxForSortingButton.ResumeLayout(false);
            this.groupBoxForSortingButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxForSortingButton;
        private System.Windows.Forms.RadioButton numberOfParkingsSortButton;
        private System.Windows.Forms.RadioButton global_idSortButton;
        private System.Windows.Forms.RadioButton carCapacitySortButton;
        private System.Windows.Forms.RadioButton defaultSortButton;
        private System.Windows.Forms.Button okButton;
    }
}