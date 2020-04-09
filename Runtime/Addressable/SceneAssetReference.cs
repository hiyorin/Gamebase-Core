using System;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Gamebase
{
    [Serializable]
    public sealed class SceneAssetReference : AssetReferenceT<Object>
    {
        public SceneAssetReference(string guid) : base(guid)
        {
            
        }
        
        public override bool ValidateAsset(Object obj)
        {
            return ValidateAsset(obj.name);
        }
        
        public override bool ValidateAsset(string path)
        {
            return path.IndexOf(".unity", StringComparison.Ordinal) > 0;
        }
    }
}