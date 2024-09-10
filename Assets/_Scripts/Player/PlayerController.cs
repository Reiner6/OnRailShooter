using DG.Tweening;
using KBCore.Refs;
using RailShooter.Input;
using UnityEngine;

namespace RailShooter.Player
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [SerializeField, Self] private InputReader input;
        [SerializeField] private Transform followTarget;
        [SerializeField] private Transform aimTarget;
        [SerializeField] private Transform playerVisual;
        [SerializeField] private float followDistance = 2f;
        [Header("Movement Control")]
        [SerializeField] private Vector2 movementLimit = new Vector2(2f, 2f);
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float smoothTime = 0.2f;
        [Header("Rotation Control")]
        [SerializeField] private float maxRoll = 15f;
        [SerializeField] private float rollSpeed = 2f;
        [SerializeField] private int numberOfRolls = 2;
        [SerializeField] private float barrelRollDuration = 1f;
        [SerializeField] private Transform modelParent;
        [SerializeField] private float rotationSpeed;

        private Vector3 velocity;
        private float roll;
        private Vector3 offsetLocalPosition;

        private void Awake()
        {
            input.DoubleTap += BarrelRoll;
        }

        private void OnDestroy()
        {
            input.DoubleTap -= BarrelRoll;
        }

        private void Update()
        {
            PlayerMovement(); 
            PlayerRotation();
            PlayerRoll();
        }

        private void PlayerMovement()
        {
            Vector3 targetPos = followTarget.position + followTarget.forward * -followDistance;
            Vector3 localPos = transform.InverseTransformPoint(targetPos);

            if(input.MoveDirection != Vector2.zero)
            {
                offsetLocalPosition.x += input.MoveDirection.x * movementSpeed * Time.deltaTime;
                offsetLocalPosition.y += input.MoveDirection.y * movementSpeed * Time.deltaTime;
                offsetLocalPosition.x = Mathf.Clamp(offsetLocalPosition.x, -movementLimit.x, movementLimit.x);
                offsetLocalPosition.y = Mathf.Clamp(offsetLocalPosition.y, -movementLimit.y, movementLimit.y);
            }
            else
            {
                offsetLocalPosition = Vector2.Lerp(offsetLocalPosition, Vector2.zero, Time.deltaTime * movementSpeed);

            }


            localPos.x += offsetLocalPosition.x;
            localPos.y += offsetLocalPosition.y;

            var newTargetPosition =  transform.TransformPoint(localPos);

           transform.position = Vector3.SmoothDamp(transform.position, newTargetPosition, ref velocity, smoothTime);
             
        }

        private void PlayerRotation()
        {
            Vector3 direction = aimTarget.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            modelParent.rotation = Quaternion.Lerp(modelParent.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void PlayerRoll()
        {
            transform.rotation = followTarget.rotation;
            roll = Mathf.Lerp(roll, input.MoveDirection.x * maxRoll, Time.deltaTime * rollSpeed);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, roll);
        }

        private void BarrelRoll(float direction = -1)
        {
            if(!DOTween.IsTweening(playerVisual))
            {
                playerVisual.DOLocalRotate(new Vector3(playerVisual.localEulerAngles.x, playerVisual.localEulerAngles.y, 360 * numberOfRolls * direction),
                    barrelRollDuration, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic);
            }
        }
    }
}