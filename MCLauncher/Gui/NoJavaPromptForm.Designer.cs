namespace MCLauncher
{
    partial class NoJavaPromptForm
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
            this.LabelPromptTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ButtonDownloadJava = new System.Windows.Forms.Button();
            this.platformInfo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelPromptTitle
            // 
            this.LabelPromptTitle.AutoSize = true;
            this.LabelPromptTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelPromptTitle.Location = new System.Drawing.Point(13, 14);
            this.LabelPromptTitle.Name = "LabelPromptTitle";
            this.LabelPromptTitle.Size = new System.Drawing.Size(203, 19);
            this.LabelPromptTitle.TabIndex = 0;
            this.LabelPromptTitle.Text = "MoeCraft 需要 {0} 才能运行";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(524, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "你的电脑可能没有安装 Java 或 Java 版本太低，你可以从以下地址获取 Java：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(12, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "提示：";
            // 
            // ButtonDownloadJava
            // 
            this.ButtonDownloadJava.Location = new System.Drawing.Point(15, 72);
            this.ButtonDownloadJava.Name = "ButtonDownloadJava";
            this.ButtonDownloadJava.Size = new System.Drawing.Size(424, 35);
            this.ButtonDownloadJava.TabIndex = 4;
            this.ButtonDownloadJava.Text = "点击获取 {0}";
            this.ButtonDownloadJava.UseVisualStyleBackColor = true;
            this.ButtonDownloadJava.Click += new System.EventHandler(this.button1_Click);
            // 
            // platformInfo
            // 
            this.platformInfo.AutoSize = true;
            this.platformInfo.Location = new System.Drawing.Point(13, 124);
            this.platformInfo.Name = "platformInfo";
            this.platformInfo.Size = new System.Drawing.Size(562, 20);
            this.platformInfo.TabIndex = 5;
            this.platformInfo.Text = "请先点击 \"Accept License Agreement\"，根据您的系统类型选择恰当的 Java 版本\r\n";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(448, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "64 位系统选择 \"Windows x64\"，32 位系统选择 \"Windows x86\"";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(341, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "请勿使用 Java9 或更高版本，否则将无法启动游戏";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(422, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "然后点击对应的右边以 .exe 结尾的链接，下载安装程序并安装";
            // 
            // NoJavaPromptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 255);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.platformInfo);
            this.Controls.Add(this.ButtonDownloadJava);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LabelPromptTitle);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NoJavaPromptForm";
            this.Text = "MoeCraft Launcher for Windows";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelPromptTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ButtonDownloadJava;
        private System.Windows.Forms.Label platformInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}