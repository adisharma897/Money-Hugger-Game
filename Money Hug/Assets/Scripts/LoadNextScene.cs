using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    private IEnumerator Start() //Loads the scene 1- Loading scene after waiting for 3sec
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(1);
    }
}
