using UnityEngine;

namespace RailShooter.Camera
{
    public class CameraController : RailFollower
    {
        [SerializeField] private float verticalOffset;
        [SerializeField] private bool updatePosition;
        private void OnValidate()
        {
            if(updatePosition)
            {
                SetOffset();
                SetPositionAndRotation();
                updatePosition = false;
            }
        }

        protected override void SetOffset()
        {
            base.SetOffset();
            offsetCamera += followTarget.up * verticalOffset;
        }
    }
}
