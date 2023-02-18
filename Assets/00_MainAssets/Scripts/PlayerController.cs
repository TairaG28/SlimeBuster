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

    //キャラの移動速度
    private Vector3 _moveVelocity;

    private PlayerStatus _status;
    private MobAttack _attack;

    float num;
    float numMax = 1.5f;

    private void Start()

    {
        //毎フレームアクセスするので、負荷を下げるためにキャッシュしておく
        _characterController = GetComponent<CharacterController>();

        //Transformもキャッシュすると少しだけ負荷が下がる
        _transform = transform;

        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
       

    }


    private void Update()
    {

        

        /*Debug.Log(_status.GetMyState());*/

        /* Debug.Log(_characterController.isGrounded ? "地上です" : "空中です");*/

        if (Input.GetButtonDown("Fire1"))
        {

         
            //UI操作中はキャラ操作無視
            /*          if (EventSystem.current.IsPointerOverGameObject())
                      {
                          return;
                      }*/
            /* if (!CheckGrounded) { return; }*/

            //Fire1ボタン（デフォルトだとマウス左クリック）で攻撃

            _attack.AttackIfPossible();
           


        }




        //移動可能な状態であれば、ユーザー入力を反映する
        if (_status.IsMovable && !_status.IsAttacking)
        {
          

            //ダッシュ速度
            if (Input.GetButton("Jump")
                /*&&(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Horizontal") != 0)*/)
            {
                moveSpeed = maxSpeed;

            }
            else
            {
                moveSpeed = defSpeed;
            }


            //入力軸による移動処理（慣性を無視しているので、キビキビ動く）
            mX = Input.GetAxis("Horizontal") * moveSpeed;
            mZ = Input.GetAxis("Vertical") * moveSpeed;

            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            Vector3 right = Camera.main.transform.TransformDirection(Vector3.right);
            _moveVelocity = new Vector3((mX * right + mZ * forward).x, _moveVelocity.y, (mX * right + mZ * forward).z);

            //移動方向に向く
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
                AudioManager.Instance.Play("ニュッ2", "SE");
                _moveVelocity.y = jumpPower;
            }
        }
        else
        {
            
            animator.SetBool("Jump", true);

        }

        _moveVelocity.y += Physics.gravity.y * Time.deltaTime;

        //移動処理
        _characterController.Move(_moveVelocity * Time.deltaTime);

        //アニメーションCTRLR　移動処理
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);

        rY += Input.GetAxis("Horizontal R") * camMoveSpeed * Time.deltaTime;
        rX += Input.GetAxis("Vertical R") * camMoveSpeed * Time.deltaTime;

        //カメラリセット
        if (Input.GetButtonDown("Fire2"))
        {

            rY = transform.localEulerAngles.y;
            rX = 0f;

        }

        camTaget.transform.localRotation = Quaternion.Euler(rX, rY, 0f);


        //連続攻撃有効時間判定
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
    /// 地面に接地しているかどうかを調べる
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


    //連撃有効時間
    public void AddComboTime()
    {

        num = numMax;
        /*Debug.Log(num);*/
    }


    public void soundFootSteps()
    {
        AudioManager.Instance.Play("決定ボタンを押す9", "SE");
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
 *      Debug.Log("移動可能か"+_status.IsMovable);
                Debug.Log("攻撃可能か" + _status.IsAttackable);
                Debug.Log("攻撃中か" + _status.IsAttacking);

*Debug.Log("よこ" + Input.GetAxis("Horizontal"));
Debug.Log("たて" + Input.GetAxis("Vertical"));
Debug.Log("カメラ：よこ" + Input.GetAxis("Horizontal R"));
Debug.Log("カメラ：たて" + Input.GetAxis("Vertical R"));
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

    //プレイヤーが倒れた時のゲームオーバー処理
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



    Debug.Log("ｒY" + rY);
Debug.Log("キャラw:" + transform.eulerAngles.y);
Debug.Log("カメラw:" + camTaget.transform.eulerAngles.y);

Debug.Log("キャラl:" + transform.localEulerAngles.y);
Debug.Log("カメラl:" + camTaget.transform.localEulerAngles.y);


Debug.Log("R");
Debug.Log(transform.localRotation.y);
Debug.Log(rY);

camTaget.transform.eulerAngles = new Vector3(camTaget.transform.eulerAngles.x, transform.eulerAngles.y, 0f);

camTaget.transform.transform.LookAt(this.transform);


camTaget.transform.localEulerAngles = new Vector3(camTaget.transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);


Debug.Log("よこ" + Input.GetAxis("Horizontal R"));
Debug.Log("たて" + Input.GetAxis("Vertical R"));

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