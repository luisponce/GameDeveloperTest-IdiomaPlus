using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCallback : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneController.Instance.GoToMenu();
    }

    public void PlayGame()
    {
        SceneController.Instance.GoToGameplay();
    }
}
