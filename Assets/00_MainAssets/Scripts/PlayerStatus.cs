using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MobStatus
{
    [SerializeField] private GameObject _myWeapon;
    [SerializeField] private TimelineControl _myLoseTime;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private PlayerAttack _playerAttack;

    protected override void OnDie()
    {
        base.OnDie();

        //プレイヤーが倒れた時のゲームオーバー処理
        StartCoroutine(GoToGameOverCoroutine());
    }


    protected override void DamegeAction()
    {

        _state = StateEnum.Hit;
        _playerAttack.OnAttackInterruptioned();
        AudioManager.Instance.Play("「きゃっ！」", "SE");
        AudioManager.Instance.Play("打撃8", "SE");
        _animator.SetTrigger("Hit");

    }

    public override void DamegeActionEnd()
    {
        //各キャラで指定
        _state = StateEnum.Nomal;
    }



    private IEnumerator GoToGameOverCoroutine()
    {
        _audioSource.Stop();
        _flagManager.setLose();
        AudioManager.Instance.Play("「もう…だめ…」", "SE");
        _myWeapon.SetActive(false);
        _myLoseTime.PlayTimeline();

        //３秒まってからゲームオーバーシーンへ遷移
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene("ResultScene");

    }






}
