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
            this.radioPanel.SuspendLayout();
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
            this.radioPanel.Size = new System.Drawing.Size(245, 52);
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
            // FrmScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 496);
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
    }
}