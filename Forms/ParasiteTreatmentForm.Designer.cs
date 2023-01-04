namespace PIS_PetRegistry.Forms
{
    partial class ParasiteTreatmentForm
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
            this.saveButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.parasiteTreatmentDatePicker = new System.Windows.Forms.DateTimePicker();
            this.medicationComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 134);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(200, 23);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Дата проведения";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Препарат";
            // 
            // parasiteTreatmentDatePicker
            // 
            this.parasiteTreatmentDatePicker.CustomFormat = "dd-MM-yyyy";
            this.parasiteTreatmentDatePicker.Location = new System.Drawing.Point(12, 84);
            this.parasiteTreatmentDatePicker.Name = "parasiteTreatmentDatePicker";
            this.parasiteTreatmentDatePicker.Size = new System.Drawing.Size(200, 23);
            this.parasiteTreatmentDatePicker.TabIndex = 6;
            // 
            // medicationComboBox
            // 
            this.medicationComboBox.FormattingEnabled = true;
            this.medicationComboBox.Location = new System.Drawing.Point(12, 31);
            this.medicationComboBox.Name = "medicationComboBox";
            this.medicationComboBox.Size = new System.Drawing.Size(200, 23);
            this.medicationComboBox.TabIndex = 5;
            // 
            // ParasiteTreatmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 165);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.parasiteTreatmentDatePicker);
            this.Controls.Add(this.medicationComboBox);
            this.Name = "ParasiteTreatmentForm";
            this.Text = "Обработка от паразитов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button saveButton;
        private Label label2;
        private Label label1;
        private DateTimePicker parasiteTreatmentDatePicker;
        private ComboBox medicationComboBox;
    }
}