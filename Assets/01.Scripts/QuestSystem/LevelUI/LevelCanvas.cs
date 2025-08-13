using UnityEngine;
namespace QuestSystem.LevelSystem.LevelUI
{

    public class LevelCanvas : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();

        }

        public virtual void Initialize()
        {
            
        }
    }
}