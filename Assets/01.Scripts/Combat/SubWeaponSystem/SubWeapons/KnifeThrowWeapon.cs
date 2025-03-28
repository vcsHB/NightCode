using System.Collections;
using Combat.SubWeaponSystem.WepaonObjects;
using UnityEngine;
namespace Combat.SubWeaponSystem.Weapons
{

    public class KnifeThrowWeapon : WeaponObjectPoolingWeapon
    {
        [SerializeField] private int _throwAmount;
        [SerializeField] private float _throwTerm;
        [SerializeField] private float _throwSpeed = 30f;

        private WaitForSeconds _waitForTerm;


        private void Awake()
        {
            _waitForTerm = new WaitForSeconds(_throwTerm);
        }
        public override void UseWeapon(SubWeaponControlData data)
        {
            data.speed = _throwSpeed;
            base.UseWeapon(data);
            StartCoroutine(ThrowCoroutine(data));
        }

        private IEnumerator ThrowCoroutine(SubWeaponControlData data)
        {
            for (int i = 0; i < _throwAmount; i++)
            {
                ThrowingKnife knife = GetNewWeaponObject() as ThrowingKnife;
                knife.transform.position = transform.position;
                knife.UseWeapon(data);
                yield return _waitForTerm;
            }

        }

    }
}