using ObjectManage.GimmickObjects;
using UnityEngine;
namespace Combat.Casters
{

    public class EvaporationCaster : MonoBehaviour, ICastable
    {
        [SerializeField] private float _waterUseAmount;
        public void Cast(Collider2D target)
        {
            if (target.TryGetComponent(out IWaterUsable water))
            {
                water.UseWater(_waterUseAmount);
            }
        }


        public void HandleSetData(CasterData data)
        {
        }
    }
}