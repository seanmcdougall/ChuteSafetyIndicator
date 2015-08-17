using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChuteSafetyIndicator
{
    internal class SettingsWindow
    {
        internal ApplicationLauncherButton launcherButton = null;
        internal IButton blizzyButton = null;

        internal bool showWindow;
        internal bool triggerSave;
        internal Rect windowRect;
        internal Rect dragRect;
        internal Vector2 scrollPos = new Vector2(0f, 0f);
        internal int windowId;
        internal Settings settings;
        internal ModStyle modStyle;

        internal SettingsWindow()
        {
            settings = SettingsWrapper.Instance.gameSettings;
            modStyle = SettingsWrapper.Instance.modStyle;
            showWindow = false;
            triggerSave = false;
            windowRect = new Rect((Screen.width - 250) / 2, (Screen.height - 600) / 2, 250, 600);
            windowId = GUIUtility.GetControlID(FocusType.Passive);
        }

        internal void draw()
        {
            if (showWindow)
            {
                GUI.skin = modStyle.skin;
                windowRect = GUILayout.Window(windowId, windowRect, drawWindow, "");
                triggerSave = true;
            }
            else
            {
                if (triggerSave)
                {
                    settings.Save();
                    triggerSave = false;
                }
            }
        }

        internal void drawWindow(int id)
        {
            GUI.skin = modStyle.skin;
            GUILayout.BeginVertical();
            GUILayout.Label("Chute Safety Indicator", modStyle.guiStyles["titleLabel"]);
            GUILayout.EndVertical();
            if (Event.current.type == EventType.Repaint)
            {
                dragRect = GUILayoutUtility.GetLastRect();
            }
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            
            bool newUseStockToolbar;
            if (ToolbarManager.ToolbarAvailable)
            {
                GUILayout.Space(10f);
                newUseStockToolbar = GUILayout.Toggle(settings.useStockToolbar, "Use stock toolbar");
            }
            else
            {
                newUseStockToolbar = true;
            }

            if (newUseStockToolbar != settings.useStockToolbar)
            {
                settings.useStockToolbar = newUseStockToolbar;
                settings.Save();
            }

            GUILayout.Space(10f);

            bool newOnlyDeployWhenSafe = GUILayout.Toggle(settings.onlyDeployWhenSafe, "Only deploy when safe");
            if (newOnlyDeployWhenSafe != settings.onlyDeployWhenSafe)
            {
                settings.onlyDeployWhenSafe = newOnlyDeployWhenSafe;
                settings.resetOnlyDeployWhenSafe = true;
                settings.Save();
            }

            GUILayout.Space(10f);

            bool newClearBackground = GUILayout.Toggle(settings.clearBackground, "Clear icon backgrounds");
            if (newClearBackground != settings.clearBackground)
            {
                settings.clearBackground = newClearBackground;
                settings.resetClearBackground = true;
                settings.Save();
            }

            GUILayout.Space(10f);

            Color oldColor = GUI.backgroundColor;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Safe Color: ", GUILayout.ExpandWidth(true));
            GUI.backgroundColor = settings.safeColor;
            if (settings.clearBackground)
            {
                GUILayout.Button("", modStyle.guiStyles["clear"]);
            }
            else
            {
                GUILayout.Button("", modStyle.guiStyles["button"]);
            }
            
            GUI.backgroundColor = oldColor;
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("R: ");
            float safeRed = GUILayout.HorizontalSlider(settings.safeColor.r, 0f, 1f,GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("G: ");
            float safeGreen = GUILayout.HorizontalSlider(settings.safeColor.g, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("B: ");
            float safeBlue = GUILayout.HorizontalSlider(settings.safeColor.b, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("A: ");
            float safeAlpha = GUILayout.HorizontalSlider(settings.safeColor.a, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            if (safeRed != settings.safeColor.r || safeGreen != settings.safeColor.g || safeBlue != settings.safeColor.b || safeAlpha != settings.safeColor.a)
            {
                settings.safeColor = new Color(safeRed, safeGreen, safeBlue, safeAlpha);
                settings.safeTexture.SetPixel(0, 0, settings.safeColor);
                settings.safeTexture.Apply();
            }


            GUILayout.BeginHorizontal();
            GUILayout.Label("Risky Color: ",GUILayout.ExpandWidth(true));
            GUI.backgroundColor = settings.riskyColor;
            if (settings.clearBackground)
            {
                GUILayout.Button("", modStyle.guiStyles["clear"]);
            }
            else
            {
                GUILayout.Button("", modStyle.guiStyles["button"]);
            }
            GUI.backgroundColor = oldColor;
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("R: ");
            float riskyRed = GUILayout.HorizontalSlider(settings.riskyColor.r, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("G: ");
            float riskyGreen = GUILayout.HorizontalSlider(settings.riskyColor.g, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("B: ");
            float riskyBlue = GUILayout.HorizontalSlider(settings.riskyColor.b, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("A: ");
            float riskyAlpha = GUILayout.HorizontalSlider(settings.riskyColor.a, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            if (riskyRed != settings.riskyColor.r || riskyGreen != settings.riskyColor.g || riskyBlue != settings.riskyColor.b || riskyAlpha != settings.riskyColor.a)
            {
                settings.riskyColor = new Color(riskyRed, riskyGreen, riskyBlue, riskyAlpha);
                settings.riskyTexture.SetPixel(0, 0, settings.riskyColor);
                settings.riskyTexture.Apply();
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Unsafe Color: ",GUILayout.ExpandWidth(true));
            GUI.backgroundColor = settings.unSafeColor;
            if (settings.clearBackground)
            {
                GUILayout.Button("", modStyle.guiStyles["clear"]);
            }
            else
            {
                GUILayout.Button("", modStyle.guiStyles["button"]);
            }
            GUI.backgroundColor = oldColor;
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("R: ");
            float unSafeRed = GUILayout.HorizontalSlider(settings.unSafeColor.r, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("G: ");
            float unSafeGreen = GUILayout.HorizontalSlider(settings.unSafeColor.g, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("B: ");
            float unSafeBlue = GUILayout.HorizontalSlider(settings.unSafeColor.b, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("A: ");
            float unSafeAlpha = GUILayout.HorizontalSlider(settings.unSafeColor.a, 0f, 1f, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            if (unSafeRed != settings.unSafeColor.r || unSafeGreen != settings.unSafeColor.g || unSafeBlue != settings.unSafeColor.b || unSafeAlpha != settings.unSafeColor.a)
            {
                settings.unSafeColor = new Color(unSafeRed, unSafeGreen, unSafeBlue, unSafeAlpha);
                settings.unSafeTexture.SetPixel(0, 0, settings.unSafeColor);
                settings.unSafeTexture.Apply();
            }

            GUI.backgroundColor = oldColor;

            GUILayout.EndScrollView();
            GUILayout.Space(25f);
            GUILayout.EndVertical();

            if (GUI.Button(new Rect(windowRect.width - 18, 3f, 15f, 15f), new GUIContent("X")))
            {
                showWindow = false;
                if (launcherButton != null)
                {
                    launcherButton.SetFalse();
                }
                
            }

            GUI.DragWindow(dragRect);

        }

    }
}
