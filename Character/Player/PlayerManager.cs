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

            if (!IsOwner)
            {
                return;
            }

            player_locomotion_manager.HandleAllMovement();
        }
    }
}
