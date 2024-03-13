using System;
using System.Collections;
using System.Collections.Generic;
using AirpassUnity.VRSports;
using UnityEngine;
using UnityEngine.UI;

namespace MonLab
{
    public sealed class VrButton : VRSportsButton
    {
        // __delay__
        [SerializeField] private bool isDelay;

        private const float MaxDelay = 0.1f;
        private float delay = 0.2f;

        private Button _btn;

        public Button Btn
        {
            get
            {
                if (ReferenceEquals(_btn, null))
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    _btn = GetComponent<Button>();
                }

                return _btn;
            }
        }

        public override void Interact(bool isExit = false)
        {
            if (isDelay)
            {
                if (!isExit && delay < MaxDelay) return;

                delay = 0.0f;
            }

            base.Interact(isExit);
        }

        protected override void Update()
        {
            if (isDelay)
            {
                if (delay < MaxDelay)
                {
                    delay += Time.deltaTime;
                }
            }

            base.Update();

            if (IsInteract != IsInteractPrev)
            {
                IsInteractPrev = IsInteract;

                if (IsInteract)
                {
                    
                }

                else
                {
                    
                }
            }
        }
    }
}
