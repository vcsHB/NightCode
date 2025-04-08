using UnityEngine;

namespace UI.OfficeScene
{

    public class ArmoyPanel : UIPanel
    {
        [SerializeField] private float _activeXPos;
        [SerializeField] private float _disableXPos;
        [SerializeField] private float _moveDuration = 0.2f;

        [SerializeField] private RectTransform _panelTrm;

        protected override void Awake()
        {
            base.Awake();

        }

        public override void Open()
        {
            base.Open();

        }
    }

}