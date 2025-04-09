using DG.Tweening;
using UnityEngine;

namespace ObjectManage.GimmickObjects.Logics
{
    public class SpringGimmick : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsGround;

        [SerializeField] private SpriteRenderer _pivot;
        [SerializeField] private Transform _edge;
        [SerializeField] private SpringGimmickBottom _bottom;

        [Space]
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _downDelay;
        [SerializeField] private float _downDuration;
        [SerializeField] private float _upDelay;
        [SerializeField] private float _upDuration;

        private Sequence _seq;
        private float _prevDown;
        private bool _isDown = false;

        private void OnEnable()
        {
            _prevDown = Time.time;
        }

        private void Update()
        {
            if (!_isDown && _prevDown + _downDelay < Time.time)
            {
                _isDown = true;
                Down();
            }
        }

        private void Down()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, _maxDistance, _whatIsGround);
            float distance = hit.distance;
            _bottom.SetIsDown(true);

            if (_seq != null && _seq.active) _seq.Kill();

            _seq = DOTween.Sequence();

            _seq.Append(DOTween.To(() => _pivot.size.y, y =>
            {
                _pivot.size = new Vector2(_pivot.size.x, y);
                _edge.transform.localPosition = new Vector2(0, -y);
            }, distance / transform.localScale.y, _downDuration))
                .AppendCallback(() => _bottom.SetIsDown(false))
                .AppendInterval(_upDelay)
                .Append(DOTween.To(() => _pivot.size.y, y =>
                {
                    _pivot.size = new Vector2(_pivot.size.x, y);
                    _edge.transform.localPosition = new Vector2(0, -y);
                }, 1, _upDuration))
                .OnUpdate(() => _bottom.transform.position = _edge.position)
                .OnComplete(() =>
                {
                    _prevDown = Time.time;
                    _isDown = false;
                });
        }
    }
}
