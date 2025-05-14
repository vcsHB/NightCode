using SoundManage;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialog
{
    public abstract class NodeSO : ScriptableObject
    {
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        public bool isFirstNode;
        public SoundSO textOutSound;

        public List<DialogEventSO> startDialogEventSO;
        [SerializeReference] public List<DialogEvent> startDialogEvent = new();

        [Space]
        public List<DialogEventSO> endDialogEventSO;
        [SerializeReference] public List<DialogEvent> endDialogEvent = new();

        private void OnValidate()
        {
            startDialogEventSO.ForEach(eventSO =>
            {
                if (eventSO == null) return;

                bool hasDialogEvent = startDialogEvent.Any(dialogEvent => dialogEvent.GetType() == eventSO.Type);
                if (hasDialogEvent) return;

                if (eventSO.GetDialogEvent(out DialogEvent dialogEvent))
                    this.startDialogEvent.Add(dialogEvent);
            });

            for (int i = 0; i < startDialogEvent.Count; i++)
            {
                DialogEventSO eventSO = startDialogEventSO.Find(so => so.Type == startDialogEvent[i].GetType());

                if (eventSO == null)
                {
                    startDialogEvent.RemoveAt(i--);
                }
            }

            endDialogEventSO.ForEach(eventSO =>
            {
                if (eventSO == null) return;

                bool hasDialogEvent = endDialogEvent.Any(dialogEvent => dialogEvent.GetType() == eventSO.Type);
                if (hasDialogEvent) return;

                if (eventSO.GetDialogEvent(out DialogEvent dialogEvent))
                    this.endDialogEvent.Add(dialogEvent);
            });

            for (int i = 0; i < endDialogEvent.Count; i++)
            {
                DialogEventSO eventSO = endDialogEventSO.Find(so => so.Type == endDialogEvent[i].GetType());

                if (eventSO == null)
                {
                    endDialogEvent.RemoveAt(i--);
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
