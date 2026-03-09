using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;

        public PlayerManager player;

        public Camera camera_object;
        [SerializeField] Transform camera_pivot_transform;

        [Header("CAMERA SETTINGS")]
        private float camera_smooth_speed = 1.0f;
        [SerializeField] float left_and_right_rotation_speed = 220.0f;
        [SerializeField] float up_and_down_rotation_speed = 220.0f;
        [SerializeField] float minimum_pivot = -30.0f;
        [SerializeField] float maximum_pivot = 60.0f;
        [SerializeField] float camera_collision_radius = 0.2f;
        [SerializeField] LayerMask collide_with_layers;

        [Header("CAMERA VALUES")]
        private Vector3 camera_velocity;
        private Vector3 camera_object_position;
        [SerializeField] float left_and_right_look_angle;
        [SerializeField] float up_and_down_look_angle;
        private float default_camera_z_position;
        private float target_camera_z_position;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            default_camera_z_position = camera_object.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                //  FOLLOW THE TARGET
                HandleFollowTarget();
                //  ROTATE AROUND THE TARGET
                HandleRotations();
                //  COLLIDE WITH OBJECTS
                HandleCollisions();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 target_camera_position = Vector3.SmoothDamp(transform.position, player.transform.position, ref camera_velocity, camera_smooth_speed * Time.deltaTime);
            transform.position = target_camera_position;
        }

        private void HandleRotations()
        {
            //  IF LOCKED ON, FORCE ROTATION TOWRDS TARGET
            //  ELSE ROTATE NORMALLY
            left_and_right_look_angle += (PlayerInputManager.instance.camera_horizontal_input * left_and_right_rotation_speed) * Time.deltaTime;
            up_and_down_look_angle -= (PlayerInputManager.instance.camera_vertical_input * up_and_down_rotation_speed) * Time.deltaTime;
            up_and_down_look_angle = Mathf.Clamp(up_and_down_look_angle, minimum_pivot, maximum_pivot);

            Vector3 camera_rotation;
            Quaternion target_rotation;

            camera_rotation = Vector3.zero;
            camera_rotation.y = left_and_right_look_angle;
            target_rotation = Quaternion.Euler(camera_rotation);
            transform.rotation = target_rotation;

            camera_rotation = Vector3.zero;
            camera_rotation.x = up_and_down_look_angle;
            target_rotation = Quaternion.Euler(camera_rotation);
            camera_pivot_transform.localRotation = target_rotation;
        }

        private void HandleCollisions()
        {
            target_camera_z_position = default_camera_z_position;
            RaycastHit hit;
            Vector3 direction = camera_object.transform.position - camera_pivot_transform.position;
            direction.Normalize();

            if (Physics.SphereCast(camera_pivot_transform.position, camera_collision_radius, direction, out hit, Mathf.Abs(target_camera_z_position), collide_with_layers))
            {
                float distance_from_hit = Vector3.Distance(camera_pivot_transform.position, hit.point);
                target_camera_z_position = -(distance_from_hit - camera_collision_radius);
            }

            if (Mathf.Abs(target_camera_z_position) < camera_collision_radius)
            {
                target_camera_z_position = -camera_collision_radius;
            }

            camera_object_position.z = Mathf.Lerp(camera_object.transform.localPosition.z, target_camera_z_position, 0.2f);
            camera_object.transform.localPosition = camera_object_position;
        }
    }
}
