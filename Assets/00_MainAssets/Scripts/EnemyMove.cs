using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]

public class EnemyMove : MonoBehaviour
{

    //レイヤーマスク
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] protected FlagManager _flagManager;

    private NavMeshAgent _agent;

    private RaycastHit[] _raycastHits = new RaycastHit[10];

    private EnemyStatus _status;


    void Start()
    {
        //NavMeshAgentを保持しておく
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }



    //ColliderDetectorのonTriggerStayにセットし、衝突判定を受け取るメソッド
    public void OnDetectObject(Collider collider)
    {

        if (!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }

        //検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (collider.CompareTag("Player"))
        {

            //自身とプレイヤーの座標差分を計算する
            var positionDiff = collider.transform.position - transform.position;

            //プレイヤーとの距離を計算する
            var distance = positionDiff.magnitude;
            //プレイヤーへの方向
            var direction = positionDiff.normalized;

            //_raycastHitsにヒットしたcolliderや座標情報などが格納される。
            //RaycastAllとRaycastNonAllocは同等の機能だが、RaycastNonAllocだとメモリにゴミが残らないのでこちらを推奨
            var hitCount =
                Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);

            /*Debug.Log("hitCount:" + hitCount);*/

            if (hitCount == 0)
            {
                //本作のプレイヤーはCharacterControllerを使っていて、Colliderは使っていないのでRaycastはヒットしない
                //つまりヒット数がゼロであれば、プレイヤーとの間に障害物が無いということになる。
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;


            }
            else
            {
                //見失ったら停止する
                _agent.isStopped = true;
            }

        }

    }

    public void PlayMoveSE()
    {
       

        AudioManager.Instance.Play("飲む", "SE");
    }




}
