using Agents;
using UnityEngine;
namespace QuestSystem.QuestTarget
{

    public class QuestTargetAgent : QuestTargetObject
    {
        private Agent _owner;

        private void Awake()
        {
            _owner = GetComponent<Agent>();
            if (_owner == null)
                _owner = GetComponentInParent<Agent>();

            if (_owner == null)
            {

                Debug.Log( $"Qeust Target's owner is null, Target is {transform.root.name}");
                return;
            }

            _owner.OnDieEvent += Complete;

        }

        void OnDestroy()
        {
            if(_owner != null)
                _owner.OnDieEvent -= Complete;
        }






    }
}