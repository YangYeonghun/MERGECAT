using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public void LoadInGameScene()
    {
        SceneManager.LoadScene("In_Game");
    }
}
