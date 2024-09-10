using UnityEngine;

namespace RailShooter.Camera
{
    public class RailFollower : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform followTarget;
        [SerializeField] private float followDistance = 22f;
        [SerializeField] private float smoothTime = 0.2f;

        Vector3 velocity;

        private void Update()
        {           
            transform.position = Vector3.SmoothDamp(transform.position, TargetPosition(), ref velocity, smoothTime);
            transform.rotation = player.rotation;
        }

        protected virtual Vector3 TargetPosition()
        {
            Vector3 targetPos = followTarget.position + followTarget.forward * -followDistance;

            return targetPos;
        }

    }
}
