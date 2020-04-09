using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gamebase
{
    [Serializable]
    public sealed class TextAssetReference : AssetReferenceT<TextAsset>
    {
        public TextAssetReference(string guid) : base(guid)
        {
            
        }
    }
}