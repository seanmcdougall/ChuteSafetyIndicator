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
        internal Settings settings = new Settings("ChuteSafetyIndicator.cfg");
        internal Texture2D safeTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        internal Texture2D riskyTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        internal Texture2D unSafeTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        internal Texture2D origTexture = null;

        internal void Awake()
        {
            settings.Load();
            settings.Save();
        }

        internal void Start()
        {
            safeTexture.SetPixel(0, 0, settings.safeColor);
            safeTexture.Apply();
            riskyTexture.SetPixel(0, 0, settings.riskyColor);
            riskyTexture.Apply();
            unSafeTexture.SetPixel(0, 0, settings.unSafeColor);
            unSafeTexture.Apply();
        }

        internal void Update()
        {
            foreach (Part p in FlightGlobals.ActiveVessel.parts)
            {
                if (p.Modules.OfType<ModuleParachute>().Any())
                {
                    StackIcon chuteIcon = Staging.FindIcon(p);
                    if (chuteIcon != null)
                    {
                        if (origTexture == null)
                        {
                            origTexture = chuteIcon.Bg;
                        }
                        ModuleParachute chute = p.Modules.GetModules<ModuleParachute>().First();
                        if (chute.deploymentState == ModuleParachute.deploymentStates.STOWED && FlightGlobals.ActiveVessel.atmDensity > 0)
                        {
                            if (chute.deploySafe == "Safe")
                            {
                                if (settings.clearBackground)
                                {
                                    chuteIcon.Bg = safeTexture;
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
                                    chuteIcon.Bg = riskyTexture;
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
                                    chuteIcon.Bg = unSafeTexture;
                                }
                                else
                                {
                                    p.stackIcon.SetBgColor(settings.unSafeColor);
                                }
                            }
                        }
                        else
                        {
                            if (settings.clearBackground)
                            {
                                chuteIcon.Bg = origTexture;
                            }
                            else
                            {
                                p.stackIcon.SetBgColor(Color.white);
                            }
                        }
                    }
                }
            }
        }
    }
}
