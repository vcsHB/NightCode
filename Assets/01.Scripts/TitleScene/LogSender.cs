using SoundManage;
using UnityEngine;
namespace TitleScene
{
    [System.Serializable]
    public class LogContent
    {
        public string content;
        public Sprite symbol;
        public Color color;
        public SoundSO recieveSound;
        public float term;
    }
    public class LogSender : MonoBehaviour
    {
        [SerializeField] private LogController _targetController;
        [SerializeField] private LogContent _sendContent;

        public void Send()
        {
            _targetController.SendLog(_sendContent);
        }
    }
}