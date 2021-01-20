using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum chan{ 
        Rock,
        Paper,
        Scissor
    };

    public GameObject[] Chans;
    public static GameManager instance_;
    void Start()
    {
        instance_ = this;
    }

    public static GameObject GetSpecificChan(chan type)
    {
        switch (type)
        {
            case chan.Rock:
                return instance_.Chans[0];
            case chan.Paper:
                return instance_.Chans[1];
            case chan.Scissor:
                return instance_.Chans[2];
            default:
                return null;
        }
    }

    
    
    public void AddPlayerCard(chan type)
    {

    }
}
