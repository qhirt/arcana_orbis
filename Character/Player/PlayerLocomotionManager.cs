using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;

        public float vertical_movement;
        public float horizontal_movement;
        public float move_amount;

        private Vector3 move_direction;
        private Vector3 target_rotation_direction;

        [SerializeField] float walking_speed = 2.0f;
        [SerializeField] float running_speed = 5.0f;
        [SerializeField] float rotation_speed = 15.0f;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public void HandleAllMovement()
        {
            GetVerticalAndHorizontalInput();
            HandleGroundedMovement();
            HandleRotation();
        }

        private void GetVerticalAndHorizontalInput()
        {
            vertical_movement = PlayerInputManager.instance.vertical_input;
            horizontal_movement = PlayerInputManager.instance.horizontal_input;
        }

        private void HandleRotation()
        {
            target_rotation_direction = PlayerCamera.instance.camera_object.transform.forward * vertical_movement;
            target_rotation_direction += PlayerCamera.instance.camera_object.transform.right * horizontal_movement;

            if (target_rotation_direction == Vector3.zero)
            {
                target_rotation_direction = transform.forward;
            }

            Quaternion new_rotation = Quaternion.LookRotation(target_rotation_direction);
            Quaternion target_rotation = Quaternion.Slerp(transform.rotation, new_rotation, rotation_speed * Time.deltaTime);
            transform.rotation = target_rotation;
            target_rotation.Normalize();
            target_rotation.y = 0.0f;
        }

        private void HandleGroundedMovement()
        {
            //  MOVEMENT IS BASED ON THE DIRECTION THE PLAYER CAMERA IS FACING
            move_direction = PlayerCamera.instance.transform.forward * vertical_movement;
            move_direction += PlayerCamera.instance.transform.right * horizontal_movement;
            move_direction.Normalize();
            move_direction.y = 0.0f;

            if (PlayerInputManager.instance.move_amount > 0.5f)
            {
                //  RUNNING SPEED
                player.character_controller.Move(move_direction * running_speed * Time.deltaTime);
            }
            else if (PlayerInputManager.instance.move_amount <= 0.5f && PlayerInputManager.instance.move_amount > 0.0f)
            {
                //  WALKING SPEED
                player.character_controller.Move(move_direction * walking_speed * Time.deltaTime);
            }
            else
            {
                //  NO MOVEMENT
            }
        }
    }
}
