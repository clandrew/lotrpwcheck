using System;
using System.Drawing;
using System.Windows.Forms;

namespace lotrpwcheck
{
    class CharacterUI
    {
        private CheckBox characterEnabledCheckBox;
        private NumericUpDown levelNumericUpDown;
        private ComboBox weaponComboBox;
        private ComboBox armorComboBox;
        private GroupBox groupBox;
        private Label levelLabel;
        private PasswordView passwordView;
        private int passwordStartIndex;
        private Checksum checksumButton;
        private bool shouldUpdatePasswordString;

        public CharacterUI(string characterName, GroupBox parentGroupBox, int x, int y, PasswordView pv, Checksum c, int psi)
        {
            this.characterEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.levelNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.weaponComboBox = new System.Windows.Forms.ComboBox();
            this.armorComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.levelLabel = new System.Windows.Forms.Label();

            string lowercaseName = characterName.ToLower();

            Point groupBoxLocation = new System.Drawing.Point(x, y);

            passwordView = pv;
            checksumButton = c;
            passwordStartIndex = psi;
            shouldUpdatePasswordString = true;

            // 
            // groupBox6
            // 
            this.groupBox.Controls.Add(this.characterEnabledCheckBox);
            this.groupBox.Controls.Add(this.levelNumericUpDown);
            this.groupBox.Controls.Add(this.levelLabel);
            this.groupBox.Controls.Add(this.armorComboBox);
            this.groupBox.Controls.Add(this.weaponComboBox);
            this.groupBox.Location = groupBoxLocation;
            this.groupBox.Name = "groupBox6";
            this.groupBox.Size = new System.Drawing.Size(122, 124);
            this.groupBox.TabIndex = 7;
            this.groupBox.TabStop = false;
            this.groupBox.Text = characterName;

            this.characterEnabledCheckBox.AutoSize = true;
            this.characterEnabledCheckBox.Location = new System.Drawing.Point(9, 19);
            this.characterEnabledCheckBox.Name = $"{lowercaseName}EnabledCheckBox";
            this.characterEnabledCheckBox.Size = new System.Drawing.Size(65, 17);
            this.characterEnabledCheckBox.TabIndex = 9;
            this.characterEnabledCheckBox.Text = "Enabled";
            this.characterEnabledCheckBox.UseVisualStyleBackColor = true;
            this.characterEnabledCheckBox.CheckedChanged += new System.EventHandler(this.EnabledCheckBox_CheckedChanged);

            this.levelNumericUpDown.Location = new System.Drawing.Point(49, 41);
            this.levelNumericUpDown.Maximum = new decimal(new int[] {
                    99,
                    0,
                    0,
                    0});
            this.levelNumericUpDown.Name = $"{lowercaseName}LevelNumericUpDown";
            this.levelNumericUpDown.Size = new System.Drawing.Size(66, 20);
            this.levelNumericUpDown.TabIndex = 13;
            this.levelNumericUpDown.ValueChanged += new System.EventHandler(this.LevelNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(6, 43);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(36, 13);
            this.levelLabel.TabIndex = 10;
            this.levelLabel.Text = "Level:";

            this.armorComboBox.FormattingEnabled = true;
            this.armorComboBox.Location = new System.Drawing.Point(9, 93);
            this.armorComboBox.Name = $"{lowercaseName}ArmorComboBox";
            this.armorComboBox.Size = new System.Drawing.Size(106, 21);
            this.armorComboBox.TabIndex = 12;
            this.armorComboBox.SelectedIndexChanged += new System.EventHandler(this.ArmorComboBox_SelectedIndexChanged);

            this.weaponComboBox.FormattingEnabled = true;
            this.weaponComboBox.Location = new System.Drawing.Point(9, 66);
            this.weaponComboBox.Name = $"{lowercaseName}WeaponComboBox";
            this.weaponComboBox.Size = new System.Drawing.Size(106, 21);
            this.weaponComboBox.TabIndex = 11;
            this.weaponComboBox.SelectedIndexChanged += new System.EventHandler(this.WeaponComboBox_SelectedIndexChanged);

            parentGroupBox.Controls.Add(groupBox);

            foreach (var s in Names.AllWeapons)
            {
                weaponComboBox.Items.Add(s);
            }

            foreach (var s in Names.AllArmor)
            {
                armorComboBox.Items.Add(s);
            }

            levelLabel.Enabled = false;
            levelNumericUpDown.Enabled = false;
            armorComboBox.Enabled = false;
            weaponComboBox.Enabled = false;

            weaponComboBox.SelectedIndex = 0;
            armorComboBox.SelectedIndex = 0;
        }

        public int PasswordStartIndex { get { return passwordStartIndex; } }

        private void UpdateUIFromCode3(
            int code1,
            int code3, 
            int badSectionLevel, 
            Names.Armor badSectionArmor1,
            Names.Armor badSectionArmor2,
            int goodSectionLevel,
            Names.Armor goodSectionArmor1,
            Names.Armor goodSectionArmor2)
        {
            if (code3 < 16)
            {
                // Bad section
                levelNumericUpDown.Value = badSectionLevel;
                if (code3 % 2 == 0)
                {
                    weaponComboBox.SelectedIndex = (int)Names.Weapon.OldDagger;
                    armorComboBox.SelectedIndex = (int)badSectionArmor1;
                }
                else
                {
                    weaponComboBox.SelectedIndex = (int)Names.Weapon.Invalid;
                    armorComboBox.SelectedIndex = (int)badSectionArmor2;
                }
            }
            else
            {
                // Good section
                levelNumericUpDown.Value = goodSectionLevel;
                if (code3 % 2 == 0)
                {
                    armorComboBox.SelectedIndex = (int)goodSectionArmor1;
                }
                else
                {
                    armorComboBox.SelectedIndex = (int)goodSectionArmor2;
                }
                int weaponDecode = (code3 - 16) / 2;
                weaponComboBox.SelectedIndex = weaponDecode;
            }

            if (code1 < 10)
            {
                levelNumericUpDown.Value += code1;
            }
            else if (code1 >= 16 && code1 <= 25)
            {
                levelNumericUpDown.Value += code1 - 16 + 10;
            }
            else
            {
                weaponComboBox.SelectedIndex = (int)Names.Weapon.Corruption;
                armorComboBox.SelectedIndex = (int)Names.Armor.Corruption;
            }

            if (armorComboBox.SelectedIndex == (int)Names.Armor.Corruption)
            {
                weaponComboBox.SelectedIndex = (int)Names.Weapon.Corruption;
            }
        }

        public void UpdateUI(char[] code)
        {
            int code1 = CharacterCode.CharToIndex(code[0]);

            int code2 = CharacterCode.CharToIndex(code[1]);

            int code3 = CharacterCode.CharToIndex(code[2]);

            // Do not reflect the changes back into the password.
            shouldUpdatePasswordString = false;

            if (code1 == 0 && code2 == 0 && code3 == 0)
            {
                characterEnabledCheckBox.Checked = false;
                levelLabel.Enabled = false;
                levelNumericUpDown.Enabled = false;
                weaponComboBox.Enabled = false;
                armorComboBox.Enabled = false;

                shouldUpdatePasswordString = true;
                return;
            }

            characterEnabledCheckBox.Checked = true;
            levelLabel.Enabled = true;
            levelNumericUpDown.Enabled = true;
            weaponComboBox.Enabled = true;
            armorComboBox.Enabled = true;

            switch(code2)
            {
                case 0: UpdateUIFromCode3(code1, code3, 0, Names.Armor.ClothCloak, Names.Armor.Invalid, 0, Names.Armor.ClothCloak, Names.Armor.PlateMail); break;
                case 1: UpdateUIFromCode3(code1, code3, 0, Names.Armor.ClothCloak, Names.Armor.Invalid, 20, Names.Armor.ClothCloak, Names.Armor.PlateMail); break;
                case 2: UpdateUIFromCode3(code1, code3, 20, Names.Armor.PlateMail, Names.Armor.Invalid, 40, Names.Armor.ClothCloak, Names.Armor.PlateMail); break;
                case 3: UpdateUIFromCode3(code1, code3, 20, Names.Armor.PlateMail, Names.Armor.Invalid, 60, Names.Armor.ClothCloak, Names.Armor.PlateMail); break;
                case 4: UpdateUIFromCode3(code1, code3, 20, Names.Armor.PlateMail, Names.Armor.Invalid, 80, Names.Armor.ClothCloak, Names.Armor.PlateMail); break;

                case 5: UpdateUIFromCode3(code1, code3, 20, Names.Armor.PlateMail, Names.Armor.Invalid, 0, Names.Armor.Corruption, Names.Armor.Corruption); break;

                case 6: 
                case 7: UpdateUIFromCode3(code1, code3, 0, Names.Armor.ClothCloak, Names.Armor.Invalid, 0, Names.Armor.Corruption, Names.Armor.Corruption); break;

                case 8: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 0, Names.Armor.PaddedArmor, Names.Armor.MithrilArmor); break;
                case 9: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 20, Names.Armor.PaddedArmor, Names.Armor.MithrilArmor); break;
                case 10: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 40, Names.Armor.PaddedArmor, Names.Armor.MithrilArmor); break;
                case 11: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 60, Names.Armor.PaddedArmor, Names.Armor.MithrilArmor); break;
                case 12: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 80, Names.Armor.PaddedArmor, Names.Armor.MithrilArmor); break;

                case 13: 
                case 14: 
                case 15: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 0, Names.Armor.Corruption, Names.Armor.Corruption); break;

                case 16: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 0, Names.Armor.LeatherArmor, Names.Armor.Corruption); break;
                case 17: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 20, Names.Armor.LeatherArmor, Names.Armor.Corruption); break;
                case 18: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 40, Names.Armor.LeatherArmor, Names.Armor.Corruption); break;
                case 19: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 60, Names.Armor.LeatherArmor, Names.Armor.Corruption); break;
                case 20: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 80, Names.Armor.LeatherArmor, Names.Armor.Corruption); break;

                case 21:
                case 22:
                case 23: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 0, Names.Armor.Corruption, Names.Armor.Corruption); break;

                case 24: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 0, Names.Armor.ChainMail, Names.Armor.Corruption); break;
                case 25: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 20, Names.Armor.ChainMail, Names.Armor.Corruption); break;
                case 26: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 40, Names.Armor.ChainMail, Names.Armor.Corruption); break;
                case 27: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 60, Names.Armor.ChainMail, Names.Armor.Corruption); break;
                case 28: UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 80, Names.Armor.ChainMail, Names.Armor.Corruption); break;

                case 29:
                case 30:
                case 31:
                    UpdateUIFromCode3(code1, code3, 0, Names.Armor.Invalid, Names.Armor.Invalid, 0, Names.Armor.Corruption, Names.Armor.Corruption); break;

                default: break;
            }


            shouldUpdatePasswordString = true;
        }

        private void EnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (characterEnabledCheckBox.Checked)
            {
                levelLabel.Enabled = true;
                levelNumericUpDown.Enabled = true;
                weaponComboBox.Enabled = true;
                armorComboBox.Enabled = true;
            }
            else
            {
                levelLabel.Enabled = false;
                levelNumericUpDown.Enabled = false;
                weaponComboBox.Enabled = false;
                armorComboBox.Enabled = false;
            }

            if (shouldUpdatePasswordString)
                OnCharacterLevelWeaponArmorUIChange();
        }

        private void LevelNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            OnCharacterLevelWeaponArmorUIChange();
        }

        private void ArmorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCharacterLevelWeaponArmorUIChange();
        }

        private void WeaponComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCharacterLevelWeaponArmorUIChange();
        }

        private void OnCharacterLevelWeaponArmorUIChange()
        {
            if (shouldUpdatePasswordString)
            {
                passwordView.UpdatePassword_Lines1And2(
                    levelLabel.Enabled,
                    (int)levelNumericUpDown.Value,
                    weaponComboBox.SelectedIndex,
                    armorComboBox.SelectedIndex,
                    passwordStartIndex);
            }

            checksumButton.UpdateButtonState(passwordView.IsValidChecksum());
        }
    }
}
