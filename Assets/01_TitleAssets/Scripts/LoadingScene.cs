using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject _loadingUI;
    [SerializeField] private Slider _slider;
    [SerializeField] Text message;



    public void LoadNextScene()
    {

        message.text = "Now Loading";
        _loadingUI.SetActive(true);

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("MainScene");
        while (!async.isDone )
        {

            _slider.value = async.progress;

            yield return null;
            /*yield return new WaitForSeconds(0.5f);*/

            if (message.text.Equals("Now Loading..."))
            {
                message.text = "Now Loading";

            }
            else
            {
                message.text += ".";
            }
            
        }
    }

}