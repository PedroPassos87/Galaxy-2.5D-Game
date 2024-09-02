using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]private Text _scoreText;
    [SerializeField]private Image _livesImg;
    [SerializeField] private Sprite[] _liveSprites;
    
    [Header("Game Over Screen")]
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartText;
    private GameManager _gameManager;
    void Start()
    {
        _scoreText.text = "Score:" + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.Log("Game Manager is null");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score:" + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverScreen();
        }
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.3f);
            _gameOverText.text = " ";
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void GameOverScreen()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
}
