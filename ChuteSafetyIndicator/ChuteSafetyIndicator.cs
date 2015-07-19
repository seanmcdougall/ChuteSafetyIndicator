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
        internal void Update()
        {
            foreach (Part p in FlightGlobals.ActiveVessel.parts)
            {
                if (p.Modules.OfType<ModuleParachute>().Any())
                {
                    ModuleParachute chute = p.Modules.GetModules<ModuleParachute>().First();
                    if (chute.deploymentState == ModuleParachute.deploymentStates.STOWED)
                    {
                        if (chute.deploySafe == "Safe")
                        {
                            p.stackIcon.SetBgColor(Color.green);
                        }
                        if (chute.deploySafe == "Risky")
                        {
                            p.stackIcon.SetBgColor(Color.yellow);
                        }
                        if (chute.deploySafe == "Unsafe")
                        {
                            p.stackIcon.SetBgColor(Color.red);
                        }
                    }
                    else
                    {
                        p.stackIcon.SetBgColor(XKCDColors.LightGrey);
                    }
                }
            }
        }
    }
}
