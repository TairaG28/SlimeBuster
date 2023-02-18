using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ReturnTitleButton : MonoBehaviour
{

    Button button;
    [SerializeField] Text message;

    // Start is called before the first frame update
    private void Start()
    {
        
        button = GetComponent<Button>();

        //  �{�^�������������Ƃ��̃��X�i�[��ݒ肷��
        button.onClick.AddListener(
            () => {

                //�V�[���J�ڂ̍ۂ�SceneManager���g�p����
                SceneManager.LoadScene("TitleScene");
            }
        );

        StartCoroutine(TextMotion());
    }



    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            AudioManager.Instance.Play("�Z���r�[����", "SE");
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
