#if GAMEBASE_ADD_NODECANVAS
using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Gamebase.Core.NodeCanvas.Actions
{
    [PublicAPI]
    [Name("Play Timeline")]
    [Category("✫ Gamebase/Playable")]
    public sealed class PlayTimeline : ActionTask<PlayableDirector>
    {
        public BBParameter<TimelineAsset> timeline = default;
        
        protected override string info
        {
            get
            {
                if (timeline != null && timeline.value != null)
                    return $"{base.info} ({timeline})";
                else if (agent != null && agent.playableAsset != null)
                    return $"{base.info} ({agent.playableAsset.name})";
                else
                    return $"{base.info} (NULL)";
            }
        }
        
        protected override void OnExecute()
        {
            if (timeline.value != null)
                agent.playableAsset = timeline.value;

            agent.stopped += OnTimelineStopped;
            agent.Play();
        }

        private void OnTimelineStopped(PlayableDirector playableDirector)
        {
            EndAction(true);
            agent.stopped -= OnTimelineStopped;
        }
    }
}
#endif