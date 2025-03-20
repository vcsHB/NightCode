using System;
using UnityEngine;

namespace Cafe
{
    public abstract class CafePlayerInputObject : MonoBehaviour
    {
        public CafeInput input;

        public abstract void Open();
        public abstract void Close();
    }
}
