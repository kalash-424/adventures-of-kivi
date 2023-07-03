using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameSession : MonoBehaviour
{
   [SerializeField] int playerLives = 5;
    [SerializeField] int score = 0;

   [SerializeField] TextMeshProUGUI livesText;
   [SerializeField] TextMeshProUGUI scoreText;


    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

   void Start(){
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
   }

   public void scoreIncrement(int points){
        score += points;
        scoreText.text = score.ToString();
   }

    public void processPlayerDeath(){
        if(playerLives > 1){
            TakeLife();
        }
        else{
            ResetGameSession();
        }
    }

    void ResetGameSession(){
        FindObjectOfType<ScenePersist>().resetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife(){
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }
}
