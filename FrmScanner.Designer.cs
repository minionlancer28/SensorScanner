namespace SensorScanner
{
    partial class FrmScanner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScanner));
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnScan = new System.Windows.Forms.Button();
            this.radioPanel = new System.Windows.Forms.Panel();
            this.radioHome = new System.Windows.Forms.RadioButton();
            this.radioCore = new System.Windows.Forms.RadioButton();
            this.RightBottomPanel_1 = new System.Windows.Forms.Panel();
            this.yearLbl = new System.Windows.Forms.Label();
            this.yearCombo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.monthCombo = new System.Windows.Forms.ComboBox();
            this.radioPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxLog
            // 
            this.lbxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxLog.BackColor = System.Drawing.Color.Black;
            this.lbxLog.ForeColor = System.Drawing.Color.White;
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.Location = new System.Drawing.Point(12, 261);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(721, 225);
            this.lbxLog.TabIndex = 1;
            // 
            // tableLayout
            // 
            this.tableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayout.AutoScroll = true;
            this.tableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayout.CausesValidation = false;
            this.tableLayout.ColumnCount = 4;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.37374F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.28283F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.17172F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.17172F));
            this.tableLayout.Location = new System.Drawing.Point(12, 70);
            this.tableLayout.MaximumSize = new System.Drawing.Size(9999999, 9999999);
            this.tableLayout.MinimumSize = new System.Drawing.Size(707, 185);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.tableLayout.RowCount = 1;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayout.Size = new System.Drawing.Size(721, 185);
            this.tableLayout.TabIndex = 3;
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScan.Location = new System.Drawing.Point(551, 15);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(168, 41);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "スキャン";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // radioPanel
            // 
            this.radioPanel.Controls.Add(this.radioHome);
            this.radioPanel.Controls.Add(this.radioCore);
            this.radioPanel.Location = new System.Drawing.Point(12, 12);
            this.radioPanel.Name = "radioPanel";
            this.radioPanel.Size = new System.Drawing.Size(203, 52);
            this.radioPanel.TabIndex = 2;
            // 
            // radioHome
            // 
            this.radioHome.AutoSize = true;
            this.radioHome.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioHome.Location = new System.Drawing.Point(3, 26);
            this.radioHome.Name = "radioHome";
            this.radioHome.Size = new System.Drawing.Size(190, 23);
            this.radioHome.TabIndex = 1;
            this.radioHome.Text = "HOME(HomeDevice)";
            this.radioHome.UseVisualStyleBackColor = true;
            // 
            // radioCore
            // 
            this.radioCore.AutoSize = true;
            this.radioCore.Checked = true;
            this.radioCore.Font = new System.Drawing.Font("MS PGothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCore.Location = new System.Drawing.Point(3, 3);
            this.radioCore.Name = "radioCore";
            this.radioCore.Size = new System.Drawing.Size(159, 23);
            this.radioCore.TabIndex = 0;
            this.radioCore.TabStop = true;
            this.radioCore.Text = "CORE(WearDev)";
            this.radioCore.UseVisualStyleBackColor = true;
            // 
            // RightBottomPanel_1
            // 
            this.RightBottomPanel_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RightBottomPanel_1.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.RightBottomPanel_1.Location = new System.Drawing.Point(735, 495);
            this.RightBottomPanel_1.Name = "RightBottomPanel_1";
            this.RightBottomPanel_1.Size = new System.Drawing.Size(20, 2);
            this.RightBottomPanel_1.TabIndex = 6;
            // 
            // yearLbl
            // 
            this.yearLbl.AutoSize = true;
            this.yearLbl.Font = new System.Drawing.Font("MS PGothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearLbl.Location = new System.Drawing.Point(6, 12);
            this.yearLbl.Margin = new System.Windows.Forms.Padding(0);
            this.yearLbl.Name = "yearLbl";
            this.yearLbl.Size = new System.Drawing.Size(43, 21);
            this.yearLbl.TabIndex = 7;
            this.yearLbl.Text = "202";
            this.yearLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // yearCombo
            // 
            this.yearCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearCombo.Font = new System.Drawing.Font("MS Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearCombo.FormattingEnabled = true;
            this.yearCombo.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.yearCombo.Location = new System.Drawing.Point(49, 10);
            this.yearCombo.Name = "yearCombo";
            this.yearCombo.Size = new System.Drawing.Size(39, 27);
            this.yearCombo.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.yearLbl);
            this.panel1.Controls.Add(this.monthCombo);
            this.panel1.Controls.Add(this.yearCombo);
            this.panel1.Location = new System.Drawing.Point(224, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 46);
            this.panel1.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS PGothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(88, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "年";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS PGothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(171, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "月";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // monthCombo
            // 
            this.monthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthCombo.Font = new System.Drawing.Font("MS Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthCombo.FormattingEnabled = true;
            this.monthCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.monthCombo.Location = new System.Drawing.Point(127, 11);
            this.monthCombo.Name = "monthCombo";
            this.monthCombo.Size = new System.Drawing.Size(43, 27);
            this.monthCombo.TabIndex = 8;
            // 
            // FrmScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 496);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RightBottomPanel_1);
            this.Controls.Add(this.radioPanel);
            this.Controls.Add(this.lbxLog);
            this.Controls.Add(this.tableLayout);
            this.Controls.Add(this.btnScan);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(770, 535);
            this.Name = "FrmScanner";
            this.Text = "シリアルID記録ツール";
            this.radioPanel.ResumeLayout(false);
            this.radioPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox lbxLog;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Panel radioPanel;
        private System.Windows.Forms.RadioButton radioHome;
        private System.Windows.Forms.RadioButton radioCore;
		private System.Windows.Forms.Panel RightBottomPanel_1;
		private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Label yearLbl;
        private System.Windows.Forms.ComboBox yearCombo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox monthCombo;
    }
}