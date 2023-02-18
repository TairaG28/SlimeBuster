using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] spawnSpots;
    [SerializeField] private GameObject bossBody;
    [SerializeField] private GameObject bossLifeGauge;
    [SerializeField] private Transform enemyParentTransform;
    [SerializeField] private int countEnemys;
    [SerializeField] private GameObject inEffectPrefab;
    [SerializeField] private GameObject inEffectBossPrefab;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private int currentCount;
    private bool bossFlg;

    // Start is called before the first frame update
    private void Start()
    {

        //Coroutineを開始する
        StartCoroutine(SpawnLoop());

        currentCount = 0;
        bossFlg = false;

    }

    private void Update()
    {


        //設定数を出現させて、全滅させたらボス召喚
        if (currentCount >= countEnemys &&
            enemyParentTransform.childCount == 0 &&
            !bossFlg
        )
        {
            BossSpawn();
            bossFlg = true;
        }

    }

    //ボス敵の生成
    public void BossSpawn()
    {
        //コルーチンのインスタンスの有無を確認してストップ
        if (SpawnLoop() != null)
        {
            StopCoroutine(SpawnLoop());

        }

        _audioSource.clip = _audioClip;
        _audioSource.Play();
        AudioManager.Instance.Play("ゴージャスエクスプロージョン", "SE");
        Instantiate(inEffectBossPrefab, bossBody.transform.position, Quaternion.identity);
        bossBody.SetActive(true);
        bossLifeGauge.SetActive(true);

        /*Instantiate(enemyPrefabs[1], spawnSpots[5].transform.position, Quaternion.identity);*/

    }

    //雑魚敵出現のCoroutine
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            currentCount++;

            int numIndex = Random.Range(0, spawnSpots.Length);

            AudioManager.Instance.Play("ゴージャスエクスプロージョン", "SE");
            Instantiate(inEffectPrefab, spawnSpots[numIndex].transform.position, Quaternion.identity);
            GameObject enemey = (GameObject)Instantiate(enemyPrefabs[0], spawnSpots[numIndex].transform.position, Quaternion.identity);

            enemey.transform.parent = enemyParentTransform;


            //10秒待つ
            yield return new WaitForSeconds(5);

            if (playerStatus.Life <= 0 || currentCount >= countEnemys)
            {
                //プレイヤーが倒れたらループを抜ける、または出現数に達したら。
                break;
            }

        }



    }





}

/*
//距離１０のベクトル
var distanceVector = new Vector3(10, 0);

//プレイヤーの位置をベースにした敵の出現位置。

//Y軸に対して上記ベクトルをランダムに0°～360°回転させている
var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;

//敵を出現させたい位置を決定する
var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

//指定座標から一番近いNavMeshの座標を返す

NavMeshHit navMeshHit;

if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
{
    //enemyPrefabを複製、NavMeshAgentは必ずNavMesh上に配置する
    Instantiate(enemyPrefabs[0], navMeshHit.position, Quaternion.identity);
}*/