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

    //ゲージを初期化する
    public void InitialiZe(RectTransform parentRectTransform, Camera camera, MobStatus status)
    {
        //座標の計算に使うパラメータを受け取り、保持しておく
        _parentRectTransform = parentRectTransform;
        _camera = camera;
        _status = status;
        Refresh();

    }


    //ゲージを更新する
    private void Refresh()
    {
        //残りライフを表示する
        _slider.value = _status.Life / _status.LifeMax;

        var cameraTransform = _camera.transform;

        // カメラの向きベクトル
        var cameraDir = cameraTransform.forward;
        // オブジェクトの位置
        var targetWorldPos = _status.transform.position + _worldOffset;
        // カメラからターゲットへのベクトル
        var targetDir = targetWorldPos - cameraTransform.position;

        // 内積を使ってカメラ前方かどうかを判定
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // カメラ前方ならUI表示、後方なら非表示
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


        // オブジェクトのワールド座標→スクリーン座標変換
        var targetScreenPos = _camera.WorldToScreenPoint(targetWorldPos);

        // スクリーン座標変換→UIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentRectTransform,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransformのローカル座標を更新
        this.transform.localPosition = uiLocalPos;

    }


}


/*
         //対象Mobの場所にゲージを移動する
        //World座標やLocal座標を変換するときはRectTransformUtilityを使う
        var screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;

        //今回はCanvasのRenderModeがScreen-Overlayなので第３引数にnullを指定している。
        //ScreenSpace-Cameraの場合は対象カメラを渡す必要がある
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null, out localPoint);

        //ゲージがキャラに重なるので、少し上にずらしている
        transform.localPosition = localPoint + new Vector2(0, 10);

 */