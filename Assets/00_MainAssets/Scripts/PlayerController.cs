using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private float defSpeed = 0.3f;
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 3;
    [SerializeField] private GameObject camTaget;
    [SerializeField] private float camMoveSpeed = 1;
    [SerializeField] private GameObject _myWeapon;

    float rX = 0;
    float rY = 0;

    float mX = 0;
    float mZ = 0;

    /*[SerializeField] private GameObject axePrefab;*/

    private CharacterController _characterController;

    private Transform _transform;

    //�L�����̈ړ����x
    private Vector3 _moveVelocity;

    private PlayerStatus _status;
    private MobAttack _attack;

    float num;
    float numMax = 1.5f;

    private void Start()

    {
        //���t���[���A�N�Z�X����̂ŁA���ׂ������邽�߂ɃL���b�V�����Ă���
        _characterController = GetComponent<CharacterController>();

        //Transform���L���b�V������Ə����������ׂ�������
        _transform = transform;

        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
       

    }


    private void Update()
    {

        

        /*Debug.Log(_status.GetMyState());*/

        /* Debug.Log(_characterController.isGrounded ? "�n��ł�" : "�󒆂ł�");*/

        if (Input.GetButtonDown("Fire1"))
        {

         
            //UI���쒆�̓L�������얳��
            /*          if (EventSystem.current.IsPointerOverGameObject())
                      {
                          return;
                      }*/
            /* if (!CheckGrounded) { return; }*/

            //Fire1�{�^���i�f�t�H���g���ƃ}�E�X���N���b�N�j�ōU��

            _attack.AttackIfPossible();
           


        }




        //�ړ��\�ȏ�Ԃł���΁A���[�U�[���͂𔽉f����
        if (_status.IsMovable && !_status.IsAttacking)
        {
          

            //�_�b�V�����x
            if (Input.GetButton("Jump")
                /*&&(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Horizontal") != 0)*/)
            {
                moveSpeed = maxSpeed;

            }
            else
            {
                moveSpeed = defSpeed;
            }


            //���͎��ɂ��ړ������i�����𖳎����Ă���̂ŁA�L�r�L�r�����j
            mX = Input.GetAxis("Horizontal") * moveSpeed;
            mZ = Input.GetAxis("Vertical") * moveSpeed;

            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
            _moveVelocity = new Vector3((mX * right + mZ * forward).x, _moveVelocity.y, (mX * right + mZ * forward).z);

            //�ړ������Ɍ���
            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));

        }
        else
        {

            _moveVelocity.x = 0;
            _moveVelocity.z = 0;

        }

        if (CheckGrounded )
        {

            Debug.Log(transform.position.y);

            animator.SetBool("Jump", false);
            if (Input.GetButtonDown("Jump"))
            {
                AudioManager.Instance.Play("�j���b2", "SE");
                _moveVelocity.y = jumpPower;
            }
        }
        else
        {
            
            animator.SetBool("Jump", true);

        }

        _moveVelocity.y += Physics.gravity.y * Time.deltaTime;

        //�ړ�����
        _characterController.Move(_moveVelocity * Time.deltaTime);

        //�A�j���[�V����CTRLR�@�ړ�����
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);

        rY += Input.GetAxis("Horizontal R") * camMoveSpeed * Time.deltaTime;
        rX += Input.GetAxis("Vertical R") * camMoveSpeed * Time.deltaTime;

        //�J�������Z�b�g
        if (Input.GetButtonDown("Fire2"))
        {

            rY = transform.localEulerAngles.y;
            rX = 0f;

        }

        camTaget.transform.localRotation = Quaternion.Euler(rX, rY, 0f);


        //�A���U���L�����Ԕ���
        num -= Time.deltaTime;
        if (num <= 0)
        {
            num = 0;
            animator.SetFloat("IsAttackFirstTest", 0);
        }
        else
        {
            animator.SetFloat("IsAttackFirstTest", num);
        }

      

    }


    /// <summary>
    /// �n�ʂɐڒn���Ă��邩�ǂ����𒲂ׂ�
    /// </summary>
    public bool CheckGrounded
    {
        get
        {
            var ray = new Ray(_transform.position + new Vector3(0, 0.1f), Vector3.down);
            var raycastHits = new RaycastHit[1];
            var hitCount = Physics.RaycastNonAlloc(ray, raycastHits, 0.2f);
            return hitCount >= 1;
        }
    }


    //�A���L������
    public void AddComboTime()
    {

        num = numMax;
        /*Debug.Log(num);*/
    }


    public void soundFootSteps()
    {
        AudioManager.Instance.Play("����{�^��������9", "SE");
    }



}


/*        if (Input.GetKey(KeyCode.G))
        {
            _myWeapon.SetActive(false);
            animator.SetTrigger("Win");
        }*/


/*
 * 
 * 
 * 
 * 
 *      Debug.Log("�ړ��\��"+_status.IsMovable);
                Debug.Log("�U���\��" + _status.IsAttackable);
                Debug.Log("�U������" + _status.IsAttacking);

*Debug.Log("�悱" + Input.GetAxis("Horizontal"));
Debug.Log("����" + Input.GetAxis("Vertical"));
Debug.Log("�J�����F�悱" + Input.GetAxis("Horizontal R"));
Debug.Log("�J�����F����" + Input.GetAxis("Vertical R"));
*
*
*
*  if (Input.GetKey(KeyCode.H))
{
    animator.SetTrigger("Hit");
}

if (Input.GetKey(KeyCode.J))
{
    animator.SetTrigger("Die");

    //�v���C���[���|�ꂽ���̃Q�[���I�[�o�[����
    StartCoroutine(GoToGameOverCoroutine());
}
*
*
*
*
if (!Input.GetKey(KeyCode.DownArrow) &&
!Input.GetKey(KeyCode.UpArrow) &&
!Input.GetKey(KeyCode.LeftArrow) &&
!Input.GetKey(KeyCode.RightArrow))



    Debug.Log("��Y" + rY);
Debug.Log("�L����w:" + transform.eulerAngles.y);
Debug.Log("�J����w:" + camTaget.transform.eulerAngles.y);

Debug.Log("�L����l:" + transform.localEulerAngles.y);
Debug.Log("�J����l:" + camTaget.transform.localEulerAngles.y);


Debug.Log("R");
Debug.Log(transform.localRotation.y);
Debug.Log(rY);

camTaget.transform.eulerAngles = new Vector3(camTaget.transform.eulerAngles.x, transform.eulerAngles.y, 0f);

camTaget.transform.transform.LookAt(this.transform);


camTaget.transform.localEulerAngles = new Vector3(camTaget.transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);


Debug.Log("�悱" + Input.GetAxis("Horizontal R"));
Debug.Log("����" + Input.GetAxis("Vertical R"));

if (Input.GetKey(KeyCode.LeftArrow))
{
    rY += 1f * camMoveSpeed * Time.deltaTime;

}

if (Input.GetKey(KeyCode.RightArrow))
{
    rY -= 1f * camMoveSpeed * Time.deltaTime;

}

if (Input.GetKey(KeyCode.UpArrow))
{
    rX += 1f * camMoveSpeed * Time.deltaTime;

}

if (Input.GetKey(KeyCode.DownArrow))
{
    rX -= 1f * camMoveSpeed * Time.deltaTime;
}*/