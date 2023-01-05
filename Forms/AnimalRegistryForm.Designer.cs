namespace PIS_PetRegistry
{
    partial class AnimalRegistryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimalRegistryForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.задатьУсловиеФильтрацииПоПолюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewListAnimals = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonClickQuery = new System.Windows.Forms.Button();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.comboBoxSex = new System.Windows.Forms.ComboBox();
            this.textBoxChipId = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListAnimals)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton4,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1406, 40);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(119, 34);
            this.toolStripDropDownButton1.Tag = "";
            this.toolStripDropDownButton1.Text = "Таблицы";
            this.toolStripDropDownButton1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(340, 40);
            this.toolStripMenuItem2.Text = "Владельцы животных";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
            this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(125, 34);
            this.toolStripDropDownButton4.Text = "Действия";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(385, 40);
            this.toolStripMenuItem3.Text = "Завести учетную карточку";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(385, 40);
            this.toolStripMenuItem4.Text = "Удалить запись";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.задатьУсловиеФильтрацииПоПолюToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(112, 34);
            this.toolStripDropDownButton2.Text = "Экспорт";
            this.toolStripDropDownButton2.ToolTipText = "Экспорт";
            // 
            // задатьУсловиеФильтрацииПоПолюToolStripMenuItem
            // 
            this.задатьУсловиеФильтрацииПоПолюToolStripMenuItem.Name = "задатьУсловиеФильтрацииПоПолюToolStripMenuItem";
            this.задатьУсловиеФильтрацииПоПолюToolStripMenuItem.Size = new System.Drawing.Size(178, 40);
            this.задатьУсловиеФильтрацииПоПолюToolStripMenuItem.Text = "Excel";
            // 
            // dataGridViewListAnimals
            // 
            this.dataGridViewListAnimals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewListAnimals.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewListAnimals.Location = new System.Drawing.Point(0, 157);
            this.dataGridViewListAnimals.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dataGridViewListAnimals.Name = "dataGridViewListAnimals";
            this.dataGridViewListAnimals.RowHeadersWidth = 72;
            this.dataGridViewListAnimals.RowTemplate.Height = 25;
            this.dataGridViewListAnimals.Size = new System.Drawing.Size(1406, 743);
            this.dataGridViewListAnimals.TabIndex = 1;
            this.dataGridViewListAnimals.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridViewListAnimals.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewListAnimals_CellContentDoubleClick);
            this.dataGridViewListAnimals.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewListAnimals_ColumnHeaderMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonClickQuery);
            this.groupBox1.Controls.Add(this.comboBoxCategory);
            this.groupBox1.Controls.Add(this.comboBoxSex);
            this.groupBox1.Controls.Add(this.textBoxChipId);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 40);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(1406, 105);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фильтры";
            // 
            // buttonClickQuery
            // 
            this.buttonClickQuery.Location = new System.Drawing.Point(1144, 38);
            this.buttonClickQuery.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonClickQuery.Name = "buttonClickQuery";
            this.buttonClickQuery.Size = new System.Drawing.Size(252, 46);
            this.buttonClickQuery.TabIndex = 4;
            this.buttonClickQuery.Text = "Сделать запрос";
            this.buttonClickQuery.UseVisualStyleBackColor = true;
            this.buttonClickQuery.Click += new System.EventHandler(this.buttonClickQuery_Click);
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(619, 38);
            this.comboBoxCategory.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(205, 38);
            this.comboBoxCategory.TabIndex = 3;
            this.comboBoxCategory.Text = "Категория";
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBoxSex
            // 
            this.comboBoxSex.FormattingEnabled = true;
            this.comboBoxSex.Items.AddRange(new object[] {
            "Девочка",
            "Мальчик"});
            this.comboBoxSex.Location = new System.Drawing.Point(422, 38);
            this.comboBoxSex.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.comboBoxSex.Name = "comboBoxSex";
            this.comboBoxSex.Size = new System.Drawing.Size(187, 38);
            this.comboBoxSex.TabIndex = 2;
            this.comboBoxSex.Text = "Пол";
            // 
            // textBoxChipId
            // 
            this.textBoxChipId.Location = new System.Drawing.Point(216, 38);
            this.textBoxChipId.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxChipId.Name = "textBoxChipId";
            this.textBoxChipId.PlaceholderText = "Номер чипа";
            this.textBoxChipId.Size = new System.Drawing.Size(193, 35);
            this.textBoxChipId.TabIndex = 1;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(10, 38);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.PlaceholderText = "Кличка";
            this.textBoxName.Size = new System.Drawing.Size(193, 35);
            this.textBoxName.TabIndex = 0;
            // 
            // AnimalRegistryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 900);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewListAnimals);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "AnimalRegistryForm";
            this.Text = "Реестр учета домашних животных";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListAnimals)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem задатьУсловиеФильтрацииПоПолюToolStripMenuItem;
        private DataGridView dataGridViewListAnimals;
        private ToolStripDropDownButton toolStripDropDownButton4;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private GroupBox groupBox1;
        private Button buttonClickQuery;
        private ComboBox comboBoxCategory;
        private ComboBox comboBoxSex;
        private TextBox textBoxChipId;
        private TextBox textBoxName;
    }
}