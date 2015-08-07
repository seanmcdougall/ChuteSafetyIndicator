# Chute Safety Indicator
Plugin for Kerbal Space Program (KSP).  Changes the background color of each parachute's staging icon to let you know whether or not it's safe to deploy.

Copyright 2015, Sean McDougall

Forum Thread: http://forum.kerbalspaceprogram.com/threads/129154

## DOWNLOAD

GitHub: https://github.com/seanmcdougall/ChuteSafetyIndicator

KerbalStuff: http://www.kerbalstuff.com/mod/1012/Chute%20Safety%20Indicator

CKAN: http://forum.kerbalspaceprogram.com/threads/100067

Curse: http://www.curse.com/ksp-mods/kerbal/232908-chute-safety-indicator

## INSTALLATION
Copy the ChuteSafetyIndicator folder and all its contents into your KSP GameData folder.

## INSTRUCTIONS
This plugin will change the background color of each parachute's staging icon based on the current safety status.

Safe: green
Risky: yellow
Unsafe: red

## ADVANCED CONFIGURATION
These default colors can be modified by editing the ChuteSafetyIndicator.cfg file inside GameData/ChuteSafetyIndicator/.  This file will be automatically created after installing this mod
and running KSP with it for the first time.

The default looks like this:

  Settings
  {
      safeColor = 0,1,0,1
      riskyColor = 1,0.9215686,0.01568628,1
      unSafeColor = 1,0,0,1
      clearBackground = False
  }

The three colors are stored in Unity format (see http://docs.unity3d.com/ScriptReference/Color.html).  Each component is a floating point number between 0 and 1.  In order, they denote the red,
green, blue, and alpha (transparency) channels.  

Setting "clearBackground" to "True" will remove the default grey gradient background that's present on the staging icons.  Doing this will make it easier for color blind users to find a workable set of
colors, since the background texture will no longer interfere.