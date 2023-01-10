namespace PIS_PetRegistry
{
    partial class LegalPersonForm
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LocalityComboBox = new System.Windows.Forms.ComboBox();
            this.CountryComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EmailText = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.PhoneText = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.AdressText = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.KPPText = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.NameText = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.INNText = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ListOfAnimalsButton = new System.Windows.Forms.Button();
            this.SaveLegalPersonButton = new System.Windows.Forms.Button();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.LocalityComboBox);
            this.groupBox6.Controls.Add(this.CountryComboBox);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.EmailText);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.PhoneText);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.AdressText);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.KPPText);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.NameText);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.INNText);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(781, 197);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Данные юридического лица";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 153);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Населенный пункт";
            // 
            // LocalityComboBox
            // 
            this.LocalityComboBox.FormattingEnabled = true;
            this.LocalityComboBox.Location = new System.Drawing.Point(282, 170);
            this.LocalityComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.LocalityComboBox.Name = "LocalityComboBox";
            this.LocalityComboBox.Size = new System.Drawing.Size(260, 23);
            this.LocalityComboBox.TabIndex = 25;
            // 
            // CountryComboBox
            // 
            this.CountryComboBox.FormattingEnabled = true;
            this.CountryComboBox.Location = new System.Drawing.Point(11, 170);
            this.CountryComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.CountryComboBox.Name = "CountryComboBox";
            this.CountryComboBox.Size = new System.Drawing.Size(260, 23);
            this.CountryComboBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 153);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "Страна";
            // 
            // EmailText
            // 
            this.EmailText.Location = new System.Drawing.Point(403, 125);
            this.EmailText.Name = "EmailText";
            this.EmailText.Size = new System.Drawing.Size(361, 23);
            this.EmailText.TabIndex = 23;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(403, 107);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(153, 15);
            this.label18.TabIndex = 22;
            this.label18.Text = "Адрес электронной почты";
            // 
            // PhoneText
            // 
            this.PhoneText.Location = new System.Drawing.Point(205, 125);
            this.PhoneText.MaxLength = 11;
            this.PhoneText.Name = "PhoneText";
            this.PhoneText.Size = new System.Drawing.Size(190, 23);
            this.PhoneText.TabIndex = 21;
            this.PhoneText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PhoneText_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(205, 107);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 15);
            this.label17.TabIndex = 20;
            this.label17.Text = "Телефон";
            // 
            // AdressText
            // 
            this.AdressText.Location = new System.Drawing.Point(11, 125);
            this.AdressText.Name = "AdressText";
            this.AdressText.Size = new System.Drawing.Size(190, 23);
            this.AdressText.TabIndex = 19;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 107);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 15);
            this.label16.TabIndex = 18;
            this.label16.Text = "Адрес";
            // 
            // KPPText
            // 
            this.KPPText.Location = new System.Drawing.Point(395, 81);
            this.KPPText.MaxLength = 9;
            this.KPPText.Name = "KPPText";
            this.KPPText.Size = new System.Drawing.Size(375, 23);
            this.KPPText.TabIndex = 17;
            this.KPPText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KPPText_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(395, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 15);
            this.label15.TabIndex = 16;
            this.label15.Text = "КПП";
            // 
            // NameText
            // 
            this.NameText.Location = new System.Drawing.Point(9, 81);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(375, 23);
            this.NameText.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 15);
            this.label14.TabIndex = 14;
            this.label14.Text = "Наименование";
            // 
            // INNText
            // 
            this.INNText.Location = new System.Drawing.Point(9, 37);
            this.INNText.MaxLength = 10;
            this.INNText.Name = "INNText";
            this.INNText.Size = new System.Drawing.Size(755, 23);
            this.INNText.TabIndex = 13;
            this.INNText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.INNText_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 15);
            this.label13.TabIndex = 12;
            this.label13.Text = "ИНН";
            // 
            // ListOfAnimalsButton
            // 
            this.ListOfAnimalsButton.Location = new System.Drawing.Point(661, 215);
            this.ListOfAnimalsButton.Name = "ListOfAnimalsButton";
            this.ListOfAnimalsButton.Size = new System.Drawing.Size(130, 23);
            this.ListOfAnimalsButton.TabIndex = 7;
            this.ListOfAnimalsButton.Text = "Список животных";
            this.ListOfAnimalsButton.UseVisualStyleBackColor = true;
            this.ListOfAnimalsButton.Click += new System.EventHandler(this.ListOfAnimalsButton_Click);
            // 
            // SaveLegalPersonButton
            // 
            this.SaveLegalPersonButton.Location = new System.Drawing.Point(525, 215);
            this.SaveLegalPersonButton.Name = "SaveLegalPersonButton";
            this.SaveLegalPersonButton.Size = new System.Drawing.Size(130, 23);
            this.SaveLegalPersonButton.TabIndex = 8;
            this.SaveLegalPersonButton.Text = "Сохранить";
            this.SaveLegalPersonButton.UseVisualStyleBackColor = true;
            this.SaveLegalPersonButton.Click += new System.EventHandler(this.SaveLegalPersonButton_Click);
            // 
            // LegalPersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 246);
            this.Controls.Add(this.SaveLegalPersonButton);
            this.Controls.Add(this.ListOfAnimalsButton);
            this.Controls.Add(this.groupBox6);
            this.MaximizeBox = false;
            this.Name = "LegalPersonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Юридическое лицо";
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox6;
        private TextBox EmailText;
        private Label label18;
        private TextBox PhoneText;
        private Label label17;
        private TextBox AdressText;
        private Label label16;
        private TextBox KPPText;
        private Label label15;
        private TextBox NameText;
        private Label label14;
        private TextBox INNText;
        private Label label13;
        private Button ListOfAnimalsButton;
        private Label label3;
        private ComboBox LocalityComboBox;
        private ComboBox CountryComboBox;
        private Label label1;
        private Button SaveLegalPersonButton;
    }
}