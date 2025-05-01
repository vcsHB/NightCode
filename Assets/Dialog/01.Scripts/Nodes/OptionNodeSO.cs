using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialog
{
    public class OptionNodeSO : NodeSO
    {
        public List<Option> options = new List<Option>();
        public OptionButton optionPf;

        public Action OnOptionChange;

        public void AddOption()
        {
            options.Add(new Option());
            OnOptionChange?.Invoke();
        }

        public void AddOption(NodeSO nextNode, int index)
        {
            options[index].nextNode = nextNode;
            OnOptionChange?.Invoke();
        }

        public void RemoveOption(int idx)
        {
            options.RemoveAt(idx);
            OnOptionChange?.Invoke();
        }

        public void RemoveEdge(int idx)
        {
            options[idx].nextNode = null;
        }

        public void RemoveOption(NodeSO nextNode)
        {
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].nextNode == nextNode)
                {
                    options[i] = null;
                    break;
                }
            }

            OnOptionChange?.Invoke();
        }

        private void OnEnable()
        {
            options.ForEach(option => option.Init());
        }

        public override List<TagAnimation> GetAllAnimations()
        {
            List<TagAnimation> tagAnimations = new List<TagAnimation>();

            for (int i = 0; i < options.Count; i++)
            {
                options[i].optionTagAnimations.ForEach(anim => tagAnimations.Add(anim));
            }

            return tagAnimations;
        }
    }

    [Serializable]
    public class Option
    {
        public string option;
        [HideInInspector] public string optionTxt;
        public List<TagAnimation> optionTagAnimations = new();


        public List<DialogEventSO> startDialogEventSO;
        [SerializeReference] public List<DialogEvent> startDialogEvent = new();

        [Space]
        public List<DialogEventSO> endDialogEventSO;
        [SerializeReference] public List<DialogEvent> endDialogEvent = new();

        [HideInInspector]public NodeSO nextNode;

        public Option()
        {
            option = "";
            nextNode = null;
        }

        public void Init()
        {
           optionTxt = option;
           optionTagAnimations = TagParser.ParseAnimation(ref optionTxt);
           optionTagAnimations.ForEach(anim =>
            {
                if (!anim.SetParameter())
                {
                    Debug.LogError(optionTxt);
                }
            });

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
    }
}
