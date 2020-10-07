## Memory Cleaner
![Memory Cleaner](https://cdn.discordapp.com/attachments/759162962325143623/763515874501984317/unknown.png)
## Description
Real-time application that displays your current available amount of RAM and your current timer resolution in milliseconds, lets you set a custom timer resolution and clear the standby list with a custom hotkey. Two versions of this app exist, the "mini" version that's really light and the bit heavier one that's the normal version with a GUI. It stores the settings in registry in the following path "HKEY_CURRENT_USER\Software\Memory Cleaner\Settings".
## Usage
To clean the standby list, just press the specificed hotkey. The range for the hotkeys is currently F1 - F12, although F12 doesn't work due to it being a debug hotkey. To change the hotkey, use the GUI and click "Save Settings". For the mini version, open regedit and navigate to this path: "HKCU\Software\Memory Cleaner\Settings" and change the "Hotkey" value to anything that's between F1 and F11.

To set a custom timer resolution, press "Start" in the normal version. In the mini version, it already sets the custom timer resolution on startup. To change the desired timer resolution value, open regedit and navigate to this path: "HKCU\Software\Memory Cleaner\Settings" and change the "DesiredTimerRes" value to whatever you desire. This is measured in microseconds divided by 10, so for example 0.5ms equals 5000 and 1ms equals 10000.

Restart the program in order for the changes to apply.
