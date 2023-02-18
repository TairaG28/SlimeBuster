using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]

public class EnemyMove : MonoBehaviour
{

    //���C���[�}�X�N
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] protected FlagManager _flagManager;

    private NavMeshAgent _agent;

    private RaycastHit[] _raycastHits = new RaycastHit[10];

    private EnemyStatus _status;


    void Start()
    {
        //NavMeshAgent��ێ����Ă���
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }



    //ColliderDetector��onTriggerStay�ɃZ�b�g���A�Փ˔�����󂯎�郁�\�b�h
    public void OnDetectObject(Collider collider)
    {

        if (!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }

        //���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        if (collider.CompareTag("Player"))
        {

            //���g�ƃv���C���[�̍��W�������v�Z����
            var positionDiff = collider.transform.position - transform.position;

            //�v���C���[�Ƃ̋������v�Z����
            var distance = positionDiff.magnitude;
            //�v���C���[�ւ̕���
            var direction = positionDiff.normalized;

            //_raycastHits�Ƀq�b�g����collider����W���Ȃǂ��i�[�����B
            //RaycastAll��RaycastNonAlloc�͓����̋@�\�����ARaycastNonAlloc���ƃ������ɃS�~���c��Ȃ��̂ł�����𐄏�
            var hitCount =
                Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);

            /*Debug.Log("hitCount:" + hitCount);*/

            if (hitCount == 0)
            {
                //�{��̃v���C���[��CharacterController���g���Ă��āACollider�͎g���Ă��Ȃ��̂�Raycast�̓q�b�g���Ȃ�
                //�܂�q�b�g�����[���ł���΁A�v���C���[�Ƃ̊Ԃɏ�Q���������Ƃ������ƂɂȂ�B
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;


            }
            else
            {
                //�����������~����
                _agent.isStopped = true;
            }

        }

    }

    public void PlayMoveSE()
    {
       

        AudioManager.Instance.Play("����", "SE");
    }




}
