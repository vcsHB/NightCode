using CameraControllers;
using UnityEngine;

namespace Dialog
{
    public class ZoomEvent : DialogEvent
    {
        public float zoomLevel;
        public float duration;
        public bool returnZoomOnEndNode;


        private CameraZoomController zoomController;

        private CameraZoomController ZoomController
        {
            get
            {
                if (zoomController == null) 
                    zoomController = CameraManager.Instance.GetCompo<CameraZoomController>();
                return zoomController;
            }
        }

        public override void PlayEvent(DialogPlayer dialogPlayer, Actor actor)
        {
            base.PlayEvent(dialogPlayer, actor);
            ZoomController.SetZoomLevel(zoomLevel, duration);
            ZoomController.onCompleteZoom += OnCompleteZoom;
        }

        private void OnCompleteZoom()
        {
            isCompleteEvent = true;
            ZoomController.onCompleteZoom -= OnCompleteZoom;
        }
    }
}
