using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//敵の状態管理スクリプト

[RequireComponent(typeof(NavMeshAgent))]

public class BossStatus : EnemyStatus
{
    [SerializeField] private GameObject _myBody;
    [SerializeField] private TimelineControl _myWinTime;
    [SerializeField] private AudioSource _audioSource;

    protected override void MakeGauge()
    {
        /*Debug.Log("未処理です");*/
        //処理無し
    }


    protected override void DeleteGauge()
    {
        //処理無し
    }

    protected override void OnDie()
    {
        base.OnDie();

     }

    protected override void enemyDieAction()
    {


        //ボスが倒れた時のゲームオーバー処理;
        StartCoroutine(GoToGameClearCoroutinePPre());
        
        
    }

    private IEnumerator GoToGameClearCoroutinePPre()
    {
        _audioSource.Stop();
        yield return new WaitForSeconds(3);
        AudioManager.Instance.Play("打撃7", "SE");
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
        //３秒まってからゲームオーバーシーンへ遷移
        yield return new WaitForSeconds(6);

 
        SceneManager.LoadScene("ResultScene");

        /*Debug.Log("コルーチン呼ばれてる？");*/

    }
}
