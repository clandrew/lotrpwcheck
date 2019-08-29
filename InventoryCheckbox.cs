using System;
using System.Drawing;
using System.Windows.Forms;

namespace lotrpwcheck
{
    class InventoryCheckBox
    {
        CheckBox checkbox;
        GroupBox parentGroupBox;
        PasswordView passwordView;
        Checksum checksumButton;

        public InventoryCheckBox(int index, GroupBox p, PasswordView pv, Checksum c)
        {
            checkbox = new CheckBox();
            parentGroupBox = p;
            passwordView = pv;
            checksumButton = c;

            int xPosition = index / 9;
            int yPosition = index % 9;

            checkbox.AutoSize = true;
            checkbox.Location = new System.Drawing.Point(9 + (127 * xPosition), 60 + (23 * yPosition));
            checkbox.Name = $"checkBox{index}";
            checkbox.Size = new System.Drawing.Size(74, 17);
            checkbox.TabIndex = 2;
            checkbox.Text = Names.AllItems[index];
            checkbox.UseVisualStyleBackColor = true;

            if (index == 48)
            {
                // The one ring can be legally encoded into the password. That being said, the bit is ignored by the game.
                checkbox.ForeColor = Color.Gray;
            }

            parentGroupBox.Controls.Add(checkbox);

        }

        public void SetChecked(bool c)
        {
            checkbox.Checked = c;
        }

        public bool GetChecked()
        {
            return checkbox.Checked;
        }

        public CheckBox GetCheckboxUIElement()
        {
            return checkbox;
        }
    }
}
