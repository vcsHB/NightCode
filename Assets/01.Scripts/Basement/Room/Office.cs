using UnityEngine;

namespace Basement
{
    public class Office : BasementRoom
    {
        [SerializeField] private ScaduleFurniture scaduleFurniture;

        protected override void Awake()
        {
            base.Awake();
            scaduleFurniture.Init(this);
        }
    }
}
