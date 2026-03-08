using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace DM
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        [Header("POSITION")]
        public NetworkVariable<Vector3> network_position = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> network_rotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public Vector3 network_position_velocity;
        public float network_position_smooth_time = 0.1f;
        public float network_rotation_smooth_time = 0.1f;
    }
}
