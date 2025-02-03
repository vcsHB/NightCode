using Combat;
using UnityEngine;
namespace Agents.Enemies
{

    public class GrabableEnemy : Enemy, IGrabable
    {
        public Transform GetTransform => transform;

        public void Grab()
        {
            // GrabStun 상태로 전환

        }
    }
}