using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{

    public enum FlagEnum
    {
        //�Q�[����
        Nomal,
        //����
        Win,
        //�s�k
        Lose,
        //�f����
        Demo,
       
    }

    //�t���O���
    public FlagEnum _flagEnum = FlagEnum.Nomal;

    public bool IsLose => FlagEnum.Lose == _flagEnum;

    public void setLose()
    {
        _flagEnum = FlagEnum.Lose;
    }



}
