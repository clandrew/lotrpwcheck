# J.R.R. Tolkien's The Lord of the Rings: Volume 1 (SNES) Password Checker

## About
This is a tool for verifying whether a password for the game "J.R.R. Tolkien's The Lord of the Rings: Volume 1" is valid or not. You can also edit passwords. This came about when I was playing the game and found that some of the passwords I had written down were correctly written down, but glitched. This program fell out of trying to understand the glitched parts more, do some reverse-engineering and ultimately work around them. I'm putting this online in case other people run into the same issues.

I wrote a blog post describing the password format, [here](http://cml-a.com/content/2021/03/31/lord-of-the-rings-snes-password-format/).

## Notes
* You can type whatever password you want, then click the "Fix Checksum" button to auto-insert a good checksum.

* The combo box shows where in the game world the password will spawn you.
  * You can edit it, and the password is updated automatically.

* The checkboxes with items show your current inventory.
  * You can check or uncheck the boxes to change the inventory, and the password is updated automatically.
  * The buttons "All Items" and "No Items" are there as a convenience to fill or clear your inventory.
  * Note: the One Ring is always in Frodo's inventory, regardless of whether you flag it as enabled through the password.
  
* There's controls for each character's level, armor and weapons.
  * You can modify the number field for the level, or use the drop downs to change equipment.
  * STR and DEF in the game is computed based on level and equipment.
  * There's a checkbox "Enabled" for each character, indicating that there is something in the password which adds that character to your party.
  * Some choices for armor or weapon are "invalid" meaning the password will be rejected by the game; others are marked "corruption" meaning the password is acceptable but causes misbehavior in the game.
  * In the same way the One Ring is always in your inventory, one of Frodo or Aragorn is always in your party even if neither are explicitly set through the password.
  
* You use the keyboard to type passwords + arrow keys to navigate between characters, so it's a bit quicker as compared to the game.
  
* This program will let you get a password with a good checksum. That being said, the game will reject some passwords for reasons other than the checksum. For example, if you specify a location code beyond the allowed bounds- these yield "(invalid)" in the dropdown. Since there are cases (described below) where you may want to specify an invalid password, the program doesn't block you from forming them.

* The choice of whether a character is player-controlled or AI-controlled is not encoded in passwords. Rather it's based off of whether you have a controller plugged into the port.

* If you enter an only-partially-valid password that includes some configuration for a character (e.g., the string is not all '.'), it permanently adds the character to your party until SNES reset. Even if the password is rejected. This leads to a well-known cheesy trick where you <details> <summary>Glitch spoiler</summary>
can [enter a bad password](https://www.gamespot.com/j-r-r-tolkiens-the-lord-of-the-rings-volume-1/cheats/), press start and hear the "invalid password" noise, then delete it and start the game with all the Fellowship unlocked.
</details>
 
* The last two location codes, 'Moria 1 (code M)' and 'Moria 2 (code N)' are glitched in that the game provides them to you but won't ever accept them. 

There are at least a couple layers of rejcting these codes.

The first is here
```
// Function: ReadPasswordLocationCode()
// Precondition: location code is stored at $81:039C
// Postcondition: stores some location-specific information in 801CCB, 801CCD,	801CC9
//     This includes area numbers.
//     Earlygame areas are high-numbered area numbers. Late-game are low-numbered.
//     For example, Crossroads is 0x12A. Rivendell is 0x138. The Moria Entrance is 0xEF. Moria 1 is 0x51. And Moria 2 is 0x0C.

$81/CBEA B9 84 03    LDA $0384,y[$81:039C]   A:FFFF X:FFFF Y:0018 P:envmxdiZC	; Load location code. E.g., the password character 'M', which is 0xA
$81/CBED 29 1F 00    AND #$001F              A:000A X:FFFF Y:0018 P:envmxdizC
$81/CBF0 C9 0A 00    CMP #$000A              A:000A X:FFFF Y:0018 P:envmxdizC	; 										
$81/CBF3 90 02       BCC $02    [$CBF7]      A:000A X:FFFF Y:0018 P:envmxdiZC	; If the password character is equal or creater than 'M' (0xA), fall through 
                                                                              ; to LocationCodeTooHigh. Otherwise, goto LocationCodeOk.

LocationCodeTooHigh:
$81/CBF5 38          SEC                     A:000A X:FFFF Y:0018 P:envmxdiZC
$81/CBF6 6B          RTL                     A:000A X:FFFF Y:0018 P:envmxdiZC	; Bail

LocationCodeOk:
$81/CBF7 85 90       STA $90    [$00:0090]   A:0009 X:FFFF Y:0018 P:eNvmxdizc
$81/CBF9 A5 90       LDA $90    [$00:0090]   A:0009 X:FFFF Y:0018 P:eNvmxdizc
$81/CBFB 0A          ASL A                   A:0009 X:FFFF Y:0018 P:envmxdizc
$81/CBFC AA          TAX                     A:0012 X:FFFF Y:0018 P:envmxdizc

// Store the corresponding codes in the output
$81/CBFD BF 18 CC 81 LDA $81CC18,x[$81:CC2A] A:0012 X:0012 Y:0018 P:envmxdizc
$81/CC01 8F CB 1C 80 STA $801CCB[$80:1CCB]   A:0140 X:0012 Y:0018 P:envmxdizc
$81/CC05 BF 30 CC 81 LDA $81CC30,x[$81:CC42] A:0140 X:0012 Y:0018 P:envmxdizc
$81/CC09 8F CD 1C 80 STA $801CCD[$80:1CCD]   A:00A0 X:0012 Y:0018 P:envmxdizc
$81/CC0D BF 48 CC 81 LDA $81CC48,x[$81:CC5A] A:00A0 X:0012 Y:0018 P:envmxdizc
$81/CC11 8F C9 1C 80 STA $801CC9[$80:1CC9]   A:00EF X:0012 Y:0018 P:envmxdizc

$81/CC15 C8          INY                     A:00EF X:0012 Y:0018 P:envmxdizc
$81/CC16 18          CLC                     A:00EF X:0012 Y:0019 P:envmxdizc
$81/CC17 6B          RTL                     A:00EF X:0012 Y:0019 P:envmxdizc
```

If the game is somehow hacked to get past that, there's another place where Moria 1 and 2 are locked out
```
Function: AreaLoadImpl2()
Preconditions: Location codes have been written to 801CCB, 801CCD,	801CC9

$81/A377 E2 30       SEP #$30                A:0091 X:0006 Y:0054 P:eNvmxdizc
$81/A379 AF 0E 1D 80 LDA $801D0E[$80:1D0E]   A:0091 X:0006 Y:0054 P:eNvMXdizc
$81/A37D CF 72 03 80 CMP $800372[$80:0372]   A:000A X:0006 Y:0054 P:envMXdizc
$81/A381 F0 76       BEQ $76    [$A3F9]      A:000A X:0006 Y:0054 P:envMXdizC
$81/A383 AF 72 03 80 LDA $800372[$80:0372]   A:000A X:0006 Y:0054 P:envMXdizC
$81/A387 30 70       BMI $70    [$A3F9]      A:0002 X:0006 Y:0054 P:envMXdizC
$81/A389 C2 20       REP #$20                A:0002 X:0006 Y:0054 P:envMXdizC
$81/A38B AF C5 1C 80 LDA $801CC5[$80:1CC5]   A:0002 X:0006 Y:0054 P:envmXdizC	; Load the area number.
										
$81/A38F C9 54 00    CMP #$0054              A:0051 X:0006 Y:0054 P:envmXdizC	; 
$81/A392 B0 52       BCS $52    [$A3E6]      A:0051 X:0006 Y:0054 P:eNvmXdizc	; If area number < 54, fall through to InvalidAreaNumber_TooLow. Otherwise, goto ValidAreaNumber.

InvalidAreaNumber_TooLow:
$81/A394 E2 20       SEP #$20                A:0051 X:0006 Y:0054 P:eNvmXdizc
$81/A396 AF 0E 1D 80 LDA $801D0E[$80:1D0E]   A:0051 X:0006 Y:0054 P:eNvMXdizc
$81/A39A C9 04       CMP #$04                A:000A X:0006 Y:0054 P:envMXdizc
$81/A39C D0 0E       BNE $0E    [$A3AC]      A:000A X:0006 Y:0054 P:envMXdizC
$81/A3AC E2 20       SEP #$20                A:000A X:0006 Y:0054 P:envMXdizC
$81/A3AE A9 00       LDA #$00                A:000A X:0006 Y:0054 P:envMXdizC
$81/A3B0 48          PHA                     A:0000 X:0006 Y:0054 P:envMXdiZC
$81/A3B1 AF 72 03 80 LDA $800372[$80:0372]   A:0000 X:0006 Y:0054 P:envMXdiZC
$81/A3B5 48          PHA                     A:0002 X:0006 Y:0054 P:envMXdizC
$81/A3B6 F4 06 00    PEA $0006               A:0002 X:0006 Y:0054 P:envMXdizC
$81/A3B9 22 02 80 81 JSL $818002[$81:8002]   A:0002 X:0006 Y:0054 P:envMXdizC
$81/A3BD 85 34       STA $34    [$00:0034]   A:FFFF X:00FF Y:00FF P:eNvMXdizc	; Fall through into BadLoop

BadLoop:
$81/A3BF A9 00       LDA #$00                A:FFFF X:00FF Y:00FF P:eNvMXdizc	
$81/A3C1 48          PHA                     A:FF00 X:00FF Y:00FF P:envMXdiZc
$81/A3C2 AF 72 03 80 LDA $800372[$80:0372]   A:FF00 X:00FF Y:00FF P:envMXdiZc
$81/A3C6 48          PHA                     A:FF02 X:00FF Y:00FF P:envMXdizc
$81/A3C7 F4 06 00    PEA $0006               A:FF02 X:00FF Y:00FF P:envMXdizc
$81/A3CA 22 02 80 81 JSL $818002[$81:8002]   A:FF02 X:00FF Y:00FF P:envMXdizc
$81/A3CE C5 34       CMP $34    [$00:0034]   A:FFFF X:00FF Y:00FF P:eNvMXdizc
$81/A3D0 F0 ED       BEQ $ED    [$A3BF]      A:FFFF X:00FF Y:00FF P:envMXdiZC
// This hangs forever :(

ValidAreaIndex:
$81/A3E6 E2 20       SEP #$20                A:00EF X:0002 Y:001C P:envmXdizC		
$81/A3E8 A9 00       LDA #$00                A:00EF X:0002 Y:001C P:envMXdizC
... clipped for brevity
```

When you play the game you suspect that it rejects Moria 1 and Moria 2. This code proves it.

* Ok so. Going from this- when you request a password, the game uses your 'furthest location code'. If you have been through Moria 1 (code M), your password will always have that glitched location code M. Even if you backtrack to the Moria Entrance (L). Even if you backtrack to the beginning of the game. This is a very serious problem. Fortunately the save state editor comes in handy for that situation :)

Preview:
![Example image](https://raw.githubusercontent.com/clandrew/lotrpwcheck/master/Images/Usage.gif "Example image.")

## Build
The program is organized as a Visual Studio 2019 solution, written in C#. The program runs on x86-64 architecture on a Windows OS environment. It was tested on Windows 7 and 10 and may work on other versions. It uses Windows Forms and .NET 4.7.2. The UI for entering the password is dressed up to look a bit like the game, just for fun. Those parts use GDI+.
