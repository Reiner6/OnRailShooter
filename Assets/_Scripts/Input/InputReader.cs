using KBCore.Refs;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RailShooter.Input
{
    public class InputReader : MonoBehaviour
    {
        public event Action<float> DoubleTap;
        public event Action Fire;

        [SerializeField, Self] PlayerInput playerInput;
        [SerializeField] float doubleTapTime = 0.5f;

        private float lastMovementTime;
        private float lastMovementDirection;
        private InputAction moveAction;
        private InputAction aimAction;
        private InputAction rollAction;
        private InputAction fireAction;
        public Vector2 MoveDirection => moveAction.ReadValue<Vector2>();
        public Vector2 AimDirection => aimAction.ReadValue<Vector2>();
        public float RollDirection => rollAction.ReadValue<float>();

        private void OnValidate() => this.ValidateRefs();

        private void Awake()
        {
            moveAction = playerInput.actions["Move"];
            rollAction = playerInput.actions["Roll"];
            aimAction = playerInput.actions["Aim"];
            fireAction = playerInput.actions["Fire"];

        }

        private void OnEnable()
        {
            moveAction.performed += OnMovePerformed;
            rollAction.performed += OnRollPerformed;
            fireAction.performed += OnFireperformed;
        }
        
        private void OnDisable()
        {
            moveAction.performed -= OnMovePerformed;
            rollAction.performed -= OnRollPerformed;
            fireAction.performed -= OnFireperformed;
        }

        private void OnRollPerformed(InputAction.CallbackContext obj)
        {
            float currentDirection = RollDirection;
            ProcessRollDirection(currentDirection);
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            float currentDirection = MoveDirection.x;
            ProcessRollDirection(currentDirection);
        }

        private void OnFireperformed(InputAction.CallbackContext obj) => Fire?.Invoke();

        private void ProcessRollDirection(float currentDirection)
        {
            if(Time.time - lastMovementTime < doubleTapTime && currentDirection == lastMovementDirection)
            {
                DoubleTap?.Invoke(currentDirection);
            }
            lastMovementTime = Time.time;
            lastMovementDirection = currentDirection;
        }
    }
}
