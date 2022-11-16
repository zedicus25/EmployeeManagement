namespace Server.View
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logRTB = new System.Windows.Forms.RichTextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logRTB
            // 
            this.logRTB.Location = new System.Drawing.Point(12, 12);
            this.logRTB.Name = "logRTB";
            this.logRTB.ReadOnly = true;
            this.logRTB.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.logRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.logRTB.Size = new System.Drawing.Size(776, 323);
            this.logRTB.TabIndex = 0;
            this.logRTB.Text = "";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(12, 353);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(85, 85);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(113, 353);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(85, 85);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.logRTB);
            this.Name = "MainForm";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox logRTB;
        private Button startBtn;
        private Button stopBtn;
    }
}