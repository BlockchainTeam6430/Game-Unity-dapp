﻿using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Behaviours;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Advanced.Scenes;
using UnityEngine;

namespace Plugins.EntenEller.Base.Scripts.Advanced.GameObjects
{
    public static class EEGameObjectUtils
    {
        private static bool isInitialized;
        private static HashSet<EEGameObject> _EEGameObjects = null;
        
        public static HashSet<EEGameObject> EEGameObjects
        {
            get
            {
                if (_EEGameObjects != null) return _EEGameObjects;
                
                if (!isInitialized)
                {
                    isInitialized = true;
                    EESceneData.ScenesRawFinishedChangesEvent += () =>
                    {
                        _EEGameObjects = null;
                    };
                }
                
                _EEGameObjects = new HashSet<EEGameObject>();
                
                var resources = Resources.FindObjectsOfTypeAll<EEBehaviourInit>();
                
                for (var i = 0; i < resources.Length; i++)
                {
                    var obj = resources[i];
                    if (obj.hideFlags != HideFlags.None) continue;
                    if (obj.gameObject.scene.name == null) continue;
                    _EEGameObjects.Add(obj.GetEEGameObject());
                }

                return _EEGameObjects;
            }
        }

        public static void Add(EEGameObject obj)
        {
            EEGameObjects.Add(obj);
        }
        
        public static void Remove(EEGameObject obj)
        {
            EEGameObjects.Remove(obj);
        }
        
        public static GameObject Create(MonoBehaviour script)
        {
            return new GameObject {name = script.gameObject.name + " - " + GenerateName(script.ToString())};
        }

        public static GameObject Create(Type script)
        {
            return new GameObject {name = GenerateName(script.ToString())};
        }

        private static string GenerateName(string script)
        {
            return script.Split('.').Last() + " [GENERATED]";
        }
        
        public static EEGameObject GetEEGameObject(this Component component)
        {
            var obj = component.GetComponent<EEGameObject>();
            if (obj.IsNull()) obj = component.gameObject.AddComponent<EEGameObject>();
            return obj;
        }
        
        public static EEGameObject GetEEGameObject(this GameObject gameObject)
        {
            return gameObject.transform.GetEEGameObject();
        }
    }
}