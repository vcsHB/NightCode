using Combat.PlayerTagSystem;
using InputManage;
using UnityEngine;
namespace PerformanceSystem.CutScene
{

    public class CutScenePlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _defaultPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private PlayerInputStatus _inputStatus;

        public void SetInputState()
        {
            _playerInput.SetInputStatus(_inputStatus);
        }
        public void ResetInputEnable()
        {
            _playerInput.SetEnabledAllStatus();
        }

        public void MoveToDefaultPoint()
        {
            SetPlayerPosition(_defaultPoint);
        }
        
        public void MoveToEndPoint()
        {
            SetPlayerPosition(_endPoint);
        }

        public void SetPlayerPosition(Transform point)
        {
            SetPlayerPosition(point.position);
        }
        public void SetPlayerPosition(Vector2 position)
        {
            PlayerManager.Instance.SetCurrentPlayerPosition(position);
        }
    }
}