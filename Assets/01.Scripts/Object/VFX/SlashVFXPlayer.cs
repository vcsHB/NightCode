using DG.Tweening;
using UnityEngine;

namespace ObjectManage
{

    public class SlashVFXPlayer : MonoBehaviour
    {
        [SerializeField] Transform _impactTrm;
        [SerializeField] private float _duration = 1f;

        [SerializeField] private float _maxImpactScale = 0.2f;


        public void Play()
        {
            _impactTrm.localScale = new Vector3(_impactTrm.localScale.x, _maxImpactScale, 1f);
            _impactTrm.DOScaleY(0f, _duration);
        }
    }
}
