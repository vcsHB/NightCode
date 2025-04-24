using UnityEngine;

namespace Cafe
{
    public class ServeFoodInputObject : CafePlayerInputObject
    {
        private CafeSit _table;

        public void Init(CafeSit table)
            => _table = table;

        private void InteractObject()
        {
            _table.ServeByPlayer();
            Close();
        }

        public override void Open()
        {
            input.onInteract += InteractObject;
            gameObject.SetActive(true);
        }
        public override void Close()
        {
            input.onInteract -= InteractObject;
            gameObject.SetActive(false);
        }
    }
}
