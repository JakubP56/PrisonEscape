using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameSession : MonoBehaviour
{
   [SerializeField] int playerLives = 3;
   [SerializeField] int score = 10;
   [SerializeField] TextMeshProUGUI lifeText;
   [SerializeField] TextMeshProUGUI scoreText;

    //display player lives on start
    void Start() 
    {
        lifeText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    void Awake()
    {   
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    //take life if lives still above 1
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        //reset if lives at 0
        else
        {
            ResetGameSession();
        }
    }

    //deduct 1 from life count.reload current scene
    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        //update life and display when player dies
        lifeText.text = playerLives.ToString();
    }

    public void AddScore(int value)
    {
        score+= value;
        scoreText.text = score.ToString();
    }

    //restart at level 0
    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


}
