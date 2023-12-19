using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayUIController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject finishScreen;

    public void RestartGame() {
        SceneManager.LoadScene("Gameplay");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeButton() {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //if (SceneManager.GetActiveScene().Equals("Gameplay"))
            //{
            //    Debug.Log("3");
            //    SceneManager.LoadScene("Level 2");
            //}

            //if (SceneManager.GetActiveScene().Equals("Level 2"))
            //{
            //    SceneManager.LoadScene("Level 3");
            //}
        }
    }

    public void gameOver()
    {
        if (!finishScreen.active) {
            gameOverScreen.SetActive(true);
        } 
        
    }

    public void finish()
    {
        finishScreen.SetActive(true);
    }
}
