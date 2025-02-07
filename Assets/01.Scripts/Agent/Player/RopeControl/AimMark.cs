using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agents.Players
{
    public enum AimMode
    {
        Targeting = 0,
        Detected,
        Attack
    }
    public class AimMark : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        public void ChangeAimMode(AimMode mode)
        {
            _spriteRenderer.sprite = _sprites[(int)mode];
        }

    }

}