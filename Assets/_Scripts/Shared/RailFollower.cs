using UnityEngine;

namespace RailShooter.Camera
{
    public class RailFollower : MonoBehaviour
    {
        [SerializeField] protected Transform player;
        [SerializeField] protected Transform followTarget;
        [SerializeField] private float followDistance = 22f;
        [SerializeField] private float smoothTime = 0.2f;

        protected Vector3 offsetCamera;
        private Vector3 velocity;
        private void Update()
        {
            TargetPosition();
            transform.SetPositionAndRotation(Vector3.SmoothDamp(transform.position, offsetCamera, ref velocity, smoothTime), player.rotation);
        }

        protected virtual void TargetPosition()
        {
            offsetCamera = followTarget.position + followTarget.forward * -followDistance;
        }
    }
}
