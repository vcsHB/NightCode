using Unity.VisualScripting;
using UnityEngine;

namespace Dialog
{
    public class ActorInstance : MonoBehaviour
    {
        [SerializeField] private Actor _actor;

        private void OnEnable()
        {
            DialogActorManager.AddActor(_actor.name, _actor);
        }

        private void OnDisable()
        {
            DialogActorManager.RemoveActor(_actor.name, _actor);
        }
    }
}
