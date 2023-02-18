using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//�G�̏�ԊǗ��X�N���v�g

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyStatus : MobStatus
{

    [SerializeField] protected GameObject dieEffectPrefab;

    private NavMeshAgent _agent;
    
    
    protected override void Start()
    {
        base.Start();

        MakeGauge();

        _agent = GetComponent<NavMeshAgent>();

    }

    protected virtual void MakeGauge()
    {
        //���C�t�Q�[�W�̕\�����J�n����
        LifeGaugeContainer.Instance.Add(this);
    }

    // Update is called once per frame
    private void Update()
    {
        //NavMeshAgent��velocity�ňړ����x�̃x�N�g�����擾�ł���
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);

    }

    protected override void OnDie()
    {
        base.OnDie();

        enemyDieAction();

    }

    protected virtual void DeleteGauge()
    {
        //���C�t�Q�[�W�̕\�����I������
        LifeGaugeContainer.Instance.Remove(this);
    }

    protected virtual void enemyDieAction()
    {
        DeleteGauge();
        StartCoroutine(DestroyCoroutine());
    }


    //�|���ꂽ���̏��ŃR���[�`��
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        AudioManager.Instance.Play("�Ō�7", "SE");
        Instantiate(dieEffectPrefab, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
