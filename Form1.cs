#define USE_CHECKBOXES

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#if USE_CHECKBOXES
class InventoryCheckBox : CheckBox
{
    static string[] allItemNames = new string[]
    {
            "Tomb Key",
            "Moria Key",
            "Red Gateway Gem",
            "Elvish Book",
            "Magic Rock",
            "Bottle",
            "Lost Amulet",
            "Maggot Note",
            "Scroll Of Floi",

            "Gate Key",
            "Moria Key",
            "Yellow Gateway Gem",
            "Book Of The Ages",
            "Gold Piece",
            "Jug Of Honey",
            "Lost Amulet",
            "Old Willow Note",
            "Scroll Of Oin",

            "Tomb Key",
            "Moria Key",
            "Gateway Keystone",
            "Book Of Mazarbul",
            "Gold Pieces",
            "Eye Glasses",
            "Lost Amulet",
            "Note From Gandalf",
            "Color Scoll",

            "Tomb Key",
            "Moria Key",
            "Green Gateway Gem",
            "Bilbo Diary",
            "Gold Pieces",
            "Healing Moss",
            "Lost Amulet",
            "Letter To Elrond",
            "Keystone Scroll",

            "Tomb Key",
            "Boat Oar",
            "Purple Gateway Gem",
            "Jeweled Ring",
            "Gold Pieces",
            "Athelas Major",
            "Lost Amulet",
            "Horn Of Boromir",
            "Long Bow",

            "Key To Bree",
            "Healing Mushroom",
            "Violet Gateway Gem",
            "The Ring",
            "Athelas Minor",
            "Healing Fruit",
            "Lost Amulet",
            "Magic Fern",
            "Orb of Drexle",
    };

    public void Initialize(int index)
    {
        int xPosition = index / 9;
        int yPosition = index % 9;

        this.AutoSize = true;
        this.Location = new System.Drawing.Point(9 + (127 * xPosition), 60 + (23 * yPosition));
        this.Name = $"checkBox{index}";
        this.Size = new System.Drawing.Size(74, 17);
        this.TabIndex = 2;
        this.Text = allItemNames[index];
        this.UseVisualStyleBackColor = true;
    }
}
#endif

namespace lotrpwcheck
{
    public partial class Form1 : Form
    {
        Bitmap baseImage;
        Bitmap caret;
        Bitmap glyphs;

        Coord caretTextPosition;

        char[] passwordString;

#if USE_CHECKBOXES
        InventoryCheckBox[] inventoryCheckboxes;
#endif

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            baseImage = new Bitmap("Base.png");
            caret = new Bitmap("caret.png");
            glyphs = new Bitmap("glyphs.png");

            passwordString = new char[48];
            for (int i=0; i<48; ++i)
            {
                passwordString[i] = '.';
            }

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
            locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

#if USE_CHECKBOXES
            inventoryCheckboxes = new InventoryCheckBox[54];
            for (int i=0; i<54; ++i)
            {
                inventoryCheckboxes[i] = new InventoryCheckBox();

                this.groupBox1.Controls.Add(inventoryCheckboxes[i]);

                inventoryCheckboxes[i].KeyUp += new System.Windows.Forms.KeyEventHandler(this.DefaultKeyUp_EnableKeyboardFocus);
                inventoryCheckboxes[i].CheckedChanged += OnInventoryCheckboxCheckChanged;
                inventoryCheckboxes[i].Initialize(i);
            }

            // The one ring can be legally encoded into the password. That being said, the bit is ignored by the game.
            inventoryCheckboxes[48].ForeColor = Color.Gray;
#endif

            UpdateReportingUI();
        }

        struct Coord
        {
            public int X;
            public int Y;
        }

        Coord TextPositionToScreenPosition(Coord textPosition)
        {
            Coord screenPos = new Coord();

            screenPos.X = 72;
            if (textPosition.X < 6)
            {
                screenPos.X += textPosition.X * 8;
            }
            else
            {
                screenPos.X += textPosition.X * 8 + 16;
            }

            screenPos.Y = 64;
            if (textPosition.Y < 3)
            {
                screenPos.Y += textPosition.Y * 24;
            }
            else
            {
                screenPos.Y = 160;
            }

            return screenPos;
        }

        Coord StringIndexToTextPosition(int stringIndex)
        {
            Coord result = new Coord();
            result.Y = stringIndex / 12;
            result.X = stringIndex % 12;
            return result;
        }

        int TextPositionToStringIndex(Coord textPosition)
        {
            return textPosition.Y * 12 + textPosition.X;
        }

        int CharToIndex(char ch)
        {
            int v;
            switch (ch)
            {
                case '.': v = 0; break;
                case 'B': v = 1; break;
                case 'C': v = 2; break;
                case 'D': v = 3; break;
                case 'F': v = 4; break;
                case 'G': v = 5; break;
                case 'H': v = 6; break;
                case 'J': v = 7; break;
                case 'K': v = 8; break;
                case 'L': v = 9; break;
                case 'M': v = 10; break;
                case 'N': v = 11; break;
                case 'P': v = 12; break;
                case 'Q': v = 13; break;
                case 'R': v = 14; break;
                case 'S': v = 15; break;
                case 'T': v = 16; break;
                case 'V': v = 17; break;
                case 'W': v = 18; break;
                case 'X': v = 19; break;
                case 'Y': v = 20; break;
                case 'Z': v = 21; break;
                case '0': v = 22; break;
                case '1': v = 23; break;
                case '2': v = 24; break;
                case '3': v = 25; break;
                case '4': v = 26; break;
                case '5': v = 27; break;
                case '6': v = 28; break;
                case '7': v = 29; break;
                case '8': v = 30; break;
                case '9': v = 31; break;

                case ' ': v = 0; break; // ignore
                case '-': v = 0; break; // ignore
                default:
                    System.Diagnostics.Debug.Fail("Unexpected value");
                    return -1;
            }
            return v;
        }
        
        char IndexToChar(int v)
        {
	        switch (v)
	        {
		        case 0: return '.';
		        case 1: return 'B';
		        case 2: return 'C';
		        case 3: return 'D';
		        case 4: return 'F';
		        case 5: return 'G';
		        case 6: return 'H';
		        case 7: return 'J';
		        case 8: return 'K';
		        case 9: return 'L';
		        case 10: return 'M';

		        case 11: return 'N';
		        case 12: return 'P';
		        case 13: return 'Q';
		        case 14: return 'R';
		        case 15: return 'S';
		        case 16: return 'T';
		        case 17: return 'V';
		        case 18: return 'W';
		        case 19: return 'X';
		        case 20: return 'Y';

		        case 21: return 'Z';
		        case 22: return '0';
		        case 23: return '1';
		        case 24: return '2';
		        case 25: return '3';
		        case 26: return '4';
		        case 27: return '5';
		        case 28: return '6';
		        case 29: return '7';
		        case 30: return '8';
		        case 31: return '9';

		        default:
                    System.Diagnostics.Debug.Fail("Unexpected value");
                    return '#';
	        }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.ScaleTransform(2, 2);
            e.Graphics.DrawImage(baseImage, 0, 0, 256, 224);

            for (int passwordStringIndex = 0; passwordStringIndex < 48; passwordStringIndex++)
            {
                Coord textPosition = StringIndexToTextPosition(passwordStringIndex);

                Coord textScreenPosition = TextPositionToScreenPosition(textPosition);
                Rectangle destRect = new Rectangle(textScreenPosition.X, textScreenPosition.Y, 8, 15);

                int index = CharToIndex(passwordString[passwordStringIndex]);
                Rectangle sourceRect = new Rectangle(index * 8 - 1, 0, 8, 15);

                e.Graphics.DrawImage(glyphs, destRect, sourceRect, GraphicsUnit.Pixel);
            }

            Coord caretScreenPosition = TextPositionToScreenPosition(caretTextPosition);
            // Adjust for caret image
            caretScreenPosition.X -= 16;
            caretScreenPosition.Y += 11;
            e.Graphics.DrawImage(caret, caretScreenPosition.X, caretScreenPosition.Y, 16, 16);
        }

        void HandleKeyPress(KeyEventArgs e)
        {
            bool invalidate = false;
            bool retreatCaret = false;
            bool advanceCaret = false;
            bool refreshUIState = false;

            switch (e.KeyCode)
            {
                case Keys.Right:
                    {
                        if (caretTextPosition.X < 12 - 1)
                        {
                            caretTextPosition.X++;
                            invalidate = true;
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        if (caretTextPosition.X > 0)
                        {
                            caretTextPosition.X--;
                            invalidate = true;
                        }
                        break;
                    }
                case Keys.Up:
                    {
                        if (caretTextPosition.Y > 0)
                        {
                            caretTextPosition.Y--;
                            invalidate = true;
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if (caretTextPosition.Y < 4 - 1)
                        {
                            caretTextPosition.Y++;
                            invalidate = true;
                        }
                        break;
                    }
                case Keys.OemPeriod:
                    {
                        int index = TextPositionToStringIndex(caretTextPosition);
                        passwordString[index] = '.';
                        advanceCaret = true;
                        invalidate = true;
                        refreshUIState = true;
                        break;
                    }

                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    {
                        int index = TextPositionToStringIndex(caretTextPosition);
                        passwordString[index] = e.KeyCode.ToString()[6];
                        advanceCaret = true;
                        invalidate = true;
                        refreshUIState = true;
                        break;
                    }

                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    {
                        int index = TextPositionToStringIndex(caretTextPosition);
                        passwordString[index] = e.KeyCode.ToString()[1];
                        advanceCaret = true;
                        invalidate = true;
                        refreshUIState = true;
                        break;
                    }

                case Keys.B:
                case Keys.C:
                case Keys.D:
                case Keys.F:
                case Keys.G:
                case Keys.H:
                case Keys.J:
                case Keys.K:
                case Keys.L:
                case Keys.M:
                case Keys.N:
                case Keys.P:
                case Keys.Q:
                case Keys.R:
                case Keys.S:
                case Keys.T:
                case Keys.V:
                case Keys.W:
                case Keys.X:
                case Keys.Y:
                case Keys.Z:
                    {
                        int index = TextPositionToStringIndex(caretTextPosition);
                        passwordString[index] = e.KeyCode.ToString()[0];
                        advanceCaret = true;
                        invalidate = true;
                        refreshUIState = true;
                        break;
                    }
                case Keys.Back:
                    {
                        int index = TextPositionToStringIndex(caretTextPosition);
                        passwordString[index] = '.';
                        retreatCaret = true;
                        invalidate = true;
                        refreshUIState = true;
                        break;
                    }
            }

            if (advanceCaret)
            {
                if (caretTextPosition.X < 12 - 1)
                {
                    caretTextPosition.X++;
                }
                else
                {
                    if (caretTextPosition.Y < 4 - 1)
                    {
                        caretTextPosition.X = 0;
                        caretTextPosition.Y++;
                    }
                }
            }

            if (retreatCaret)
            {
                if (caretTextPosition.X > 0)
                {
                    caretTextPosition.X--;
                }
                else
                {
                    if (caretTextPosition.Y > 0)
                    {
                        caretTextPosition.Y--;
                        caretTextPosition.X = 12 - 1;
                    }
                }
            }

            if (refreshUIState)
            {
                UpdateChecksumButtonStateImpl(IsValidChecksum());
                UpdateLocationTextAndDropdown();
                UpdateInventoryCheckboxes();
            }

            if (invalidate)
            {
                Invalidate();
            }
        }

        byte GetChecksumComponent(string passwordText)
        {
            byte result = 0;
            for (int i = 0; i < passwordText.Length; ++i)
            {
                byte v = (byte)CharToIndex(passwordText[i]);
                result += v;
            }

            return result;
        }

        struct ExpectedChecksum
        {
            public char ExpectedPartyChecksum;
            public char ExpectedEventChecksum;
            public char ExpectedInventoryChecksum;
        }

        ExpectedChecksum GetExpectedChecksum()
        {
            ExpectedChecksum result = new ExpectedChecksum();

            string str = new string(passwordString);
            string line1 = str.Substring(0, 12);
            string line2 = str.Substring(12, 12);
            string line3 = str.Substring(24, 12);
            string inventory = str.Substring(36, 12);

            byte checksum1 = GetChecksumComponent(line1);
            byte checksum2 = GetChecksumComponent(line2);
            int expectedPartyChecksum = (checksum1 + checksum2) % 32;
            result.ExpectedPartyChecksum = IndexToChar(expectedPartyChecksum);

            string eventInfo = line3.Substring(0, 9);
            int eventChecksum = GetChecksumComponent(eventInfo) % 32;
            result.ExpectedEventChecksum = IndexToChar(eventChecksum);

            int inventoryChecksum = GetChecksumComponent(inventory) % 32;
            result.ExpectedInventoryChecksum = IndexToChar(inventoryChecksum);

            return result;
        }

        bool IsValidChecksum()
        {
            string str = new string(passwordString);
            string line3 = str.Substring(24, 12);

            char partyCode = line3[9];
            char eventCode = line3[10];
            char inventoryCode = line3[11];

            var expectedChecksum = GetExpectedChecksum();

            if (partyCode != expectedChecksum.ExpectedPartyChecksum)
                return false;
            
            if (eventCode != expectedChecksum.ExpectedEventChecksum)
                return false;

            if (inventoryCode != expectedChecksum.ExpectedInventoryChecksum)
                return false;

            return true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
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

        private void ChecksumButton_Click(object sender, EventArgs e)
        {
            var expectedChecksum = GetExpectedChecksum();
            passwordString[24 + 9] = expectedChecksum.ExpectedPartyChecksum;
            passwordString[24 + 10] = expectedChecksum.ExpectedEventChecksum;
            passwordString[24 + 11] = expectedChecksum.ExpectedInventoryChecksum;
            Invalidate();

            bool isValidChecksum = IsValidChecksum();
            System.Diagnostics.Debug.Assert(isValidChecksum);
            UpdateChecksumButtonStateImpl(isValidChecksum);
        }

        void UpdateChecksumButtonStateImpl(bool isValidChecksum)
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

        void UpdateLocationTextAndDropdown()
        {
            string str = new string(passwordString);
            string line3 = str.Substring(24, 12);
            char locationCodeCharacter = line3[0];

            int locationCode = CharToIndex(locationCodeCharacter);
            locationComboBox.SelectedIndex = locationCode;
        }

        void UpdateInventoryCheckboxes()
        {
#if USE_CHECKBOXES
            string str = new string(passwordString);
            string inventory = str.Substring(36, 12);

            int inventoryIndex = 0;
            int inventoryChrIndex = 0;
            for (int column = 0; column < 6; column++)
            {
                {
                    int chr = CharToIndex(inventory[inventoryChrIndex]);
                    inventoryCheckboxes[inventoryIndex + 0].Checked = (chr & 0x1) != 0;
                    inventoryCheckboxes[inventoryIndex + 1].Checked = (chr & 0x2) != 0;
                    inventoryCheckboxes[inventoryIndex + 2].Checked = (chr & 0x4) != 0;
                    inventoryCheckboxes[inventoryIndex + 3].Checked = (chr & 0x8) != 0;
                    inventoryCheckboxes[inventoryIndex + 4].Checked = (chr & 0x10) != 0;
                    inventoryChrIndex++;
                    inventoryIndex += 5;
                }
                {
                    int chr = CharToIndex(inventory[inventoryChrIndex]);
                    inventoryCheckboxes[inventoryIndex + 0].Checked = (chr & 0x1) != 0;
                    inventoryCheckboxes[inventoryIndex + 1].Checked = (chr & 0x2) != 0;
                    inventoryCheckboxes[inventoryIndex + 2].Checked = (chr & 0x4) != 0;
                    inventoryCheckboxes[inventoryIndex + 3].Checked = (chr & 0x8) != 0;
                    inventoryChrIndex++;
                    inventoryIndex += 4;
                }
            }
#endif
        }

        private void OnInventoryCheckboxCheckChanged(object sender, EventArgs e)
        {
#if USE_CHECKBOXES
            // Update password based on the checkbox UI
            int inventoryCheckboxIndex = 0;
            int passwordStringIndex = 36;
            for (int column = 0; column < 6; column++)
            {
                {
                    int l0 = 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 0].Checked ? 0x1 : 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 1].Checked ? 0x2 : 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 2].Checked ? 0x4 : 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 3].Checked ? 0x8 : 0;
                    l0 |= inventoryCheckboxes[inventoryCheckboxIndex + 4].Checked ? 0x10 : 0;
                    passwordString[passwordStringIndex] = IndexToChar(l0);
                    passwordStringIndex++;
                    inventoryCheckboxIndex += 5;
                }
                {
                    int l1 = 0;
                    l1 |= inventoryCheckboxes[inventoryCheckboxIndex + 0].Checked ? 0x1 : 0;
                    l1 |= inventoryCheckboxes[inventoryCheckboxIndex + 1].Checked ? 0x2 : 0;
                    l1 |= inventoryCheckboxes[inventoryCheckboxIndex + 2].Checked ? 0x4 : 0;
                    l1 |= inventoryCheckboxes[inventoryCheckboxIndex + 3].Checked ? 0x8 : 0;

                    passwordString[passwordStringIndex] = IndexToChar(l1);
                    passwordStringIndex++;
                    inventoryCheckboxIndex += 4;
                }
            }

            UpdateChecksumButtonStateImpl(IsValidChecksum());
            Invalidate();
#endif
        }

        void UpdateReportingUI()
        {
            UpdateChecksumButtonStateImpl(IsValidChecksum());
            UpdateLocationTextAndDropdown();
            UpdateInventoryCheckboxes();
        }

        private void AllItemsButton_Click(object sender, EventArgs e)
        {
#if USE_CHECKBOXES
            for(int i=0; i < 54; ++i)
            {
                inventoryCheckboxes[i].Checked = true;
            }
#endif
        }

        private void NoItemsButton_Click(object sender, EventArgs e)
        {
#if USE_CHECKBOXES
            for (int i = 0; i < 54; ++i)
            {
                inventoryCheckboxes[i].Checked = false;
            }
#endif
        }

        private void LocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int locationIndex = locationComboBox.SelectedIndex;
            char characterCode = IndexToChar(locationIndex);
            passwordString[24] = characterCode;

            UpdateChecksumButtonStateImpl(IsValidChecksum());
            Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return true;
        }
    }
}
