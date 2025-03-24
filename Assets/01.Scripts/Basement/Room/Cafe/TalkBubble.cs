using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class TalkBubble : MonoBehaviour
    {
        public Image foodIcon;
        
        public void SetIcon(Sprite sprite)
            => foodIcon.sprite = sprite;


        public void Open()
        {
            //나중에 애니메이션 같은거 추가해주
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
