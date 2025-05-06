using CameraControllers;
using Dialog;
using Office;
using System;
using UnityEngine;

namespace Base.Office
{
    public class OfficeNPC : BaseInteractiveObject
    {
        [SerializeField] private InGameDialogPlayer _dialogPlayer;
        [SerializeField] private BaseInput _officeInput;
        [SerializeField] private OfficeNPCSO _dialog;
        private PlayerMoveTarget _moveTarget;
        private CameraFocuser _cameraFocus;
        private int _talkCount = 0;

        private void Awake()
        {
            _moveTarget = GetComponentInChildren<PlayerMoveTarget>();
            _cameraFocus = GetComponentInChildren<CameraFocuser>();
        }

        public void OnInteract()
        {
            _officeInput.DisableInput();
            _moveTarget.MovePlayer(_player);
            _moveTarget.onCompleteMove += StartReadDialog;
            
        }

        private void StartReadDialog()
        {
            _cameraFocus.SetFocus();
            _cameraFocus.onCompleteFocus += ReadDialogOnCompletFocus;
            _moveTarget.onCompleteMove -= StartReadDialog;
        }

        private void ReadDialogOnCompletFocus()
        {
            _dialogPlayer.SetDialog(_dialog.GetDialog(_talkCount++));
            _dialogPlayer.OnDialogEnd += OnCompleteDialog;
            _dialogPlayer.StartDialog();

            _cameraFocus.onCompleteFocus -= ReadDialogOnCompletFocus;
        }


        private void OnCompleteDialog()
        {
            _officeInput.EnableInput();
            _cameraFocus.ResetFocus();
            _dialogPlayer.OnDialogEnd -= OnCompleteDialog;
        }


        public override void OnPlayerInteract()
        {
            _player.AddInteract(OnInteract);
        }

        public override void OnPlayerInteractExit()
        {
            _player.RemoveInteract(OnInteract);
        }

        public void Init(OfficeNPCSO npcInfo)
        {
            _dialog = npcInfo;
            _talkCount = 0;
        }
    }
}
