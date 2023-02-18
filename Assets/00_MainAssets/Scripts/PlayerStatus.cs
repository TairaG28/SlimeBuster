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

        //�v���C���[���|�ꂽ���̃Q�[���I�[�o�[����
        StartCoroutine(GoToGameOverCoroutine());
    }


    protected override void DamegeAction()
    {

        _state = StateEnum.Hit;
        _playerAttack.OnAttackInterruptioned();
        AudioManager.Instance.Play("�u������I�v", "SE");
        AudioManager.Instance.Play("�Ō�8", "SE");
        _animator.SetTrigger("Hit");

    }

    public override void DamegeActionEnd()
    {
        //�e�L�����Ŏw��
        _state = StateEnum.Nomal;
    }



    private IEnumerator GoToGameOverCoroutine()
    {
        _audioSource.Stop();
        _flagManager.setLose();
        AudioManager.Instance.Play("�u�����c���߁c�v", "SE");
        _myWeapon.SetActive(false);
        _myLoseTime.PlayTimeline();

        //�R�b�܂��Ă���Q�[���I�[�o�[�V�[���֑J��
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene("ResultScene");

    }






}
