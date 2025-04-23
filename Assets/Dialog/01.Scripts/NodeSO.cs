using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Toggle;

namespace Dialog
{
    public abstract class NodeSO : ScriptableObject
    {
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        public bool isFirstNode;
        public List<DialogEventSO> dialogEventSO;

        [SerializeReference] public List<DialogEvent> dialogEvents = new();

        private void OnValidate()
        {
            dialogEventSO.ForEach(eventSO =>
            {
                if (eventSO == null) return;

                bool hasDialogEvent = dialogEvents.Any(dialogEvent => dialogEvent.GetType() == eventSO.Type);
                if (hasDialogEvent) return;

                if (eventSO.GetDialogEvent(out DialogEvent dialogEvent))
                    this.dialogEvents.Add(dialogEvent);
            });

            for(int i = 0; i < dialogEvents.Count; i++)
            {
                DialogEventSO eventSO = dialogEventSO.Find(so => so.Type == dialogEvents[i].GetType());

                if (eventSO == null)
                {
                    dialogEvents.RemoveAt(i--);
                    Debug.Log("zxcv");
                }
            }
        }

        public abstract List<TagAnimation> GetAllAnimations();

        public bool IsCompleteEvent()
        {
            bool isComplete = true;



            return isComplete;
        }
    }
}
