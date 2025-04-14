using DG.Tweening;
using UnityEngine;

namespace ObjectManage.GimmickObjects.Logics
{
    public class SpringGimmick : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsGround;

        [SerializeField] private SpriteRenderer _pivot;
        [SerializeField] private Transform _edge;
        [SerializeField] private Transform _warning;
        [SerializeField] private SpringGimmickBottom _bottom;

        [Space]
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _warningDelay;
        [SerializeField] private float _downDelay;
        [SerializeField] private float _downDuration;
        [SerializeField] private float _upDelay;
        [SerializeField] private float _upDuration;


        private Sequence _seq;
        private float _prevDown;
        private bool _isDown = false;
        private bool _isWarninig = false;

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

            if (!_isWarninig && _prevDown + _warningDelay < Time.time)
            {
                _isWarninig = true;
                Warning();
            }
        }

        private void Warning()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, _maxDistance, _whatIsGround);
            float distance = hit.distance;

            _warning.localScale = new Vector3(1, distance, 1);
        }

        private void Down()
        {
            _warning.localScale = new Vector3(1, 0, 1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, _maxDistance, _whatIsGround);
            float distance = hit.distance;
            _bottom.SetIsDown(true);

            if (_seq != null && _seq.active) _seq.Kill();

            _seq = DOTween.Sequence();

            _seq.Append(DOTween.To(() => _pivot.size.y, y =>
            {
                _pivot.size = new Vector2(_pivot.size.x, y);
                _edge.transform.localPosition = new Vector2(0, -y);
            }, distance, _downDuration))
                .AppendCallback(() => _bottom.SetIsDown(false))
                .AppendInterval(_upDelay)
                .Append(DOTween.To(() => _pivot.size.y, y =>
                {
                    _pivot.size = new Vector2(_pivot.size.x, y);
                    _edge.transform.localPosition = new Vector2(0, -y);
                }, distance, _downDuration))
                .OnUpdate(() => _bottom.transform.position = _edge.position)
                .OnComplete(() =>
                {
                    _prevDown = Time.time;
                    _isDown = false;
                });
        }
    }
}
