using UnityEngine;
namespace Agents.Players.SkillSystem
{

    public class ShotgunFireVFX : MonoBehaviour
    {
        private ParticleSystem _vfx;
        private void Awake()
        {
            _vfx = transform.GetComponentInChildren<ParticleSystem>();
        }
        public void Play(Vector2 direction)
        {
            transform.right = direction;
            _vfx.Play();
        }
    }
}