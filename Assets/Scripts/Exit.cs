using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //prevent bullets from loading next level
        if(other.tag == "Player")
        {
        StartCoroutine(LoadNextLevel());
        }
       
    }

    IEnumerator LoadNextLevel()
    {
        //delay load
        yield return new WaitForSecondsRealtime(1);
         //get index of current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
         //load next scene
        SceneManager.LoadScene(nextSceneIndex);
    }



}
