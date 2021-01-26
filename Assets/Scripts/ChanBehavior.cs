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

    public GameObject Percentages_;
    public GameObject[] AnimHearts_;
    public GameObject[] miniRPS_;
    public GameObject[] WinDrawLoss_;


    void Awake()
    {
        HeartAmount = 0;
        Affection = 0;
        cardIndex_ = 0;
        Invoke("SetDailyOdds", 0.2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SetDailyOdds();
        }
    }
    public void useAffection()
    {
        Affection--;
    }

    public void addHeart()
    {
        if(HeartAmount + 1 == 3)
        {
            HeartAmount = 0;
            //heart animation
            Affection++;
        }
        else
        {
            HeartAmount++;
        }
    }

    public void PlayCard()
    {
        deactivateAllMinis();
        chooseCard();
        switch (Chan)
        {
            case GameLogistics.chan.Rock:
                if(cardIndex_ == 0){
                    pickedCard_ = 0;
                    miniRPS_[0].SetActive(true);
                }
                else if(cardIndex_ == 1){
                    pickedCard_ = 1;
                    miniRPS_[1].SetActive(true);
                }
                else{
                    pickedCard_ = 2;
                    miniRPS_[2].SetActive(true);
                }
                break;
            case GameLogistics.chan.Paper:
                if (cardIndex_ == 0){
                    pickedCard_ = 1;
                    miniRPS_[1].SetActive(true);
                }
                else if (cardIndex_ == 1){
                    pickedCard_ = 0;
                    miniRPS_[0].SetActive(true);
                }
                else{
                    pickedCard_ = 2;
                    miniRPS_[2].SetActive(true);
                }
                break;
            case GameLogistics.chan.Scissor:
                if (cardIndex_ == 0){
                    pickedCard_ = 2;
                    miniRPS_[2].SetActive(true);
                }
                else if (cardIndex_ == 1){
                    pickedCard_ = 0;
                    miniRPS_[0].SetActive(true);
                }
                else{
                    pickedCard_ = 1;
                    miniRPS_[1].SetActive(true);
                }
                break;
        }
        
    }

    void SetDailyOdds()
    {
        float tmp = Random.Range(35, 85);
        tmp = tmp / 10;
        tmp = Mathf.Round(tmp);
        tmp = tmp * 10;

        if (tmp == 50)
            tmp = 60;
        if (tmp == 70)
            tmp = 60;
        FavorableOdds = (int)tmp;
        tmp = 100 - tmp;
        Odds2 = (int)tmp / 2;
        Odds3 = (int)tmp / 2;
        
        switch(Chan)
        {
            case GameLogistics.chan.Rock:
                SetPercentageNumber(GameLogistics.chan.Rock, (FavorableOdds / 10));
                SetPercentageNumber(GameLogistics.chan.Paper, ((Odds2 / 10)));
                SetPercentageNumber(GameLogistics.chan.Scissor, ((Odds3 / 10)));
                break;
            case GameLogistics.chan.Paper:
                SetPercentageNumber(GameLogistics.chan.Paper, (FavorableOdds / 10));
                SetPercentageNumber(GameLogistics.chan.Rock, ((Odds2 / 10)));
                SetPercentageNumber(GameLogistics.chan.Scissor, ((Odds3 / 10)));
                break;
            case GameLogistics.chan.Scissor:
                SetPercentageNumber(GameLogistics.chan.Scissor, (FavorableOdds / 10));
                SetPercentageNumber(GameLogistics.chan.Paper, ((Odds2 / 10)));
                SetPercentageNumber(GameLogistics.chan.Rock, ((Odds3 / 10)));
                break;
        }

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

    void deactivateAllMinis()
    {
        foreach(var mini in miniRPS_)
        {
            mini.SetActive(false);
        }
    }

    void SetPercentageNumber(GameLogistics.chan type, int number)
    {
        switch(type)
        {
            case GameLogistics.chan.Rock:
                Percentages_.transform.GetChild(0).transform.GetChild(0).GetComponent<NumberScript>().setNumber(number);
                break;
            case GameLogistics.chan.Paper:
                Percentages_.transform.GetChild(1).transform.GetChild(0).GetComponent<NumberScript>().setNumber(number);
                break;
            case GameLogistics.chan.Scissor:
                Percentages_.transform.GetChild(2).transform.GetChild(0).GetComponent<NumberScript>().setNumber(number);
                break;
        }
    }
}
