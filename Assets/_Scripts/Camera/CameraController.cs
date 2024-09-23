using Unity.VisualScripting;
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
                TargetPosition();
                updatePosition = false;
            }
        }

        protected override void TargetPosition()
        {
            base.TargetPosition();
            offsetCamera += followTarget.up * verticalOffset;
        }
    }
}
