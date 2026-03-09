using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class PlayerManager : CharacterManager
    {
        PlayerLocomotionManager player_locomotion_manager;

        protected override void Awake()
        {
            base.Awake();

            player_locomotion_manager = GetComponent<PlayerLocomotionManager>();
        }

        protected override void Update()
        {
            base.Update();

            if (IsOwner)
            {
                player_locomotion_manager.HandleAllMovement();
            }
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            if (IsOwner)
            {
                PlayerCamera.instance.HandleAllCameraActions();
            }
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
            }
        }
    }
}
