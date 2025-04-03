using UnityEngine;

namespace Cafe
{
    public class ServeFoodInputObject : CafePlayerInputObject
    {
        private CafeTable _table;

        public void Init(CafeTable table)
            => _table = table;

        private void InteractObject()
        {
            _table.OnServingMenu();
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
