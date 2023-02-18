using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//�G�̏�ԊǗ��X�N���v�g

[RequireComponent(typeof(NavMeshAgent))]

public class BossStatus : EnemyStatus
{
    [SerializeField] private GameObject _myBody;
    [SerializeField] private TimelineControl _myWinTime;
    [SerializeField] private AudioSource _audioSource;

    protected override void MakeGauge()
    {
        /*Debug.Log("�������ł�");*/
        //��������
    }


    protected override void DeleteGauge()
    {
        //��������
    }

    protected override void OnDie()
    {
        base.OnDie();

     }

    protected override void enemyDieAction()
    {


        //�{�X���|�ꂽ���̃Q�[���I�[�o�[����;
        StartCoroutine(GoToGameClearCoroutinePPre());
        
        
    }

    private IEnumerator GoToGameClearCoroutinePPre()
    {
        _audioSource.Stop();
        yield return new WaitForSeconds(3);
        AudioManager.Instance.Play("�Ō�7", "SE");
        Instantiate(dieEffectPrefab, this.transform.position, Quaternion.identity);
        _myBody.SetActive(false);
        /*Destroy(gameObject);*/
        StartCoroutine(GoToGameClearCoroutinePre());
    }


    private IEnumerator GoToGameClearCoroutinePre()
    {
        
        yield return new WaitForSeconds(2);
        StartCoroutine(GoToGameClearCoroutine());
    }



    private IEnumerator GoToGameClearCoroutine()
    {
        
        _myWinTime.PlayTimeline();
        //�R�b�܂��Ă���Q�[���I�[�o�[�V�[���֑J��
        yield return new WaitForSeconds(6);

 
        SceneManager.LoadScene("ResultScene");

        /*Debug.Log("�R���[�`���Ă΂�Ă�H");*/

    }
}
