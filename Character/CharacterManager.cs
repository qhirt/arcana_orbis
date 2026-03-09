using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace DM
{
    public class CharacterManager : NetworkBehaviour
    {
        public CharacterController character_controller;

        CharacterNetworkManager character_network_manager;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);

            character_controller = GetComponent<CharacterController>();
            character_network_manager = GetComponent<CharacterNetworkManager>();
        }

        protected virtual void Update()
        {
            if (IsOwner)
            {
                character_network_manager.network_position.Value = transform.position;
                character_network_manager.network_rotation.Value = transform.rotation;
            }
            else
            {
                transform.position = Vector3.SmoothDamp
                    (transform.position,
                    character_network_manager.network_position.Value,
                    ref character_network_manager.network_position_velocity,
                    character_network_manager.network_position_smooth_time);

                transform.rotation = Quaternion.Slerp
                    (transform.rotation,
                    character_network_manager.network_rotation.Value,
                    character_network_manager.network_rotation_smooth_time);
            }
        }

        protected virtual void LateUpdate()
        {

        }
    }
}
