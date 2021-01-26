using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanBehavior : MonoBehaviour
{
    int HeartAmount;
    [HideInInspector]
    public int Affection;
    [HideInInspector]
    public int pickedCard_;
    private int cardIndex_;
    public GameLogistics.chan Chan;
    int FavorableOdds;
    int Odds2;
    int Odds3;


    void Start()
    {
        HeartAmount = 0;
        Affection = 0;
        cardIndex_ = 0;
        SetDailyOdds();
    }

    public void useAffection()
    {
        Affection--;
    }

    public void PlayCard()
    {
        chooseCard();
        switch (Chan)
        {
            case GameLogistics.chan.Rock:
                if(cardIndex_ == 0){
                    pickedCard_ = 0;
                }else if(cardIndex_ == 1){
                    pickedCard_ = 1;
                }else{
                    pickedCard_ = 2;
                }
                break;
            case GameLogistics.chan.Paper:
                if (cardIndex_ == 0){
                    pickedCard_ = 1;
                }
                else if (cardIndex_ == 1){
                    pickedCard_ = 0;
                }else{
                    pickedCard_ = 2;
                }
                break;
            case GameLogistics.chan.Scissor:
                if (cardIndex_ == 0){
                    pickedCard_ = 2;
                }
                else if (cardIndex_ == 1){
                    pickedCard_ = 0;
                }else{
                    pickedCard_ = 1;
                }
                break;
        }
        
    }

    void SetDailyOdds()
    {
        int tmp = Random.Range(43, 80);
        tmp = tmp / 10;
        Mathf.Round(tmp);
        tmp = tmp * 10;
        FavorableOdds = tmp;
        tmp = 100 - tmp;
        Odds2 = tmp / 2;
        Odds3 = tmp / 2;
    }

    void chooseCard()
    {
        int tmp = Random.Range(1, 101);
        if (tmp <= FavorableOdds)
        {
            cardIndex_ = 0;
            
        }
        else if (tmp <= FavorableOdds + Odds2 && tmp > FavorableOdds)
        {
            cardIndex_ = 1;
        }
        else if (tmp <= FavorableOdds + Odds2 + Odds3 && tmp > FavorableOdds + Odds2)
        {
            cardIndex_ = 2;
        }
    }
}
