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

## What it looks like
![Example image](https://raw.githubusercontent.com/clandrew/lotrpwcheck/master/Images/Usage.gif "Example image.")


## Glitched Passwords
If you’re in Moria past the entrance and the first part and request a password, the game will give you one. However, if you write down and try to use that same password later, the game won’t accept it.

The last two location codes, 'Moria 1 (code M)' and 'Moria 2 (code N)' are glitched in that the game provides them to you but won't ever accept them. This is especially bad since backtracking to the Moria entrance or even to the start of the game won't get you valid passwords again.

If you're on console with no cheat device, you can use this program to fix your password to 'Moria entrance (code L)'. It's a bit of a hassle since you still need to go through Moria, but at least you can keep your levels, equipment, items, and doors that were unlocked stay unlocked.

If you're on console or you have a Pro Action Replay device, you can fix the bug with the codes
```
81CBF10C
81A3900C
81A35C0C
```
Those codes patch the game code to fix the bug and allow the game to accept Moria passwords. They don't require game reset.

I wrote a [blog post](http://cml-a.com/content/2021/04/06/lord-of-the-rings-snes-bugfix/) explaining the bug and where the fix comes from.

## Build
The program is organized as a Visual Studio 2019 solution, written in C#. The program runs on x86-64 architecture on a Windows OS environment. It was tested on Windows 7 and 10 and may work on other versions. It uses Windows Forms and .NET 4.7.2. The UI for entering the password is dressed up to look a bit like the game, just for fun. Those parts use GDI+.
