using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DM
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;

        public Camera camera_object;

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
        }
    }
}
