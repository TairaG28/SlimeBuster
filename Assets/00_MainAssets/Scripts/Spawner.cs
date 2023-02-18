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

        //Coroutine���J�n����
        StartCoroutine(SpawnLoop());

        currentCount = 0;
        bossFlg = false;

    }

    private void Update()
    {


        //�ݒ萔���o�������āA�S�ł�������{�X����
        if (currentCount >= countEnemys &&
            enemyParentTransform.childCount == 0 &&
            !bossFlg
        )
        {
            BossSpawn();
            bossFlg = true;
        }

    }

    //�{�X�G�̐���
    public void BossSpawn()
    {
        //�R���[�`���̃C���X�^���X�̗L�����m�F���ăX�g�b�v
        if (SpawnLoop() != null)
        {
            StopCoroutine(SpawnLoop());

        }

        _audioSource.clip = _audioClip;
        _audioSource.Play();
        AudioManager.Instance.Play("�S�[�W���X�G�N�X�v���[�W����", "SE");
        Instantiate(inEffectBossPrefab, bossBody.transform.position, Quaternion.identity);
        bossBody.SetActive(true);
        bossLifeGauge.SetActive(true);

        /*Instantiate(enemyPrefabs[1], spawnSpots[5].transform.position, Quaternion.identity);*/

    }

    //�G���G�o����Coroutine
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            currentCount++;

            int numIndex = Random.Range(0, spawnSpots.Length);

            AudioManager.Instance.Play("�S�[�W���X�G�N�X�v���[�W����", "SE");
            Instantiate(inEffectPrefab, spawnSpots[numIndex].transform.position, Quaternion.identity);
            GameObject enemey = (GameObject)Instantiate(enemyPrefabs[0], spawnSpots[numIndex].transform.position, Quaternion.identity);

            enemey.transform.parent = enemyParentTransform;


            //10�b�҂�
            yield return new WaitForSeconds(5);

            if (playerStatus.Life <= 0 || currentCount >= countEnemys)
            {
                //�v���C���[���|�ꂽ�烋�[�v�𔲂���A�܂��͏o�����ɒB������B
                break;
            }

        }



    }





}

/*
//�����P�O�̃x�N�g��
var distanceVector = new Vector3(10, 0);

//�v���C���[�̈ʒu���x�[�X�ɂ����G�̏o���ʒu�B

//Y���ɑ΂��ď�L�x�N�g���������_����0���`360����]�����Ă���
var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;

//�G���o�����������ʒu�����肷��
var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

//�w����W�����ԋ߂�NavMesh�̍��W��Ԃ�

NavMeshHit navMeshHit;

if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
{
    //enemyPrefab�𕡐��ANavMeshAgent�͕K��NavMesh��ɔz�u����
    Instantiate(enemyPrefabs[0], navMeshHit.position, Quaternion.identity);
}*/