using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Gamebase.Core.Timeline
{
    public sealed class WaitForInputClip : PlayableAsset, ITimelineClipAsset
    {
        [Serializable]
        public enum Category
        {
            UI,
            Collider,
        }

        [SerializeField] private Category category = Category.UI;

        [SerializeField] private WaitForInputGraphicBehaviour graphicTemplate = default;

        [SerializeField] private WaitForInputColliderBehaviour colliderTemplate = default;

        ClipCaps ITimelineClipAsset.clipCaps => ClipCaps.None;
        
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            switch (category)
            {
                case Category.UI:
                    return ScriptPlayable<WaitForInputGraphicBehaviour>.Create(graph, graphicTemplate);
                case Category.Collider:
                    return ScriptPlayable<WaitForInputColliderBehaviour>.Create(graph, colliderTemplate);
                default:
                    Debug.unityLogger.LogError(nameof(WaitForInputClip), $"Not implementation {category}");
                    break;
            }

            return Playable.Null;
        }
    }
}