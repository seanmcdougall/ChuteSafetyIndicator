using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChuteSafetyIndicator
{
    public sealed class SettingsWrapper
    {
        public static readonly SettingsWrapper instance = new SettingsWrapper();
        public Settings gameSettings { get; private set; }
        public ModStyle modStyle { get; private set; }

        static SettingsWrapper()
        {
        }

        private SettingsWrapper()
        {
            gameSettings = new Settings("ChuteSafetyIndicator.cfg");
            gameSettings.Load();
            gameSettings.Save();

            gameSettings.safeTexture.SetPixel(0, 0, gameSettings.safeColor);
            gameSettings.safeTexture.Apply();
            gameSettings.riskyTexture.SetPixel(0, 0, gameSettings.riskyColor);
            gameSettings.riskyTexture.Apply();
            gameSettings.unSafeTexture.SetPixel(0, 0, gameSettings.unSafeColor);
            gameSettings.unSafeTexture.Apply();



            modStyle = new ModStyle();
        }

        public static SettingsWrapper Instance
        {
            get
            {
                return instance;
            }
        }
    }

}
