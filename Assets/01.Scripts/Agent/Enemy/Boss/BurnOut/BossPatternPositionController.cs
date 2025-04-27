using System.Collections.Generic;
using UnityEngine;
namespace Agents.Enemies.BossManage
{

    public class BossPatternPositionController : MonoBehaviour
    {
        [SerializeField] private Transform _defaultPos;
        [SerializeField] private Transform[] _sequence1Positions;
        [SerializeField] private Transform[] _sequence2Positions;
        [SerializeField] private Transform[] _steamCoolingPositions;
        [SerializeField] private Transform _sequence3Position;
        [SerializeField] private Transform[] _sequence4Positions;
        [SerializeField] private Transform _coolingPosition;
        [SerializeField] private Transform _flareDefencePosition;

        private void Awake()
        {

        }

        public Vector2 GetStateSequencePosition(BurnOutStateEnum state)
        {
            switch (state)
            {
                case BurnOutStateEnum.Idle:
                    return _defaultPos.position;
                case BurnOutStateEnum.Cooling:
                    return _coolingPosition.position;
                case BurnOutStateEnum.DestroySequence1:
                    return GetSequence1Pos();
                case BurnOutStateEnum.DestroySequence2:
                    return GetSequence2Pos();
                case BurnOutStateEnum.DestroySequence3:
                    return _sequence3Position.position;
                case BurnOutStateEnum.DestroySequence4:
                    return GetSequence4Pos();
                case BurnOutStateEnum.SteamCooling:
                    return GetSteamCoolingPos();

                case BurnOutStateEnum.DefenceSequence:
                    break;
            }
            return _defaultPos.position;
        }
        public Vector2 SteamCoolingPos()
        {
            return GetRandomTransform(_steamCoolingPositions).position;
        }
        public Vector2 GetSequence1Pos()
        {
            return GetRandomTransform(_sequence1Positions).position;
        }
        public Vector2 GetSequence2Pos()
        {
            return GetRandomTransform(_sequence2Positions).position;
        }
        public Vector2 GetSequence4Pos()
        {
            return GetRandomTransform(_sequence4Positions).position;
        }
        public Vector2 GetSteamCoolingPos()
        {
            return GetRandomTransform(_steamCoolingPositions).position;
        }
        public Vector2 GetCoolingPos()
        {
            return _coolingPosition.position;
        }


        private Transform GetRandomTransform(Transform[] positionList)
        {
            if (positionList == null || positionList.Length <= 0) return null;
            return positionList[Random.Range(0, positionList.Length)];
        }

    }
}