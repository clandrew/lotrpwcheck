#define USE_CHECKBOXES

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace lotrpwcheck
{
    struct Coord
    {
        public int X;
        public int Y;
    }
    
    public partial class Form1 : Form
    {
        PasswordView passwordView;
        Checksum checksum;

#if USE_CHECKBOXES
        InventoryCheckBox[] inventoryCheckboxes;
#endif        

        List<CharacterUI> characterUIs;

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            passwordView = new PasswordView(passwordUI);
            checksum = new Checksum(passwordView, groupBox1);

            locationComboBox.Items.Add("Hobbiton");
            locationComboBox.Items.Add("Brandywine Bridge");
            locationComboBox.Items.Add("Farmer Maggot");
            locationComboBox.Items.Add("Ferry");
            locationComboBox.Items.Add("Crickhollow");
            locationComboBox.Items.Add("Tom Bombadil's House");
            locationComboBox.Items.Add("Barrow Downs Stones");
            locationComboBox.Items.Add("Crossroads");
            locationComboBox.Items.Add("Rivendell");
            locationComboBox.Items.Add("Moria entrance");
            locationComboBox.Items.Add("Moria 1 (glitched)");
            locationComboBox.Items.Add("Moria 2 (glitched)");
            locationComboBox.Items.Add("(invalid)");
            locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;


            inventoryCheckboxes = new InventoryCheckBox[54];
            for (int i = 0; i < 54; ++i)
            {
                inventoryCheckboxes[i] = new InventoryCheckBox(i, this.groupBox1, passwordView, checksum);
                inventoryCheckboxes[i].GetCheckboxUIElement().CheckedChanged += OnInventoryCheckboxCheckChanged;
            }

            int xDisp = 128;
            characterUIs = new List<CharacterUI>();

            characterUIs.Add(new CharacterUI("Samwise", this.groupBox1, 9 + (xDisp * 0), 302, passwordView, checksum, 0));
            characterUIs.Add(new CharacterUI("Merry", this.groupBox1, 9 + (xDisp * 1), 302, passwordView, checksum, 3));
            characterUIs.Add(new CharacterUI("Frodo", this.groupBox1, 9 + (xDisp * 2), 302, passwordView, checksum, 6));
            characterUIs.Add(new CharacterUI("Pippin", this.groupBox1, 9 + (xDisp * 3), 302, passwordView, checksum, 9));

            characterUIs.Add(new CharacterUI("Legolas", this.groupBox1, 9 + (xDisp * 0), 432, passwordView, checksum, 12));
            characterUIs.Add(new CharacterUI("Aragorn", this.groupBox1, 9 + (xDisp * 1), 432, passwordView, checksum, 15));
            characterUIs.Add(new CharacterUI("Gimli", this.groupBox1, 9 + (xDisp * 2), 432, passwordView, checksum, 18));
            characterUIs.Add(new CharacterUI("Gandalf", this.groupBox1, 9 + (xDisp * 3), 432, passwordView, checksum, 21));

            UpdateLocationDropdownUI();
            UpdateInventoryCheckboxUI();
            checksum.UpdateButtonState(passwordView.IsValidChecksum());
        }


        private void OnInventoryCheckboxCheckChanged(object sender, EventArgs e)
        {
            // Update password based on the checkbox UI
            int inventoryCheckboxIndex = 0;

            int passwordStringIndex = 0;
            char[] inventoryCharacterCodes = new char[12];
            for (int column = 0; column < 6; column++)
            {
                {
                    int l0 = 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 0].GetChecked() ? 0x1 : 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 1].GetChecked() ? 0x2 : 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 2].GetChecked() ? 0x4 : 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 3].GetChecked() ? 0x8 : 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 4].GetChecked() ? 0x10 : 0;
                    inventoryCharacterCodes[passwordStringIndex] = CharacterCode.IndexToChar(l0);
                    passwordStringIndex++;
                    inventoryCheckboxIndex += 5;
                }
                {
                    int l1 = 0;
                    l1 |= inventoryCheckboxes[inventoryCheckboxIndex + 0].GetChecked() ? 0x1 : 0;
                    l1 |= inventoryCheckboxes[inventoryCheckboxIndex + 1].GetChecked() ? 0x2 : 0;
                    l1 |= inventoryCheckboxes[inventoryCheckboxIndex + 2].GetChecked() ? 0x4 : 0;
                    l1 |= inventoryCheckboxes[inventoryCheckboxIndex + 3].GetChecked() ? 0x8 : 0;

                    inventoryCharacterCodes[passwordStringIndex] = CharacterCode.IndexToChar(l1);
                    passwordStringIndex++;
                    inventoryCheckboxIndex += 4;
                }
            }

            passwordView.UpdatePassword_Inventory(inventoryCharacterCodes);
            checksum.UpdateButtonState(passwordView.IsValidChecksum());
        }


        private void PasswordUI_Paint(object sender, PaintEventArgs e)
        {
            passwordView.Paint(e);
        }

        void HandleKeyPress(KeyEventArgs e)
        {
            PasswordView.KeyPressResult result = passwordView.HandleKeyPress(e);

            if (result.ShouldRefreshLocationInventoryAndCharacters)
            {
                UpdateLocationDropdownUI();
                UpdateInventoryCheckboxUI();
                UpdateCharactersUI();
            }

            if (result.ShouldRecomputeChecksum)
            {
                checksum.UpdateButtonState(passwordView.IsValidChecksum());
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKeyPress(e);
        }

        private void PasswordUI_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKeyPress(e);
        }

        private void DefaultKeyUp_EnableKeyboardFocus(object sender, KeyEventArgs e)
        {
            HandleKeyPress(e);
        }

        private void LocationComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKeyPress(e);
        }

        void UpdateLocationDropdownUI()
        {
            int locationCode = passwordView.GetLocationCode();

            if (locationCode < locationComboBox.Items.Count - 1)
            {
                locationComboBox.SelectedIndex = locationCode;
            }
            else
            {
                locationComboBox.SelectedIndex = locationComboBox.Items.Count - 1;
            }
        }

        void UpdateInventoryCheckboxUI()
        {
#if USE_CHECKBOXES
            string inventory = passwordView.GetInventoryCharacterCodes();

            int inventoryIndex = 0;
            int inventoryChrIndex = 0;
            for (int column = 0; column < 6; column++)
            {
                {
                    int chr = CharacterCode.CharToIndex(inventory[inventoryChrIndex]);
                    inventoryCheckboxes[inventoryIndex + 0].SetChecked((chr & 0x1) != 0);
                    inventoryCheckboxes[inventoryIndex + 1].SetChecked((chr & 0x2) != 0);
                    inventoryCheckboxes[inventoryIndex + 2].SetChecked((chr & 0x4) != 0);
                    inventoryCheckboxes[inventoryIndex + 3].SetChecked((chr & 0x8) != 0);
                    inventoryCheckboxes[inventoryIndex + 4].SetChecked((chr & 0x10) != 0);
                    inventoryChrIndex++;
                    inventoryIndex += 5;
                }
                {
                    int chr = CharacterCode.CharToIndex(inventory[inventoryChrIndex]);
                    inventoryCheckboxes[inventoryIndex + 0].SetChecked((chr & 0x1) != 0);
                    inventoryCheckboxes[inventoryIndex + 1].SetChecked((chr & 0x2) != 0);
                    inventoryCheckboxes[inventoryIndex + 2].SetChecked((chr & 0x4) != 0);
                    inventoryCheckboxes[inventoryIndex + 3].SetChecked((chr & 0x8) != 0);
                    inventoryChrIndex++;
                    inventoryIndex += 4;
                }
            }
#endif
        }

        void UpdateCharactersUI()
        {
                       
            foreach (var c in characterUIs)
            {
                char[] code = passwordView.GetCharacterCode(c.PasswordStartIndex);
                c.UpdateUI(code);
            }
        }

        private void AllItemsButton_Click(object sender, EventArgs e)
        {
#if USE_CHECKBOXES
            for(int i=0; i < 54; ++i)
            {
                inventoryCheckboxes[i].SetChecked(true);
            }
#endif
        }

        private void NoItemsButton_Click(object sender, EventArgs e)
        {
#if USE_CHECKBOXES
            for (int i = 0; i < 54; ++i)
            {
                inventoryCheckboxes[i].SetChecked(false);
            }
#endif
        }

        private void LocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            passwordView.UpdatePassword_LocationIndex(locationComboBox.SelectedIndex);

            checksum.UpdateButtonState(passwordView.IsValidChecksum());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return true;
        }
    }
}
