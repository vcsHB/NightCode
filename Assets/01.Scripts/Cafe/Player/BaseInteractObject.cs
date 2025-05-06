using System;
using UnityEngine;

namespace Base
{
    public abstract class BaseInteractObject : MonoBehaviour
    {
        public BaseInput input;

        public abstract void Open();
        public abstract void Close();
    }
}
