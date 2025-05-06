using Unity.VisualScripting;
using UnityEngine;

namespace Dialog
{
    public class ActorInstance : MonoBehaviour
    {
        [SerializeField] private Actor _actor;

        private void OnEnable()
        {
            DialogActorManager.Instance.AddActor(_actor.name, _actor);
        }

        private void OnDisable()
        {
            if (DialogActorManager.IsDestroyed == false) DialogActorManager.Instance.RemoveActor(_actor.name, _actor);
        }
    }
}
