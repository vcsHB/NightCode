using UnityEngine;
using UnityEngine.Events;
namespace ObjectManage.GimmickObjects.Logics
{

    public class TargetArriveLogic : GimmickLogic
    {
        [SerializeField] private string _targetTag;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_targetTag))
            {
                Solve();

            }
        }


    }
}