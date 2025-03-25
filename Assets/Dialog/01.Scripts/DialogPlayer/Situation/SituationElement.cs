using UnityEngine;
namespace Dialog.SituationControl
{

    public abstract class SituationElement : MonoBehaviour
    {
        public abstract void StartSituation();
        public abstract void EndSituation(); 
    }
}
