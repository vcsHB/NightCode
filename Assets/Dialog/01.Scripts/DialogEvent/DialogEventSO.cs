using System;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(fileName = "DialogEventSO", menuName = "SO/Dialog/DialogEventSO")]
    public class DialogEventSO : ScriptableObject
    {
        [Tooltip("class name without namespace")]
        public string className;
        private Type type;

        public Type Type
        {
            get
            {
                if(type == null)
                {
                    try
                    {
                        type = Type.GetType($"Dialog.{className}");
                    }
                    catch
                    {
                        Debug.LogWarning($"Class named {className} is not exsist");
                    }
                }

                return type;
            }
        }


        public bool GetDialogEvent(out DialogEvent uiEvent)
        {
            try
            {
                var eventInstance = Activator.CreateInstance(Type);

                if (eventInstance is DialogEvent)
                {
                    uiEvent = eventInstance as DialogEvent;
                    return true;
                }

                uiEvent = null;
                return false;
            }
            catch
            {
                Debug.LogWarning($"Class named {className} is not exsist");
                uiEvent = null;
                return false;
            }
        }
    }
}
