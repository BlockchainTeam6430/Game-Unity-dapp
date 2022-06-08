﻿
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Plugins.EntenEller.Base.Scripts.Advanced.ForEditor
{
    public static class EEInfoWindowUtils
    {
#if UNITY_EDITOR
        public static void ShowInfoWindow(string title, string message)
        {
            EditorUtility.DisplayDialog(title, message, "Ok");
            GUIUtility.ExitGUI();
        }
#endif
    }
}
