using System.Collections.Generic;
using UnityEngine;
namespace TitleScene
{

    public class LogController : MonoBehaviour
    {
        [SerializeField] private LogSlot _slotPrefab;
        [SerializeField] private Transform _contentTrm;
        private List<LogSlot> _logList = new();
        
        public void SendLog(LogContent content)
        {
            LogSlot log = Instantiate(_slotPrefab, _contentTrm);
            log.SetContent(content);
            _logList.Add(log);
        }

    }
}