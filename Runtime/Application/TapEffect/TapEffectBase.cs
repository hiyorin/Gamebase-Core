using UnityEngine;

namespace Gamebase.Application.TapEffect
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TapEffectBase : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="worldPosition"></param>
        public abstract void OnPress(Vector3 worldPosition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worldPosition"></param>
        public abstract void OnSwipe(Vector3 worldPosition);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worldPosition"></param>
        public abstract void OnRelease(Vector3 worldPosition);
    }
}
