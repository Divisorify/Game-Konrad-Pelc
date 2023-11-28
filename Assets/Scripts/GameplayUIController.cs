using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIController : MonoBehaviour
{
    public GameObject gameOverScreen;

    public void RestartGame() {
        //SceneManager.LoadScene("Gameplay");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeButton() {
        SceneManager.LoadScene("MainMenu");
    }

    public void gameOver()
    {
         gameOverScreen.SetActive(true);
    }
}
