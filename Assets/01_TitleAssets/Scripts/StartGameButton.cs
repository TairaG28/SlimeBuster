using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviour
{


    Button button;
    [SerializeField] Text message;

    // Start is called before the first frame update
    private void Start()
    {
        button = GetComponent<Button>();

        //  ボタンを押下したときのリスナーを設定する
        button.onClick.AddListener(
            () =>
            {

                /*Debug.Log("押せてる");*/

                //シーン遷移の際にSceneManagerを使用する
                SceneManager.LoadScene("MainScene");
                /*MainScene*/
            }
        );

        StartCoroutine(TextMotion());

    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            AudioManager.Instance.Play("短いビーム音", "SE");
            button.onClick.Invoke();
        }
    }


    private IEnumerator TextMotion()
    {


        while (true)
        {
            message.enabled = !message.enabled;

            yield return new WaitForSeconds(0.5f);


        }
    }
}
