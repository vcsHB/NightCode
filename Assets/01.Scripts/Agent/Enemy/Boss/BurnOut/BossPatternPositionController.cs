using UnityEngine;
namespace Agents.Enemies.BossManage
{

    public class BossPatternPositionController : MonoBehaviour
    {
        [SerializeField] private Transform _defaultPos;
        [SerializeField] private Transform[] _sequence1Positions;
        [SerializeField] private Transform[] _steamCoolingPositions;
        [SerializeField] private Transform _sequence3Position;
        [SerializeField] private Transform[] _sequence4Positions;

        public Vector2 GetSequence1Pos()
        {
            return GetRandomTransform(_sequence1Positions).position;
        }

        public Vector2 SteamCoolingPos()
        {
            return GetRandomTransform(_steamCoolingPositions).position;
        }

        public Vector2 GetSequence4Pos()
        {
            return GetRandomTransform(_sequence4Positions).position;
        }

        private Transform GetRandomTransform(Transform[] positionList)
        {
            if (positionList == null || positionList.Length <= 0) return null;
            return positionList[Random.Range(0, positionList.Length)];
        }

    }
}