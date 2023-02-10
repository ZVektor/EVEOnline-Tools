namespace EVEOnline.Forms
{
    partial class FormLoadData
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
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btmAddSystems = new System.Windows.Forms.Button();
            this.btnAddConstellations = new System.Windows.Forms.Button();
            this.btnAddRegion = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // progressBar3
            // 
            this.progressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar3.Location = new System.Drawing.Point(496, 112);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(216, 9);
            this.progressBar3.Step = 1;
            this.progressBar3.TabIndex = 6;
            // 
            // progressBar2
            // 
            this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar2.Location = new System.Drawing.Point(496, 73);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(216, 9);
            this.progressBar2.Step = 1;
            this.progressBar2.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(496, 35);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(216, 9);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 4;
            // 
            // btmAddSystems
            // 
            this.btmAddSystems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btmAddSystems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btmAddSystems.Location = new System.Drawing.Point(496, 89);
            this.btmAddSystems.Name = "btmAddSystems";
            this.btmAddSystems.Size = new System.Drawing.Size(216, 23);
            this.btmAddSystems.TabIndex = 3;
            this.btmAddSystems.Text = "Загрузить СИСТЕМЫ";
            this.btmAddSystems.UseVisualStyleBackColor = true;
            this.btmAddSystems.Click += new System.EventHandler(this.btmAddSystems_Click);
            // 
            // btnAddConstellations
            // 
            this.btnAddConstellations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddConstellations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddConstellations.Location = new System.Drawing.Point(496, 50);
            this.btnAddConstellations.Name = "btnAddConstellations";
            this.btnAddConstellations.Size = new System.Drawing.Size(216, 23);
            this.btnAddConstellations.TabIndex = 2;
            this.btnAddConstellations.Text = "Загрузить СОЗВЕЗДИЯ\r\n";
            this.btnAddConstellations.UseVisualStyleBackColor = true;
            this.btnAddConstellations.Click += new System.EventHandler(this.btnAddConstellations_Click);
            // 
            // btnAddRegion
            // 
            this.btnAddRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRegion.Location = new System.Drawing.Point(496, 12);
            this.btnAddRegion.Name = "btnAddRegion";
            this.btnAddRegion.Size = new System.Drawing.Size(216, 23);
            this.btnAddRegion.TabIndex = 1;
            this.btnAddRegion.Text = "Загрузить РЕГИОНЫ";
            this.btnAddRegion.UseVisualStyleBackColor = true;
            this.btnAddRegion.Click += new System.EventHandler(this.btnAddRegions_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(12, 10);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(478, 454);
            this.listBox1.TabIndex = 0;
            // 
            // FormLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 476);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnAddRegion);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.btnAddConstellations);
            this.Controls.Add(this.btmAddSystems);
            this.Controls.Add(this.progressBar1);
            this.Name = "FormLoadData";
            this.Text = "ЗАГРУЗКА ДАННЫХ";
            this.Load += new System.EventHandler(this.FormLoadData_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private ProgressBar progressBar3;
        private ProgressBar progressBar2;
        private ProgressBar progressBar1;
        private Button btmAddSystems;
        private Button btnAddConstellations;
        private Button btnAddRegion;
        private ListBox listBox1;
    }
}