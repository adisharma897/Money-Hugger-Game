using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoading : MonoBehaviour
{
    [SerializeField]
    private Image ProgressBarFill; //reference of Fill image

    void Start()
    {
        StartCoroutine(LoadAsyncOperation()); //start async operation
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation GameLevel = SceneManager.LoadSceneAsync(2); //create async operation
        while (GameLevel.progress < 1)
        {
            ProgressBarFill.fillAmount = GameLevel.progress; //filling progress bar as per loading progress
            yield return new WaitForEndOfFrame();
        }
    }
    
}
