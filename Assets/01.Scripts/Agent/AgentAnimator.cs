using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
namespace Agents
{

    public class AgentAnimator : MonoBehaviour
    {
        protected Animator _animator;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();

        }


        
        
    }
}