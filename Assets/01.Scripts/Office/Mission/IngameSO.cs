using UnityEngine;

namespace Office
{
    [CreateAssetMenu(menuName = "SO/Stage/Ingame")]
    public class IngameSO : StageSO
    {
        public string stageName;
        public Sprite stageIcon;

        [TextArea(5, 20)]
        public string stageExplain;
        //보상에 대한 정보?
    }
}
