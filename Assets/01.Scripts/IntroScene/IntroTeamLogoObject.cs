using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IntroScene
{

    public class IntroTeamLogoObject : MonoBehaviour
    {
        [SerializeField] private float _fadeOutDuration = 0.3f;
        [SerializeField] private float _sceneMoveDelay = 0.5f;
        private SpriteRenderer _visualRenderer;
        [SerializeField] private string _moveSceneName;

        private void Awake()
        {
            _visualRenderer = GetComponent<SpriteRenderer>();
        }

        public void HandleAnimationOver()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(_visualRenderer.DOFade(0f, _fadeOutDuration));
            seq.AppendInterval(_sceneMoveDelay);
            seq.AppendCallback(MoveNextScene);
            
        }

        private void MoveNextScene()
        {
            SceneManager.LoadScene(_moveSceneName);
        }

    }

}