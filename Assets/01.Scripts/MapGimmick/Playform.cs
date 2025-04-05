using UnityEngine;

namespace Ingame.Gimmick
{
    public class Playform : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
    }
}
