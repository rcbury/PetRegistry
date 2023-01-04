namespace PIS_PetRegistry.Forms
{
    partial class VeterinaryProcedure
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
            this.veterinaryAppointmentDatePicker = new System.Windows.Forms.DateTimePicker();
            this.veterinaryAppointmentNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.veterinaryAppointmentCompletedCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 134);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(200, 23);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Дата проведения";
            // 
            // veterinaryAppointmentDatePicker
            // 
            this.veterinaryAppointmentDatePicker.Location = new System.Drawing.Point(12, 72);
            this.veterinaryAppointmentDatePicker.Name = "veterinaryAppointmentDatePicker";
            this.veterinaryAppointmentDatePicker.Size = new System.Drawing.Size(200, 23);
            this.veterinaryAppointmentDatePicker.TabIndex = 11;
            // 
            // veterinaryAppointmentNameTextBox
            // 
            this.veterinaryAppointmentNameTextBox.Location = new System.Drawing.Point(12, 28);
            this.veterinaryAppointmentNameTextBox.Name = "veterinaryAppointmentNameTextBox";
            this.veterinaryAppointmentNameTextBox.Size = new System.Drawing.Size(200, 23);
            this.veterinaryAppointmentNameTextBox.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Назначение";
            // 
            // veterinaryAppointmentCompletedCheckBox
            // 
            this.veterinaryAppointmentCompletedCheckBox.AutoSize = true;
            this.veterinaryAppointmentCompletedCheckBox.Location = new System.Drawing.Point(12, 101);
            this.veterinaryAppointmentCompletedCheckBox.Name = "veterinaryAppointmentCompletedCheckBox";
            this.veterinaryAppointmentCompletedCheckBox.Size = new System.Drawing.Size(87, 19);
            this.veterinaryAppointmentCompletedCheckBox.TabIndex = 17;
            this.veterinaryAppointmentCompletedCheckBox.Text = "Проведено";
            this.veterinaryAppointmentCompletedCheckBox.UseVisualStyleBackColor = true;
            // 
            // VeterinaryProcedure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 165);
            this.Controls.Add(this.veterinaryAppointmentCompletedCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.veterinaryAppointmentNameTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.veterinaryAppointmentDatePicker);
            this.Name = "VeterinaryProcedure";
            this.Text = "Ветеринарное мероприятие";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button saveButton;
        private Label label2;
        private DateTimePicker veterinaryAppointmentDatePicker;
        private TextBox veterinaryAppointmentNameTextBox;
        private Label label1;
        private CheckBox veterinaryAppointmentCompletedCheckBox;
    }
}