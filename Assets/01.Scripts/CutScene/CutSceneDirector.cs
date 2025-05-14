using CameraControllers;
using Combat.PlayerTagSystem;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Playables;

namespace PerformanceSystem.CutScene
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CutSceneDirector : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _director;

        private void Awake()
        {
        }

        public void StopTimeline()
        {
            _director.Stop();
        }
        public void PauseTimeline()
        {
            _director.playableGraph.GetRootPlayable(0).SetSpeed(0f);
        }

        public void ResumeTimeline()
        {
            _director.playableGraph.GetRootPlayable(0).SetSpeed(1f);
            print("Resume");
        }

        public void StartTimeline()
        {
            _director.Play();
        }

        public void EndTimeline()
        {
            _director.time = _director.duration;
        }
    }

}