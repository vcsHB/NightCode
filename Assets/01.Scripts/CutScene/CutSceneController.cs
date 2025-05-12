using CameraControllers;
using Combat.PlayerTagSystem;
using UnityEngine;
using UnityEngine.Playables;

namespace PerformanceSystem.CutScene
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CutSceneDirector : MonoBehaviour
    {
        private PlayableDirector _director;

        private void Awake()
        {
            _director = GetComponentInChildren<PlayableDirector>();
        }
      

        public void PauseTimeline()
        {
            _director.Pause();
        }

        public void Restart()
        {
            _director.Resume();
        }

        public void StartTimeline()
        {
            _director.Play();
        }
    }

}