using UnityEngine;

namespace DM
{
    public class CharacterManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
