using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{

    public enum FlagEnum
    {
        //ƒQ[ƒ€’†
        Nomal,
        //Ÿ—˜
        Win,
        //”s–k
        Lose,
        //ƒfƒ‚’†
        Demo,
       
    }

    //ƒtƒ‰ƒOó‘Ô
    public FlagEnum _flagEnum = FlagEnum.Nomal;

    public bool IsLose => FlagEnum.Lose == _flagEnum;

    public void setLose()
    {
        _flagEnum = FlagEnum.Lose;
    }



}
