using Office;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Base.Office
{
    public class MainTable : BaseInteractiveObject
    {
        [SerializeField] private BaseInput _baseInput;
        private CharacterSelectPanel _characterSelectPanel;
        private SkillTreePanel _skillTreePanel;
        private PlayerMoveTarget _playerMove;

        private void Awake()
        {
            _characterSelectPanel = FindAnyObjectByType<CharacterSelectPanel>();
            _skillTreePanel = FindAnyObjectByType<SkillTreePanel>();
            _playerMove = GetComponentInChildren<PlayerMoveTarget>();
        }

        public override void OnPlayerInteract()
        {
            _player.AddInteract(OnInteract);
        }

        public override void OnPlayerInteractExit()
        {
            _player.RemoveInteract(OnInteract);
        }

        private void OnInteract()
        {
            _playerMove.MovePlayer(_player);
            _playerMove.onCompleteMove += OpenUI;
            _baseInput.DisableInput();
        }

        private void OnCloseUI()
        {
            _baseInput.EnableInput();
            _characterSelectPanel.onCloseUI -= OnCloseUI;
        }

        private void OpenUI()
        {
            _characterSelectPanel.Open();
            _characterSelectPanel.onCloseUI += OnCloseUI;
            //_skillTreePanel.Open();
            _playerMove.onCompleteMove -= OpenUI;
        }
    }
}
