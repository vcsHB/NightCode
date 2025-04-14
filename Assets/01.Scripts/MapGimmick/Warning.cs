using UnityEngine;
using UnityEngine.UI;

namespace ObjectManage.GimmickObjects.Logics
{
    public class Warning : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField]private float _speed;


        private bool _startFade;
        private float _timer;

        private void Update()
        {
            if (_startFade)
            {
                _timer += Time.deltaTime * _speed;

                float alpha = (Mathf.Sin(_timer) + 1) / 2;
                _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, alpha);
            }
        }


        public void SetSize(float length)
        {
            _timer = 0;
            _startFade = true;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0);

            transform.localScale = new Vector3(1, length, 1);
        }

        public void Disable()
        {
            _startFade = false;
            transform.localScale = new Vector3(1, 0, 1);
        }
    }
}
