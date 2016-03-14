namespace TaskInUI.BigSmallWindowsSample
{
    partial class BigModalWindow
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
            this.btnClose = new System.Windows.Forms.Button();
            this.buttonCreateSmall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(193, 221);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // buttonCreateSmall
            // 
            this.buttonCreateSmall.Location = new System.Drawing.Point(46, 95);
            this.buttonCreateSmall.Name = "buttonCreateSmall";
            this.buttonCreateSmall.Size = new System.Drawing.Size(164, 39);
            this.buttonCreateSmall.TabIndex = 1;
            this.buttonCreateSmall.Text = "Create Small Window";
            this.buttonCreateSmall.UseVisualStyleBackColor = true;
            this.buttonCreateSmall.Click += new System.EventHandler(this.buttonCreateSmall_Click);
            // 
            // BigModalWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.buttonCreateSmall);
            this.Controls.Add(this.btnClose);
            this.Name = "BigModalWindow";
            this.Text = "BigModalWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button buttonCreateSmall;
    }
}