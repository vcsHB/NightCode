using System;
using Ingame.Gimmick;
using UnityEngine;
namespace ObjectManage.GimmickObjects
{

    public class WaterTank : MonoBehaviour
    {
        [SerializeField] private DamageWater _water;
        [SerializeField] private float _maxWaterLevel;
        [SerializeField] private float _minWaterLevel;
        private void Awake()
        {
            _water.OnFillEvent += HandleWaterFilled;
        }

        private void HandleWaterFilled(float amount)
        {
            _water.transform.localPosition = new Vector3(0, Mathf.Clamp(_water.transform.localPosition.y + amount, _minWaterLevel, _maxWaterLevel), 0f);
        }
    }
}