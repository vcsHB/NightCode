using Combat;
using UnityEngine;
namespace ObjectManage
{

    public class DummyObject : MonoBehaviour, IGrabable
    {
        public Transform GetTransform => transform;

        public void Grab()
        {
            
        }
    }
}