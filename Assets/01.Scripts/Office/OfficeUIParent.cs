using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Office
{
    public abstract class OfficeUIParent : MonoBehaviour, IWindowPanel
    {
        public event Action onOpenUI;
        public event Action onCloseUI;

        public OfficeUIParent linkedUI;
        public List<OfficeUIParent> connectedUI;
        public bool isOpenAfterAnimation = false;

        public bool isLinkedUIOpened
        {
            get
            {
                if (linkedUI == null)
                    return false;

                return linkedUI.isOpened;
            }
        }
        public RectTransform RectTrm => transform as RectTransform;
        [HideInInspector] public bool isOpened;

        public abstract void OpenAnimation();
        public abstract void CloseAnimation();

        public virtual void Open()
        {
            isOpened = true;
            connectedUI.ForEach(ui =>
            {
                if (ui.isOpenAfterAnimation) onOpenUI += ui.Open;
                else ui.Open();
            });

            OpenAnimation();
        }

        public virtual void Close()
        {
            if (linkedUI != null && linkedUI.isOpened)
            {
                linkedUI.Close();
                return;
            }

            isOpened = false;

            connectedUI.ForEach(ui =>
            {
                ui.Close();

                if (ui.isOpenAfterAnimation)
                {
                    ui.onCloseUI += CloseAnimation;
                    return;
                }
            });

            CloseAnimation();
        }

        public virtual void CloseAllUI()
        {
            if (linkedUI != null && linkedUI.isOpened)
            {
                linkedUI.Close();

                if(isOpenAfterAnimation)
                {
                    linkedUI.onCloseUI += Close;
                    return;
                }
            }

            connectedUI.ForEach(ui => ui.Close());
            CloseAnimation();
        }

        protected void OnCompleteOpen()
        {
            onOpenUI?.Invoke();
            onOpenUI = null;
        }
        protected void OnCompleteClose()
        {
            onCloseUI?.Invoke();
            onCloseUI = null;
        }
    }
}
