using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanBehavior : MonoBehaviour
{
    int HeartAmount;
    int Affection;
    GameManager.chan Chan;

    void Start()
    {
        HeartAmount = 0;
        Affection = 0;
    }


    void Update()
    {
        
    }

    void useAffection()
    {
        Affection--;
    }
    void PlayCard()
    {

    }

}
