using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

namespace DM
{
    public class TitleScreenManager : MonoBehaviour
    {
        public void StartNetworkAsHost()
        {
            try
            {
                NetworkManager.Singleton.StartHost();
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                NetworkManager.Singleton.StartClient();
            }
        }

        public void StartNewGame()
        {
            StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
        }
    }
}
