using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Verificações")]
    [SerializeField] private bool _isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene("Game"); //current game scene
        }

        if (Input.GetKeyDown(KeyCode.Escape)  && _isGameOver)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    
    
    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() called");
        _isGameOver = true;
    }
    
    

    
}
