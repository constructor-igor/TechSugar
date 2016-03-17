namespace TaskInUI.FilesComboBox
{
    partial class FilesComboBoxSample
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
            this.filesComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.filesMaskTextBox = new System.Windows.Forms.TextBox();
            this.maskTextBox = new System.Windows.Forms.TextBox();
            this.btnOpenExplorer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filesComboBox
            // 
            this.filesComboBox.FormattingEnabled = true;
            this.filesComboBox.Location = new System.Drawing.Point(57, 56);
            this.filesComboBox.Name = "filesComboBox";
            this.filesComboBox.Size = new System.Drawing.Size(121, 21);
            this.filesComboBox.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(57, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load files from ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // filesMaskTextBox
            // 
            this.filesMaskTextBox.Location = new System.Drawing.Point(138, 116);
            this.filesMaskTextBox.Name = "filesMaskTextBox";
            this.filesMaskTextBox.Size = new System.Drawing.Size(134, 20);
            this.filesMaskTextBox.TabIndex = 2;
            this.filesMaskTextBox.Text = "d:\\";
            // 
            // maskTextBox
            // 
            this.maskTextBox.Location = new System.Drawing.Point(278, 117);
            this.maskTextBox.Name = "maskTextBox";
            this.maskTextBox.Size = new System.Drawing.Size(134, 20);
            this.maskTextBox.TabIndex = 3;
            this.maskTextBox.Text = "*.png";
            // 
            // btnOpenExplorer
            // 
            this.btnOpenExplorer.Location = new System.Drawing.Point(234, 53);
            this.btnOpenExplorer.Name = "btnOpenExplorer";
            this.btnOpenExplorer.Size = new System.Drawing.Size(138, 23);
            this.btnOpenExplorer.TabIndex = 4;
            this.btnOpenExplorer.Text = "open explorer";
            this.btnOpenExplorer.UseVisualStyleBackColor = true;
            this.btnOpenExplorer.Click += new System.EventHandler(this.btnOpenExplorer_Click);
            // 
            // FilesComboBoxSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 262);
            this.Controls.Add(this.btnOpenExplorer);
            this.Controls.Add(this.maskTextBox);
            this.Controls.Add(this.filesMaskTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.filesComboBox);
            this.Name = "FilesComboBoxSample";
            this.Text = "FilesComboBoxSample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox filesComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox filesMaskTextBox;
        private System.Windows.Forms.TextBox maskTextBox;
        private System.Windows.Forms.Button btnOpenExplorer;
    }
}