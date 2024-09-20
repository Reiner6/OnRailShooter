using UnityEngine;

namespace RailShooter.Camera
{
    public class CameraController : RailFollower
    {
        [SerializeField] private float verticalOffset;

        protected override void TargetPosition()
        {
            base.TargetPosition();
            offsetCamera += followTarget.up * verticalOffset;
        }
    }
}
