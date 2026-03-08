using UnityEngine;
using UnityEngine.SceneManagement;

namespace DM
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        PlayerControls player_controls;

        [SerializeField] Vector2 movement_input;

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
    }
}
