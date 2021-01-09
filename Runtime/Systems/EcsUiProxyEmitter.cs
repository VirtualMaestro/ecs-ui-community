// ----------------------------------------------------------------------------
// The MIT License
// Ui extension https://github.com/Leopotam/ecs-ui
// for ECS framework https://github.com/Leopotam/ecs
// Copyright (c) 2017-2020 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine;

namespace Leopotam.Ecs.Ui.Systems {
    /// <summary>
    /// Proxy Emitter for redirect calls to root one.
    /// </summary>
    public class EcsUiProxyEmitter : EcsUiEmitter {
        [SerializeField] EcsUiEmitter _parent;

        public override EcsWorld GetWorld () {
            return ValidateEmitter () ? _parent.GetWorld () : default;
        }

        public override EcsEntity CreateEntity () {
            return ValidateEmitter () ? _parent.CreateEntity () : default;
        }

        /// <summary>
        /// Sets link to named GameObject to use it later from code. If GameObject is null - unset named link.
        /// </summary>
        /// <param name="widgetName">Logical name.</param>
        /// <param name="go">GameObject link.</param>
        public override void SetNamedObject (string widgetName, GameObject go) {
            if (ValidateEmitter ()) { _parent.SetNamedObject (widgetName, go); }
        }

        /// <summary>
        /// Gets link to named GameObject to use it later from code.
        /// </summary>
        /// <param name="widgetName">Logical name.</param>
        public override GameObject GetNamedObject (string widgetName) {
            return ValidateEmitter () ? _parent.GetNamedObject (widgetName) : default;
        }

        bool ValidateEmitter () {
            if (_parent) { return true; }
            // parent was killed.
            if ((object) _parent != null) { return false; }

            _parent = GetComponentInParent<EcsUiEmitter> ();
#if DEBUG
            if (_parent == null) {
                Debug.LogError ("EcsUiEmitter not found in hierarchy", this);
            }
#endif
            return true;
        }
    }
}