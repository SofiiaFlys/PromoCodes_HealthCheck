namespace PromoCodesWork
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ChoseFileButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ClientCodeLabel = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.ChoseFileButton);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.ClientCodeLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 428);
            this.panel1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(534, 248);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(181, 52);
            this.button4.TabIndex = 6;
            this.button4.Text = "Make Promo Codes in Required Format";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(313, 248);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(181, 52);
            this.button3.TabIndex = 5;
            this.button3.Text = "Check if Required Promo Codes already exist";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(58, 248);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(181, 52);
            this.button2.TabIndex = 4;
            this.button2.Text = "Verify Required Promo Codes Uniqueness";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChoseFileButton
            // 
            this.ChoseFileButton.Location = new System.Drawing.Point(26, 104);
            this.ChoseFileButton.Name = "ChoseFileButton";
            this.ChoseFileButton.Size = new System.Drawing.Size(249, 50);
            this.ChoseFileButton.TabIndex = 3;
            this.ChoseFileButton.Text = "Load Required Promo Codes from choosen file";
            this.ChoseFileButton.UseVisualStyleBackColor = true;
            this.ChoseFileButton.Click += new System.EventHandler(this.ChoseFileButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(157, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "SZGR";
            // 
            // ClientCodeLabel
            // 
            this.ClientCodeLabel.AutoSize = true;
            this.ClientCodeLabel.Location = new System.Drawing.Point(26, 43);
            this.ClientCodeLabel.Name = "ClientCodeLabel";
            this.ClientCodeLabel.Size = new System.Drawing.Size(112, 15);
            this.ClientCodeLabel.TabIndex = 0;
            this.ClientCodeLabel.Text = "Choose Client Code";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label ClientCodeLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button ChoseFileButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}

