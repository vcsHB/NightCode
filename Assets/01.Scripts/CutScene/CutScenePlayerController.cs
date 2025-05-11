using Combat.PlayerTagSystem;
using UnityEngine;
namespace PerformanceSystem.CutScene
{

    public class CutScenePlayerController : MonoBehaviour
    {

        public void SetPlayerPosition(Transform point)
        {
            SetPlayerPosition(point.position);
        }
        public void SetPlayerPosition(Vector2 position)
        {
            PlayerManager.Instance.SetCurrentPlayerPosition(position);
        }
    }
}