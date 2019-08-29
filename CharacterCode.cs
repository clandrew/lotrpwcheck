using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lotrpwcheck
{
    static class CharacterCode
    {
        static public int CharToIndex(char ch)
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

        static public char IndexToChar(int v)
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
    }
}
