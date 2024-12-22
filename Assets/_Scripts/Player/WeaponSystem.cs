using KBCore.Refs;
using RailShooter.Input;
using UnityEngine;

namespace RailShooter.Player
{
    public class WeaponSystem : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private InputReader input;
        [SerializeField] private Transform targetPoint;

        [SerializeField] private float targetDistance = 50f;
        [SerializeField] private float smoothTime = 0.2f;
        [SerializeField] private Vector2 aimLimit = new Vector2(50f, 20f);
        [SerializeField] private float aimSpeed = 10f;
        [SerializeField] private float aimReturnSpeed = 0.2f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;

        Vector3 velocity;
        Vector3 aimOffset;

        private void Awake()
        {
            input.Fire += OnFire;
        }

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            Vector3 targetPosition = transform.position + transform.forward * targetDistance;
            Vector3 localPosition = transform.InverseTransformPoint(targetPosition);

            //if there is aim inpunt
            if(input.AimDirection != Vector2.zero)
            {
                aimOffset += (Vector3)input.AimDirection * (aimSpeed * Time.deltaTime);

                aimOffset.x = Mathf.Clamp(aimOffset.x, -aimLimit.x, aimLimit.x);
                aimOffset.y = Mathf.Clamp(aimOffset.y, -aimLimit.y, aimLimit.y);

            }
            else
            {
                aimOffset = Vector2.Lerp(aimOffset, Vector2.zero, Time.deltaTime * aimReturnSpeed);
            }

            localPosition.x += aimOffset.x;
            localPosition.y += aimOffset.y;

            var desiredPosition = transform.TransformPoint(localPosition);

            targetPoint.position = Vector3.SmoothDamp(targetPoint.position, desiredPosition, ref velocity, smoothTime);
        }

        private void OnFire()
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(targetPoint.position - firePoint.position));
            Destroy(projectile, 5f);
        }

        private void OnDrawGizmos()
        {
            DebugExtension.DrawLocalCube(transform, new Vector3(aimLimit.x, aimLimit.y, 0.5f) * 2f, transform.InverseTransformPoint(transform.position + transform.forward * targetDistance));           
        }
    }
}