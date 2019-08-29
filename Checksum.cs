using System;
using System.Drawing;
using System.Windows.Forms;

namespace lotrpwcheck
{

    class Checksum
    {
        private System.Windows.Forms.Button checksumButton;
        private PasswordView passwordView;

        public Checksum(PasswordView pv, GroupBox parentBox)
        {
            passwordView = pv;
            this.checksumButton = new System.Windows.Forms.Button();

            // 
            // checksumButton
            // 
            this.checksumButton.Location = new System.Drawing.Point(637, 491);
            this.checksumButton.Name = "checksumButton";
            this.checksumButton.Size = new System.Drawing.Size(128, 62);
            this.checksumButton.TabIndex = 0;
            this.checksumButton.Text = "button1";
            this.checksumButton.UseVisualStyleBackColor = true;
            this.checksumButton.Click += new System.EventHandler(this.ChecksumButton_Click);

            parentBox.Controls.Add(this.checksumButton);
        }

        private void ChecksumButton_Click(object sender, EventArgs e)
        {
            passwordView.FixChecksum();
            UpdateButtonState(true);
        }

        public void UpdateButtonState(bool isValidChecksum)
        {
            if (isValidChecksum)
            {
                checksumButton.BackColor = Color.LightGreen;
                checksumButton.Text = "Checksum✔️";
            }
            else
            {
                checksumButton.BackColor = Color.LightYellow;
                checksumButton.Text = "Fix Checksum";
            }
        }
    }

}
