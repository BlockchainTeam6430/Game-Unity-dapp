using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.EntenEller.Base.Scripts.Advanced.Components;
using Plugins.EntenEller.Base.Scripts.Advanced.UnityEvents;
using Plugins.EntenEller.Base.Scripts.Cache.Components.Master;
using Sirenix.OdinInspector;

namespace Plugins.EntenEller.Base.Scripts.Advanced.Triggers
{
    public abstract class EETriggerColliderListener : EEBehaviour
    {
        public event Action<EETriggerColliderListener> RawEnterEvent;
        public event Action<EETriggerColliderListener> RawExitEvent;
        
        public EESuperAction RawEnterSuperAction;
        public EESuperAction RawExitSuperAction;
        
        [ReadOnly] public List<EETriggerColliderListener> RawCollisions = new List<EETriggerColliderListener>();
        
        private List<EETriggerColliderListener> cache = new List<EETriggerColliderListener>();
        
        protected override void EEDisable()
        {
            base.EEDisable();
            while (RawCollisions.Any())
            {
                var they = RawCollisions[0];
                RawCollisions.RemoveAt(0);
                they.Exit(this);
                Exit(they);
            }
        }

        protected void Enter(EETriggerColliderListener they)
        {
            if (they.IsNull()) return;
            cache.Add(they);
            RawCollisions.Add(they);
        }

        private void LateUpdate()
        {
            if (cache.Count == 0) return;
            foreach (var trigger in cache)
            {
                RawEnterEvent.Call(trigger);
                RawEnterSuperAction.Call(ComponentID);
            }
            cache.Clear();
        }

        protected void Exit(EETriggerColliderListener they)
        {
            if (they.IsNull()) return;
            RawCollisions.Remove(they);
            RawExitEvent.Call(they);
            RawExitSuperAction.Call(ComponentID);
        }
    }
}
