using UnityEngine;

namespace Gamebase.Application.TapEffect
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleTapEffect : TapEffectBase
    {
        private ParticleSystem effect;

        protected ParticleSystem Effect => effect;

        private void Awake()
        {
            effect = GetComponent<ParticleSystem>();
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }

        #region TapEffectBase implementation

        public override void OnPress(Vector3 worldPosition)
        {
            effect.Stop();
            effect.transform.position = worldPosition;
            effect.Play();
        }

        public override void OnSwipe(Vector3 worldPosition)
        {
            
        }

        public override void OnRelease(Vector3 worldPosition)
        {
            
        }

        #endregion
    }
}