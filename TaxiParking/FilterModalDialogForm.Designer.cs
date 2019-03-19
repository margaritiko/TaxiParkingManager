namespace TaxiParking
{
    partial class FilterModalDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterModalDialogForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.admAreaTextField = new System.Windows.Forms.TextBox();
            this.carCapacityLabel = new System.Windows.Forms.Label();
            this.maxLabel = new System.Windows.Forms.Label();
            this.admAreaLabel = new System.Windows.Forms.Label();
            this.carCapacityMinTextField = new System.Windows.Forms.TextBox();
            this.carCapacityMaxTextField = new System.Windows.Forms.TextBox();
            this.minLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.Controls.Add(this.admAreaTextField, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.carCapacityLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.maxLabel, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.admAreaLabel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.carCapacityMinTextField, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.carCapacityMaxTextField, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.minLabel, 1, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(33, 60);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.32433F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.67567F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(858, 168);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // admAreaTextField
            // 
            this.tableLayoutPanel.SetColumnSpan(this.admAreaTextField, 2);
            this.admAreaTextField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.admAreaTextField.Location = new System.Drawing.Point(603, 96);
            this.admAreaTextField.Name = "admAreaTextField";
            this.admAreaTextField.Size = new System.Drawing.Size(252, 31);
            this.admAreaTextField.TabIndex = 14;
            // 
            // carCapacityLabel
            // 
            this.carCapacityLabel.AutoSize = true;
            this.carCapacityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.carCapacityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carCapacityLabel.Location = new System.Drawing.Point(3, 0);
            this.carCapacityLabel.Name = "carCapacityLabel";
            this.tableLayoutPanel.SetRowSpan(this.carCapacityLabel, 2);
            this.carCapacityLabel.Size = new System.Drawing.Size(594, 93);
            this.carCapacityLabel.TabIndex = 13;
            this.carCapacityLabel.Text = "Укажите минимальное и максимальное значения для поля CarCapacity";
            this.carCapacityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // maxLabel
            // 
            this.maxLabel.AutoSize = true;
            this.maxLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxLabel.Location = new System.Drawing.Point(731, 0);
            this.maxLabel.Name = "maxLabel";
            this.maxLabel.Size = new System.Drawing.Size(124, 20);
            this.maxLabel.TabIndex = 12;
            this.maxLabel.Text = "макс";
            this.maxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // admAreaLabel
            // 
            this.admAreaLabel.AutoSize = true;
            this.admAreaLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.admAreaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.admAreaLabel.Location = new System.Drawing.Point(3, 93);
            this.admAreaLabel.Name = "admAreaLabel";
            this.admAreaLabel.Size = new System.Drawing.Size(594, 75);
            this.admAreaLabel.TabIndex = 7;
            this.admAreaLabel.Text = "Введите строку, содержащуюся в столбце AdmArea записи";
            this.admAreaLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // carCapacityMinTextField
            // 
            this.carCapacityMinTextField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.carCapacityMinTextField.Location = new System.Drawing.Point(603, 23);
            this.carCapacityMinTextField.Name = "carCapacityMinTextField";
            this.carCapacityMinTextField.Size = new System.Drawing.Size(122, 31);
            this.carCapacityMinTextField.TabIndex = 8;
            // 
            // carCapacityMaxTextField
            // 
            this.carCapacityMaxTextField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.carCapacityMaxTextField.Location = new System.Drawing.Point(731, 23);
            this.carCapacityMaxTextField.Name = "carCapacityMaxTextField";
            this.carCapacityMaxTextField.Size = new System.Drawing.Size(124, 31);
            this.carCapacityMaxTextField.TabIndex = 9;
            // 
            // minLabel
            // 
            this.minLabel.AutoSize = true;
            this.minLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minLabel.Location = new System.Drawing.Point(603, 0);
            this.minLabel.Name = "minLabel";
            this.minLabel.Size = new System.Drawing.Size(122, 20);
            this.minLabel.TabIndex = 10;
            this.minLabel.Text = "мин";
            this.minLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.okButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("okButton.BackgroundImage")));
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.okButton.Location = new System.Drawing.Point(334, 234);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(251, 183);
            this.okButton.TabIndex = 8;
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // FilterModalDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(924, 429);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(950, 500);
            this.MinimumSize = new System.Drawing.Size(950, 500);
            this.Name = "FilterModalDialogForm";
            this.Text = "Фильтр";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox admAreaTextField;
        private System.Windows.Forms.Label carCapacityLabel;
        private System.Windows.Forms.Label maxLabel;
        private System.Windows.Forms.Label admAreaLabel;
        private System.Windows.Forms.TextBox carCapacityMinTextField;
        private System.Windows.Forms.TextBox carCapacityMaxTextField;
        private System.Windows.Forms.Label minLabel;
        private System.Windows.Forms.Button okButton;
    }
}