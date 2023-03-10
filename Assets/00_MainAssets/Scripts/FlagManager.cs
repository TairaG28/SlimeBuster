using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{

    public enum FlagEnum
    {
        //ゲーム中
        Nomal,
        //勝利
        Win,
        //敗北
        Lose,
        //デモ中
        Demo,
       
    }

    //フラグ状態
    public FlagEnum _flagEnum = FlagEnum.Nomal;

    public bool IsLose => FlagEnum.Lose == _flagEnum;

    public void setLose()
    {
        _flagEnum = FlagEnum.Lose;
    }



}
