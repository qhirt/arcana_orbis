using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DM
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        PlayerControls player_controls;

        [SerializeField] Vector2 movement_input;
        [SerializeField] public float vertical_input;
        [SerializeField] public float horizontal_input;
        [SerializeField] public float move_amount;


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
            SceneManager.activeSceneChanged += OnSceneChange;
            instance.enabled = false;
        }

        private void OnEnable()
        {
            if (player_controls == null)
            {
                player_controls = new PlayerControls();

                player_controls.PlayerMovement.Movement.performed += i => movement_input = i.ReadValue<Vector2>();
            }

            player_controls.Enable();
        }

        private void Oestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    player_controls.Enable();
                }
                else
                {
                    player_controls.Disable();
                }
            }
        }

        private void OnSceneChange(Scene old_scene, Scene new_scene)
        {
            if (new_scene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            else
            {
                instance.enabled = false;
            }
        }

        private void Update()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            vertical_input = movement_input.y;
            horizontal_input = movement_input.x;

            move_amount = Mathf.Clamp01(Mathf.Abs(vertical_input) + Mathf.Abs(horizontal_input));

            //  SNAP THE MOVE AMOUNT TO 0.0f, 0.5f, OR 1.0f
            if (move_amount <= 0.5f && move_amount > 0.0f)
            {
                move_amount = 0.5f;
            }
            else if (move_amount > 0.5f && move_amount <= 1.0f)
            {
                move_amount = 1.0f;
            }
            else
            {
                move_amount = 0.0f;
            }
        }
    }
}
