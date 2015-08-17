﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChuteSafetyIndicator
{
    public class ModStyle
    {
        public GUISkin skin;
        public Dictionary<string, GUIStyle> guiStyles;
        public int fontSize = 12;
        public int minWidth = 110;
        public int minHeight = 100;

        public ModStyle()
        {
            guiStyles = new Dictionary<string, GUIStyle>();

            skin = GameObject.Instantiate(HighLogic.Skin) as GUISkin;

            skin.button.padding = new RectOffset() { left = 3, right = 3, top = 3, bottom = 3 };
            skin.button.wordWrap = true;
            skin.button.fontSize = fontSize;

            skin.label.padding.top = 0;
            skin.label.fontSize = fontSize;

            skin.verticalScrollbar.fixedWidth = 10f;

            skin.window.onNormal.textColor = skin.window.normal.textColor = XKCDColors.Green_Yellow;
            skin.window.onHover.textColor = skin.window.hover.textColor = XKCDColors.YellowishOrange;
            skin.window.onFocused.textColor = skin.window.focused.textColor = Color.red;
            skin.window.onActive.textColor = skin.window.active.textColor = Color.blue;
            skin.window.padding.left = skin.window.padding.right = skin.window.padding.bottom = 2;
            skin.window.fontSize = (fontSize + 2);
            skin.window.padding = new RectOffset() { left = 1, top = 5, right = 1, bottom = 1 };

            Texture2D blackBackground = new Texture2D(1, 1);
            blackBackground.SetPixel(0, 0, Color.black);
            blackBackground.Apply();

            guiStyles["titleLabel"] = new GUIStyle();
            guiStyles["titleLabel"].name = "titleLabel";
            guiStyles["titleLabel"].fontSize = fontSize + 3;
            guiStyles["titleLabel"].fontStyle = FontStyle.Bold;
            guiStyles["titleLabel"].alignment = TextAnchor.MiddleCenter;
            guiStyles["titleLabel"].wordWrap = true;
            guiStyles["titleLabel"].normal.textColor = Color.yellow;
            guiStyles["titleLabel"].padding = new RectOffset() { left = 20, right = 20, top = 0, bottom = 0 };

            guiStyles["tooltip"] = new GUIStyle();
            guiStyles["tooltip"].name = "tooltip";
            guiStyles["tooltip"].fontSize = fontSize + 3;
            guiStyles["tooltip"].wordWrap = true;
            guiStyles["tooltip"].alignment = TextAnchor.MiddleCenter;
            guiStyles["tooltip"].normal.textColor = Color.yellow;
            guiStyles["tooltip"].normal.background = blackBackground;

            Texture2D clearTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            clearTexture.SetPixel(0, 0, Color.clear);
            clearTexture.Resize(30, 20);
            clearTexture.Apply();

            guiStyles["clear"] = new GUIStyle();
            guiStyles["clear"].padding = guiStyles["clear"].border = new RectOffset() { left = 0, right = 0, top = 0, bottom = 0 };
            guiStyles["clear"].margin = new RectOffset() { left = 0, right = 0, top = 2, bottom = 2 };
            guiStyles["clear"].normal.background = clearTexture;
            guiStyles["clear"].onNormal.background = clearTexture;
            guiStyles["clear"].hover.background = clearTexture;
            guiStyles["clear"].active.background = clearTexture;
            guiStyles["clear"].fixedHeight = 25f;
            guiStyles["clear"].fixedWidth = 25f;

            guiStyles["button"] = new GUIStyle();
            guiStyles["button"].padding = guiStyles["clear"].border = new RectOffset() { left = 0, right = 0, top = 0, bottom = 0 };
            guiStyles["button"].margin = new RectOffset() { left = 0, right = 0, top = 2, bottom = 2 };
            guiStyles["button"].normal.background = skin.button.normal.background;
            guiStyles["button"].onNormal.background = skin.button.onNormal.background;
            guiStyles["button"].hover.background = skin.button.hover.background;
            guiStyles["button"].active.background = skin.button.active.background;
            guiStyles["button"].fixedHeight = 25f;
            guiStyles["button"].fixedWidth = 25f;

        }

        public Texture2D GetImage(String path, int width, int height)
        {
            Texture2D img = new Texture2D(width, height, TextureFormat.ARGB32, false);
            img = GameDatabase.Instance.GetTexture(path, false);
            return img;
        }

    }
}
