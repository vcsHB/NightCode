using UnityEngine;
namespace Combat.PlayerTagSystem
{
    [CreateAssetMenu(menuName = "SO/PlayerManage/PlayerGroupData")]
    public class PlayerGroupDataSO : ScriptableObject
    {
        public PlayerSO[] playerDatas;

        public PlayerSO GetPlayerData(int id)
        {
            if (playerDatas == null) return null;
            if (id < 0 || id > playerDatas.Length - 1) return null;
            return playerDatas[id];
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < playerDatas.Length; i++)
            {
                playerDatas[i].SetId(i);
            }
        }

#endif
    }
}