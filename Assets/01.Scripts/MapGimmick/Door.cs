using DG.Tweening;
using UnityEngine;

namespace ObjectManage.GimmickObjects.Logics
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float _openDuration;
        [SerializeField] private Transform _spriteTrm;

        private Tween _openCloseTween;

        public void HandelDoor(bool isClose)
        {
            Debug.Log(isClose);
            if (isClose) Close();
            else Open();
        }

        public void Open()
        {
            if (_openCloseTween != null && _openCloseTween.active) 
                _openCloseTween.Kill();

            _openCloseTween = _spriteTrm.DOScaleY(1, _openDuration);
        }

        public void Close()
        {
            if (_openCloseTween != null && _openCloseTween.active)
                _openCloseTween.Kill();

            _openCloseTween = _spriteTrm.DOScaleY(0, _openDuration);
        }
    }
}
