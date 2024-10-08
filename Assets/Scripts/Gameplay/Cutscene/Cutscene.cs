using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Zenject;

namespace Gameplay.Cutscene
{
    public abstract class Cutscene : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _director;

        private TaskCompletionSource<bool> _endAwaiter;

        [Inject]
        private void Construct(ICinemachineBrainProvider сinemachineBrainProvider)
        {
            SetCinemachineBrain(_director, сinemachineBrainProvider.CinemachineBrain);
        }

        public Task Play()
        {
            _director.stopped += Stopped;
            _director.Play();
            _endAwaiter = new TaskCompletionSource<bool>();
            return _endAwaiter.Task;
        }

        private void Stopped(PlayableDirector playableDirector)
        {
            _endAwaiter?.TrySetResult(true);
            DestroyInternal();
        }

        protected virtual void DestroyInternal()
        {
            Destroy(gameObject);
        }

        private void SetCinemachineBrain(PlayableDirector director, CinemachineBrain brain)
        {
            var timeline = director.playableAsset as TimelineAsset;

            if (timeline == null)
            {
                Debug.Log("Timeline is null");
                return;
            }

            foreach (var track in timeline.GetOutputTracks())
            {
                if (track is CinemachineTrack)
                {
                    director.SetGenericBinding(track, brain);
                }
            }
        }
    }
}