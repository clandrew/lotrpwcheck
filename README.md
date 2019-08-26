# J.R.R. Tolkien's The Lord of the Rings: Volume 1 (SNES) Password Checker

## About
This is a tool for verifying whether a password for the game "J.R.R. Tolkien's The Lord of the Rings: Volume 1" is valid or not. This came about when I was playing the game and found that some of the passwords I had written down were correctly written down, but glitched. This program fell out of trying to understand the glitched parts more, do some reverse-engineering and ultimately work around them. I'm putting this online in case other people run into the same issues.

## Notes
* You can type whatever password you want, then click the "Fix Checksum" button to auto-insert a good checksum.
* The combo box shows where in the game world the password will spawn you.
  * You can edit it, and the password is updated automatically.
* The checkboxes show your current inventory.
  * You can check or uncheck the boxes to change the inventory, and the password is updated automatically.
  * The buttons "All Items" and "No Items" are there as a convenience to fill or clear your inventory.
  * Note: the One Ring is always in Frodo's inventory, regardless of whether you flag it as enabled through the password.
  
* This program will ensure you get a password with a good checksum. That being said, the game will reject some passwords for reasons other than the checksum. For example, if you specify a location code beyond the allowed bounds- these yield "(invalid)" in the dropdown.

* The choice of whether a character is player-controlled or AI-controlled is not encoded in passwords. It looks to be based off of whether you have a controller plugged into the port.

* If you enter an only-partially-valid password that includes some configuration for a character (e.g., the string is not all '.'), it permanently adds the character to your party until SNES reset. Even if the password is rejected. This leads to a well-known cheesy trick where you can [enter a bad password](https://www.gamespot.com/j-r-r-tolkiens-the-lord-of-the-rings-volume-1/cheats/), press start and hear the "invalid password" noise, then delete it and start the game with all the Fellowship unlocked.

![Example image](https://raw.githubusercontent.com/clandrew/lotrpwcheck/master/Images/Usage.gif "Example image.")

## Build
The program is organized as a Visual Studio 2019 solution, written in C#. The program runs on x86-64 architecture. It uses Windows Forms and .NET 4.7.2. The UI for entering the password is dressed up to look a bit like the game, just for fun. Those parts use GDI+.
