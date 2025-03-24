using System;
using System.Collections.Generic;
using UI;
using UnityEditor.Compilation;
using UnityEngine;

namespace Basement
{
    public abstract class BasementUI : MonoBehaviour, IWindowPanel
    {
        public Action onCompleteOpen;
        public Action onCompleteClose;

        // 같이 켜지고, 같이 꺼질 UI
        public List<BasementUI> connectedUIList = new List<BasementUI>();
        // 연결된 UI임: linkedUI가 켜져있으면 이 UI를 꺼도 linkedUI만 꺼짐
        public BasementUI linkedUI;
        // 반대 UI: 이 UI끄면 저게 켜짐
        public BasementUI oppositeUI;

        private bool _isOpenCloseByOpposite = false;


        [SerializeField] protected bool _openCloseAfterAnim;

        #region Property

        public bool OpenCloseAfterAnim => _openCloseAfterAnim;
        public bool isOpend { get; protected set; } = false;
        public bool isLinkedUIOpend 
        { 
            get 
            {
                if (linkedUI == null) return false;
                return linkedUI.isOpend;
            }
        }

        #endregion

        protected abstract void OpenAnimation();
        protected abstract void CloseAnimation();

        protected virtual void OnCompleteCloseAction()
        {
            onCompleteClose?.Invoke();
            onCompleteClose = null;
        }

        protected virtual void OnCompleteOpenAction()
        {
            onCompleteOpen?.Invoke();
            onCompleteOpen = null;
        }

        public virtual void Open()
        {
            // 이미 열려 있으면 RETURN
            if (isOpend) return;

            isOpend = true;

            // 연동된 UI 함께 열기
            if (connectedUIList != null)
            {
                connectedUIList.ForEach(ui =>
                {
                    if (ui.OpenCloseAfterAnim) onCompleteOpen += ui.Open;
                    else ui.Open();
                });
            }


            //반대쪽 UI 끄기
            if (oppositeUI != null)
            {
                //반대쪽에서 신호를 받아서 끄는거면 안해
                if (_isOpenCloseByOpposite) _isOpenCloseByOpposite = false;
                else
                {
                    oppositeUI.OpenCloseByOpposite();

                    if (oppositeUI.OpenCloseAfterAnim) onCompleteOpen += oppositeUI.CloseAllUI;
                    else oppositeUI.CloseAllUI();
                }
            }

            //여는 애니메이션
            OpenAnimation();
        }

        public virtual void Close()
        {
            // 닫혀있으면 RETURN
            if (isOpend == false) return;

            // 연결되있는 UI를 먼저 확인해서 연결된 UI만 꺼줘
            if (linkedUI != null && linkedUI.isOpend)
            {
                linkedUI.Close();
                return;
            }

            isOpend = false;

            // 연동된 UI 함께 꺼줘
            if (connectedUIList != null)
            {
                connectedUIList.ForEach(ui =>
                {
                    if (ui.OpenCloseAfterAnim) onCompleteClose += ui.Close;
                    else ui.Close();
                });
            }


            // 반대쪽은 켜줘
            if (oppositeUI != null)
            {
                if (_isOpenCloseByOpposite) _isOpenCloseByOpposite = false;
                else
                {
                    oppositeUI.OpenCloseByOpposite();

                    if (oppositeUI.OpenCloseAfterAnim) onCompleteClose += oppositeUI.Open;
                    else oppositeUI.Open();
                }
            }


            CloseAnimation();
        }

        /// <summary>
        /// Close All UI that connected and linked
        /// opposite UI will be opend
        /// </summary>
        public virtual void CloseAllUI()
        {
            if (linkedUI != null && linkedUI.isOpend)
            {
                if(linkedUI.OpenCloseAfterAnim)
                {
                    linkedUI.onCompleteClose += Close;
                    linkedUI.Close();
                    return;
                }
                linkedUI.Close();
            }

            isOpend = false;
            if (connectedUIList != null) connectedUIList.ForEach(ui => ui.Close());

            if (oppositeUI != null)
            {
                if (_isOpenCloseByOpposite) _isOpenCloseByOpposite = false;
                else
                {
                    oppositeUI.OpenCloseByOpposite();
                    if(oppositeUI.OpenCloseAfterAnim)
                    {
                        onCompleteClose += oppositeUI.Open;
                    }
                    else
                    {
                        oppositeUI.Open();
                    }
                }
            }

            CloseAnimation();
        }

        public virtual void SetOppositeUI(BasementUI basementUI)
        {
            if (basementUI == null)
            {
                Debug.LogError("BasementUI you want to set opposite is null\nIf you want to remove oppositeUI use RemovOppositeUI function");
                return;
            }

            if (isOpend && basementUI.isOpend) basementUI.Close();
            if (!isOpend && !basementUI.isOpend) Open();

            oppositeUI = basementUI;
            if (basementUI.oppositeUI != this) basementUI.SetOppositeUI(this);
        }

        public void RemoveOppositeUI(bool closeOpositeUI = false)
        {
            if (oppositeUI == null) return;

            oppositeUI.oppositeUI = null;
            if (closeOpositeUI) oppositeUI.Close();
            oppositeUI = null;
        }

        public virtual void SetUILink(BasementUI basementUI)
        {
            if (basementUI == null)
            {
                Debug.LogError("BasementUI you want to link is null\nIf you want to remove oppositeUI use RemovLinkedUI function");
                return;
            }

            if (linkedUI != null) linkedUI.Close();
            linkedUI = basementUI;
        }

        public void RemoveLinkedUI(bool closeLinkedUI = false)
        {
            if (oppositeUI == null)
            {
                Debug.LogWarning("There is no linkedUI but you still trying to remove it");
                return;
            }

            if (closeLinkedUI) linkedUI.Close();
            linkedUI = null;
        }

        private void OpenCloseByOpposite() => _isOpenCloseByOpposite = true;
    }
}
