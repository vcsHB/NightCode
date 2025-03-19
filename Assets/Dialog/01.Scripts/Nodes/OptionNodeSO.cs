using System;
using System.Collections;
using System.Collections.Generic;
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
            options.ForEach(option =>
            {
                option.optionTxt = option.option;
                option.optionTagAnimations = TagParser.ParseAnimation(ref option.optionTxt);
                option.optionTagAnimations.ForEach(anim =>
                {
                    if (!anim.SetParameter())
                    {
                        Debug.LogError(option.optionTxt);
                    }
                });
            });
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

        public NodeSO nextNode;

        public Option()
        {
            option = "";
            nextNode = null;
        }
    }
}
