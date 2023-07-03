using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float delay = 1f;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player") StartCoroutine(LoadnextLevel());
    }

        IEnumerator LoadnextLevel(){
            yield return new WaitForSecondsRealtime(delay);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if(currentSceneIndex + 1 == SceneManager.sceneCountInBuildSettings) currentSceneIndex = -1;

            FindObjectOfType<ScenePersist>().resetScenePersist();
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    
}
