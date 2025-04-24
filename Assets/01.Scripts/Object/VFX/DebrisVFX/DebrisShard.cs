using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace ObjectManage.VFX
{

    public class DebrisShard : MonoBehaviour
    {
        public event Action OnShardDeadEvent;
        [SerializeField] private float _speed;
        [SerializeField] private float _dissolveDuration = 0.3f;
        private SpriteRenderer _visualRenderer;
        private Rigidbody2D _rigid;
        private PolygonCollider2D _collider;
        private CircleCollider2D _circleCollider;
        private readonly int _dissolveHash = Shader.PropertyToID("_DissolveLevel");

        private void Awake()
        {
            _visualRenderer = GetComponent<SpriteRenderer>();
            _rigid = GetComponent<Rigidbody2D>();
            _collider = GetComponent<PolygonCollider2D>();
            _circleCollider = GetComponent<CircleCollider2D>();
        }
        public void Initialize(DebrisData data)
        {
            _visualRenderer.sprite = data.debrisSprite;
            _circleCollider.enabled = data.isCircleCollider;
            _collider.enabled = !data.isCircleCollider;
            if (data.isCircleCollider)
            {
                _circleCollider.radius = data.shardSize;
            }
            else
            {
                _collider.pathCount = 0; // remove prev Path
                _collider.pathCount = 0;

                int shapeCount = data.debrisSprite.GetPhysicsShapeCount();
                _collider.pathCount = shapeCount;

                List<Vector2> path = new List<Vector2>();
                for (int i = 0; i < shapeCount; i++)
                {
                    path.Clear();
                    data.debrisSprite.GetPhysicsShape(i, path);
                    _collider.SetPath(i, path);
                }
            }


        }

        private IEnumerator LifeTimeCoroutine(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime - _dissolveDuration);
            float currentTime = 0f;
            while (currentTime < _dissolveDuration)
            {
                currentTime += Time.deltaTime;

                _visualRenderer.material.SetFloat(_dissolveHash,
                 Mathf.Lerp(1f, 0f, currentTime / _dissolveDuration));
                yield return null;
            }
            gameObject.SetActive(false);
            OnShardDeadEvent?.Invoke();

        }

        public void Play(Vector2 direction, float speed, float lifeTime)
        {
            StartCoroutine(LifeTimeCoroutine(lifeTime));
            _visualRenderer.material.SetFloat(_dissolveHash, 1f);
            _rigid.linearVelocity = direction.normalized * speed;

        }
    }
}