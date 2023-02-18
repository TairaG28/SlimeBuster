using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{

    [SerializeField] private Slider _slider;
    [SerializeField] private Vector3 _worldOffset;

    private RectTransform _parentRectTransform;
    private Camera _camera;
    private MobStatus _status;

    private void Update()
    {
        Refresh();
    }

    //�Q�[�W������������
    public void InitialiZe(RectTransform parentRectTransform, Camera camera, MobStatus status)
    {
        //���W�̌v�Z�Ɏg���p�����[�^���󂯎��A�ێ����Ă���
        _parentRectTransform = parentRectTransform;
        _camera = camera;
        _status = status;
        Refresh();

    }


    //�Q�[�W���X�V����
    private void Refresh()
    {
        //�c�胉�C�t��\������
        _slider.value = _status.Life / _status.LifeMax;

        var cameraTransform = _camera.transform;

        // �J�����̌����x�N�g��
        var cameraDir = cameraTransform.forward;
        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = _status.transform.position + _worldOffset;
        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - cameraTransform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
       /* this.gameObject.SetActive(isFront);*/

        

        /*Debug.Log(isFront);*/

        /*if (!isFront) return;*/

        if (!isFront)
        {
            transform.localScale = new Vector3(transform.localScale.x, 0f, transform.localScale.x);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, 0.125f, transform.localScale.x);
        }


        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        var targetScreenPos = _camera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentRectTransform,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        this.transform.localPosition = uiLocalPos;

    }


}


/*
         //�Ώ�Mob�̏ꏊ�ɃQ�[�W���ړ�����
        //World���W��Local���W��ϊ�����Ƃ���RectTransformUtility���g��
        var screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;

        //�����Canvas��RenderMode��Screen-Overlay�Ȃ̂ő�R������null���w�肵�Ă���B
        //ScreenSpace-Camera�̏ꍇ�͑ΏۃJ������n���K�v������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null, out localPoint);

        //�Q�[�W���L�����ɏd�Ȃ�̂ŁA������ɂ��炵�Ă���
        transform.localPosition = localPoint + new Vector2(0, 10);

 */