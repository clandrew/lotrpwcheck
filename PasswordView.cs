using System;
using System.Drawing;
using System.Windows.Forms;

namespace lotrpwcheck
{
    class PasswordView
    {
        Coord caretTextPosition;
        char[] passwordString;
        Bitmap baseImage;
        Bitmap caret;
        Bitmap glyphs;
        Button passwordUI;

        public PasswordView(Button p)
        {
            passwordUI = p;
            baseImage = new Bitmap("Images/Base.png");
            caret = new Bitmap("Images/caret.png");
            glyphs = new Bitmap("Images/glyphs.png");

            passwordString = new char[48];
            for (int i = 0; i < 48; ++i)
            {
                passwordString[i] = '.';
            }
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

        public void Paint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.ScaleTransform(2, 2);
            e.Graphics.DrawImage(baseImage, 0, 0, 256, 224);

            for (int passwordStringIndex = 0; passwordStringIndex < 48; passwordStringIndex++)
            {
                Coord textPosition = StringIndexToTextPosition(passwordStringIndex);

                Coord textScreenPosition = TextPositionToScreenPosition(textPosition);
                Rectangle destRect = new Rectangle(textScreenPosition.X, textScreenPosition.Y, 8, 15);

                int index = CharacterCode.CharToIndex(passwordString[passwordStringIndex]);
                float awfulFudgeFactor = 0.5f;
                RectangleF sourceRect = new RectangleF(index * 8, -1, 8 - awfulFudgeFactor, 16);

                e.Graphics.DrawImage(glyphs, destRect, sourceRect, GraphicsUnit.Point);
            }

            Coord caretScreenPosition = TextPositionToScreenPosition(caretTextPosition);
            // Adjust for caret image
            caretScreenPosition.X -= 16;
            caretScreenPosition.Y += 11;
            e.Graphics.DrawImage(caret, caretScreenPosition.X, caretScreenPosition.Y, 16, 16);
        }

        public struct KeyPressResult
        {
            public bool ShouldRecomputeChecksum;
            public bool ShouldRefreshLocationInventoryAndCharacters;
        }

        public KeyPressResult HandleKeyPress(KeyEventArgs e)
        {
            KeyPressResult r = new KeyPressResult();

            bool retreatCaret = false;
            bool advanceCaret = false;
            bool invalidate = false;

            switch (e.KeyCode)
            {
                case Keys.Right:
                    {
                        advanceCaret = true;
                        invalidate = true;
                        break;
                    }
                case Keys.Left:
                    {
                        retreatCaret = true;
                        invalidate = true;
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
                        r.ShouldRefreshLocationInventoryAndCharacters = true;
                        advanceCaret = true;
                        invalidate = true;
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
                        r.ShouldRefreshLocationInventoryAndCharacters = true;
                        advanceCaret = true;
                        invalidate = true;
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
                        r.ShouldRefreshLocationInventoryAndCharacters = true;
                        advanceCaret = true;
                        invalidate = true;
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
                        r.ShouldRefreshLocationInventoryAndCharacters = true;
                        advanceCaret = true;
                        invalidate = true;
                        break;
                    }
                case Keys.Back:
                    {
                        int index = TextPositionToStringIndex(caretTextPosition);
                        passwordString[index] = '.';
                        r.ShouldRefreshLocationInventoryAndCharacters = true;
                        retreatCaret = true;
                        invalidate = true;
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

            if (r.ShouldRefreshLocationInventoryAndCharacters)
            {
                passwordUI.Invalidate();
                r.ShouldRecomputeChecksum = true;
            }
            else if (invalidate)
            {
                passwordUI.Invalidate();
            }

            return r;
        }


        struct ExpectedChecksum
        {
            public char ExpectedPartyChecksum;
            public char ExpectedEventChecksum;
            public char ExpectedInventoryChecksum;
        }

        byte GetChecksumComponent(string passwordText)
        {
            byte result = 0;
            for (int i = 0; i < passwordText.Length; ++i)
            {
                byte v = (byte)CharacterCode.CharToIndex(passwordText[i]);
                result += v;
            }

            return result;
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
            result.ExpectedPartyChecksum = CharacterCode.IndexToChar(expectedPartyChecksum);

            string eventInfo = line3.Substring(0, 9);
            int eventChecksum = GetChecksumComponent(eventInfo) % 32;
            result.ExpectedEventChecksum = CharacterCode.IndexToChar(eventChecksum);

            int inventoryChecksum = GetChecksumComponent(inventory) % 32;
            result.ExpectedInventoryChecksum = CharacterCode.IndexToChar(inventoryChecksum);

            return result;
        }

        public bool IsValidChecksum()
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
        public void UpdatePassword_LocationIndex(int locationIndex)
        {
            char characterCode = CharacterCode.IndexToChar(locationIndex);
            passwordString[24] = characterCode;
            passwordUI.Invalidate();
        }

        public void UpdatePassword_Inventory(char[] codes)
        {
            System.Diagnostics.Debug.Assert(codes.Length == 12);
            for(int i=0; i<12; ++i)
            {
                passwordString[36 + i] = codes[i];
            }
            passwordUI.Invalidate();
        }


        public void UpdatePassword_Lines1And2(bool enabled, int desiredLevel, int weaponIndex, int armorIndex, int passwordStartIndex)
        {
            if (weaponIndex == -1 || armorIndex == -1)
                return; // Not finished loading

            if (weaponIndex == (int)Names.Weapon.Invalid || armorIndex == (int)Names.Armor.Invalid)
            {
                // Choose some arbitrary invalid sequence
                passwordString[passwordStartIndex + 0] = '.';
                passwordString[passwordStartIndex + 1] = '.';
                passwordString[passwordStartIndex + 2] = 'B';
                passwordUI.Invalidate();
                return;
            }

            int code1 = 0;
            int code2 = 0;
            int code3 = 0;

            if (enabled)
            {
                int multipleOf20 = desiredLevel / 20;
                int remainderOf20 = desiredLevel % 20;
                System.Diagnostics.Debug.Assert(multipleOf20 >= 0 && multipleOf20 <= 4);

                int levelEncode = remainderOf20;
                if (remainderOf20 >= 10)
                    levelEncode += 6;
                code1 = levelEncode;

                int[] armorEncodeList2 = { 0, 0, 1, 1, 2, 3 };
                int armorEncode2 = armorEncodeList2[armorIndex];
                code2 = multipleOf20 + (armorEncode2 * 8);

                int weaponEncode = weaponIndex;
                int[] armorEncodeList3 = { 0, 1, 0, 1, 0, 0 };
                int armorEncode3 = armorEncodeList3[armorIndex];
                code3 = 16 + (weaponEncode * 2) + armorEncode3;
            }

            passwordString[passwordStartIndex + 0] = CharacterCode.IndexToChar(code1);
            passwordString[passwordStartIndex + 1] = CharacterCode.IndexToChar(code2);
            passwordString[passwordStartIndex + 2] = CharacterCode.IndexToChar(code3);

            passwordUI.Invalidate();
        }

        public int GetLocationCode()
        {
            string str = new string(passwordString);
            string line3 = str.Substring(24, 12);
            char locationCodeCharacter = line3[0];

            return CharacterCode.CharToIndex(locationCodeCharacter);
        }

        public void FixChecksum()
        {
            var expectedChecksum = GetExpectedChecksum();
            passwordString[24 + 9] = expectedChecksum.ExpectedPartyChecksum;
            passwordString[24 + 10] = expectedChecksum.ExpectedEventChecksum;
            passwordString[24 + 11] = expectedChecksum.ExpectedInventoryChecksum;
            passwordUI.Invalidate();

            System.Diagnostics.Debug.Assert(IsValidChecksum());
        }

        public string GetInventoryCharacterCodes()
        {
            string str = new string(passwordString);
            string inventory = str.Substring(36, 12);
            return inventory;
        }

        public char[] GetCharacterCode(int offset)
        {
            char[] code = new char[3];
            code[0] = passwordString[offset + 0];
            code[1] = passwordString[offset + 1];
            code[2] = passwordString[offset + 2];
            return code;
        }
    }
}
