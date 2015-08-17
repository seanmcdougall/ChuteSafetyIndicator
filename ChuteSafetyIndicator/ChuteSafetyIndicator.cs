// 
//     Chute Safety Indicator
// 
//     Copyright (C) 2015 Sean McDougall
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChuteSafetyIndicator
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ChuteSafetyIndicator : MonoBehaviour
    {
        internal Settings settings = SettingsWrapper.instance.gameSettings;
        
        internal Texture2D origTexture = null;

        internal SettingsWindow settingsWindow = null;

        internal bool visibleUI = true;

        internal void Awake()
        {
            if (settings.useStockToolbar)
            {
                GameEvents.onGUIApplicationLauncherReady.Add(OnGUIApplicationLauncherReady);
            }

            GameEvents.onGameSceneLoadRequested.Add(onSceneChange);
        }

        internal void Start()
        {
            // Add hooks for showing/hiding on F2
            GameEvents.onShowUI.Add(showUI);
            GameEvents.onHideUI.Add(hideUI);

            settingsWindow = new SettingsWindow();
            addLauncherButtons();
        }

        // Remove the launcher button when the scene changes
        internal void onSceneChange(GameScenes scene)
        {
            removeLauncherButtons();
        }

        internal void showUI() // triggered on F2
        {
            visibleUI = true;
        }

        internal void hideUI() // triggered on F2
        {
            visibleUI = false;
        }

        internal void OnGUIApplicationLauncherReady()
        {
            if (settingsWindow.launcherButton == null && settings.useStockToolbar)
            {
                settingsWindow.launcherButton = ApplicationLauncher.Instance.AddModApplication(showWindow, hideWindow, null, null, null, null, ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.MAPVIEW, SettingsWrapper.Instance.modStyle.GetImage("ChuteSafetyIndicator/textures/toolbar", 38, 38));
            }
        }

        internal void addLauncherButtons()
        {
            // Load Blizzy toolbar
            if (settingsWindow.blizzyButton == null)
            {
                if (ToolbarManager.ToolbarAvailable)
                {
                    // Create button
                    settingsWindow.blizzyButton = ToolbarManager.Instance.add("ChuteSafetyIndicator", "blizzyButton");
                    settingsWindow.blizzyButton.TexturePath = "ChuteSafetyIndicator/textures/blizzyToolbar";
                    settingsWindow.blizzyButton.ToolTip = "Chute Safety Indicator";
                    settingsWindow.blizzyButton.OnClick += (e) => toggleWindow();
                }
                else
                {
                    // Blizzy Toolbar not available, fall back to stock launcher
                    settings.useStockToolbar = true;
                }
            }

            // Load Application Launcher
            if (settingsWindow.launcherButton == null && settings.useStockToolbar)
            {
                OnGUIApplicationLauncherReady();
            }
        }

        internal void removeLauncherButtons()
        {
            if (settingsWindow.launcherButton != null)
            {
                removeApplicationLauncher();
            }
            if (settingsWindow.blizzyButton != null)
            {
                settingsWindow.blizzyButton.Destroy();
            }
        }

        internal void removeApplicationLauncher()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIApplicationLauncherReady);
            ApplicationLauncher.Instance.RemoveModApplication(settingsWindow.launcherButton);
        }

        internal void showWindow()  // triggered by application launcher
        {
            settingsWindow.showWindow = true;
        }

        internal void hideWindow() // triggered by application launcher
        {
            settingsWindow.showWindow = false;
        }

        internal void toggleWindow()
        {
            if (settingsWindow.launcherButton != null)
            {
                if (settingsWindow.showWindow)
                {
                    settingsWindow.launcherButton.SetFalse();
                }
                else
                {
                    settingsWindow.launcherButton.SetTrue();
                }
            }
            else
            {
                if (settingsWindow.showWindow)
                {
                    hideWindow();
                }
                else
                {
                    showWindow();
                }
            }
        }

        internal void OnGUI()
        {
            if (visibleUI)
            {
                settingsWindow.draw();
            }
        }

        internal void Update()
        {

            // Load Application Launcher
            if (settingsWindow.launcherButton == null && settings.useStockToolbar)
            {
                OnGUIApplicationLauncherReady();
                if (settingsWindow.showWindow)
                {
                    settingsWindow.launcherButton.SetTrue();
                }
            }

            // Destroy application launcher
            if (settingsWindow.launcherButton != null && settings.useStockToolbar == false)
            {
                removeApplicationLauncher();
            }

            foreach (Part p in FlightGlobals.ActiveVessel.parts)
            {
                if (p.Modules.OfType<ModuleParachute>().Any())
                {
                    StackIcon chuteIcon = Staging.FindIcon(p);
                    if (chuteIcon != null)
                    {
                        if (origTexture == null)
                        {
                            // First time through, set original texture
                            origTexture = chuteIcon.Bg;
                        }
                        else
                        {
                            // Other times, reset to original first then change below as needed
                            chuteIcon.Bg = origTexture;
                            p.stackIcon.SetBgColor(Color.white);
                        }

                        ModuleParachute chute = p.Modules.GetModules<ModuleParachute>().First();

                        if (settings.resetOnlyDeployWhenSafe)
                        {
                            chute.deployAltitude = 500f;
                            chute.minAirPressureToOpen = 0.01f;
                            settings.resetOnlyDeployWhenSafe = false;
                        }

                        if (chute.deploymentState == ModuleParachute.deploymentStates.STOWED && FlightGlobals.ActiveVessel.atmDensity > 0)
                        {
                            if (chute.deploySafe == "Safe")
                            {
                                if (settings.clearBackground)
                                {
                                    chuteIcon.Bg = settings.safeTexture;
                                }
                                else
                                {
                                    p.stackIcon.SetBgColor(settings.safeColor);
                                }
                            }
                            if (chute.deploySafe == "Risky")
                            {
                                if (settings.clearBackground)
                                {
                                    chuteIcon.Bg = settings.riskyTexture;
                                }
                                else
                                {
                                    p.stackIcon.SetBgColor(settings.riskyColor);
                                }
                            }
                            if (chute.deploySafe == "Unsafe")
                            {
                                if (settings.clearBackground)
                                {
                                    chuteIcon.Bg = settings.unSafeTexture;
                                }
                                else
                                {
                                    p.stackIcon.SetBgColor(settings.unSafeColor);
                                }
                            }

                        }

                        if (settings.onlyDeployWhenSafe && chute.deploymentState == ModuleParachute.deploymentStates.ACTIVE && FlightGlobals.ActiveVessel.atmDensity > 0)
                        {
                            if (chute.deploySafe == "Safe")
                            {
                                p.stackIcon.SetIconColor(settings.safeColor);
                                chute.deployAltitude = (float)FlightGlobals.ActiveVessel.altitude;
                                chute.minAirPressureToOpen = 0.01f;
                            }
                            else
                            {
                                p.stackIcon.SetIconColor(settings.riskyColor);
                                chute.deployAltitude = 100f;
                                chute.minAirPressureToOpen = 100f;
                            }
                        }

                    }
                }
            }
        }

        internal void OnDestroy()
        {
            settingsWindow.showWindow = false;

            removeLauncherButtons();

            GameEvents.onGameSceneLoadRequested.Remove(onSceneChange);
            GameEvents.onShowUI.Remove(showUI);
            GameEvents.onHideUI.Remove(hideUI);

        }

    }
}
