﻿namespace lotrpwcheck
{
    partial class Form1
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
            this.checksumButton = new System.Windows.Forms.Button();
            this.locationText = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.locationComboBox = new System.Windows.Forms.ComboBox();
            this.allItemsButton = new System.Windows.Forms.Button();
            this.noItemsButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checksumButton
            // 
            this.checksumButton.Location = new System.Drawing.Point(544, 300);
            this.checksumButton.Name = "checksumButton";
            this.checksumButton.Size = new System.Drawing.Size(128, 62);
            this.checksumButton.TabIndex = 0;
            this.checksumButton.Text = "button1";
            this.checksumButton.UseVisualStyleBackColor = true;
            this.checksumButton.Click += new System.EventHandler(this.ChecksumButton_Click);
            this.checksumButton.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DefaultKeyUp_EnableKeyboardFocus);
            // 
            // locationText
            // 
            this.locationText.AutoSize = true;
            this.locationText.Location = new System.Drawing.Point(6, 19);
            this.locationText.Name = "locationText";
            this.locationText.Size = new System.Drawing.Size(51, 13);
            this.locationText.TabIndex = 1;
            this.locationText.Text = "Location:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.locationComboBox);
            this.groupBox1.Controls.Add(this.locationText);
            this.groupBox1.Location = new System.Drawing.Point(544, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(771, 282);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Password Info";
            // 
            // locationComboBox
            // 
            this.locationComboBox.FormattingEnabled = true;
            this.locationComboBox.Location = new System.Drawing.Point(63, 16);
            this.locationComboBox.Name = "locationComboBox";
            this.locationComboBox.Size = new System.Drawing.Size(133, 21);
            this.locationComboBox.TabIndex = 2;
            this.locationComboBox.SelectedIndexChanged += new System.EventHandler(this.LocationComboBox_SelectedIndexChanged);
            this.locationComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LocationComboBox_KeyUp);
            // 
            // allItemsButton
            // 
            this.allItemsButton.Location = new System.Drawing.Point(687, 300);
            this.allItemsButton.Name = "allItemsButton";
            this.allItemsButton.Size = new System.Drawing.Size(131, 23);
            this.allItemsButton.TabIndex = 3;
            this.allItemsButton.Text = "All Items";
            this.allItemsButton.UseVisualStyleBackColor = true;
            this.allItemsButton.Click += new System.EventHandler(this.AllItemsButton_Click);
            // 
            // noItemsButton
            // 
            this.noItemsButton.Location = new System.Drawing.Point(687, 339);
            this.noItemsButton.Name = "noItemsButton";
            this.noItemsButton.Size = new System.Drawing.Size(131, 23);
            this.noItemsButton.TabIndex = 4;
            this.noItemsButton.Text = "No Items";
            this.noItemsButton.UseVisualStyleBackColor = true;
            this.noItemsButton.Click += new System.EventHandler(this.NoItemsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 496);
            this.Controls.Add(this.noItemsButton);
            this.Controls.Add(this.allItemsButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checksumButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button checksumButton;
        private System.Windows.Forms.Label locationText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button allItemsButton;
        private System.Windows.Forms.Button noItemsButton;
        private System.Windows.Forms.ComboBox locationComboBox;
    }
}

