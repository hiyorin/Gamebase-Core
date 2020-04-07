using UnityEngine;
#if UNITY_2019_3_OR_NEWER
using UnityEngine.VFX;
#else
using UnityEngine.Experimental.VFX;
#endif

namespace Gamebase.Application.TapEffect
{
    [RequireComponent(typeof(VisualEffect))]
    public class VfxTapEffect : TapEffectBase
    {
        [SerializeField] private string positionName = "Position";

        [SerializeField] private string scaleName = "Scale";
        
        private VisualEffect effect;

        protected VisualEffect Effect => effect;
        
        protected int PositionId { private set; get; }
        
        protected int ScaleId { private set; get; }
        
        private void Awake()
        {
            effect = GetComponent<VisualEffect>();

            PositionId = Shader.PropertyToID(positionName);
            ScaleId = Shader.PropertyToID(scaleName);
            
            OnAwake();
        }

        private void Start()
        {
            effect.Stop();
            OnStart();
        }
        
        protected virtual void OnAwake()
        {
            
        }

        protected virtual void OnStart()
        {
            
        }
        
        #region TapEffectBase implementaion

        public override void OnPress(Vector3 worldPosition)
        {
            effect.SetVector3(PositionId, worldPosition);
            effect.SetVector3(ScaleId, transform.lossyScale);
            effect.Play();
        }

        public override void OnSwipe(Vector3 worldPosition)
        {
            effect.SetVector3(PositionId, worldPosition);
            effect.SetVector3(ScaleId, transform.lossyScale);
        }

        public override void OnRelease(Vector3 worldPosition)
        {
            effect.Stop();
        }

        #endregion
    }
}