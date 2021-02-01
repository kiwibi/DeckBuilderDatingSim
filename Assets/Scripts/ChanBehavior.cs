using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanBehavior : MonoBehaviour
{
    [HideInInspector]
    public int HeartAmount;
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
    public GameObject AffectionNumber_;
    public GameObject[] AnimHearts_;
    public GameObject[] miniRPS_;
    public GameObject[] WinDrawLoss_;


    void Awake()
    {
        HeartAmount = 0;
        Affection = 0;
        cardIndex_ = 0;
        Invoke("SetDailyOdds", 0.2f);
        AffectionNumber_.transform.GetChild(0).GetComponent<NumberScript>().setNumber(0);
        AffectionNumber_.transform.GetChild(1).GetComponent<NumberScript>().setNumber(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //StartCoroutine("heartAnimation");
        }
    }
    public void useAffection()
    {
        Affection--;
        //ActiveCardNumbers_[0].transform.GetChild(0).GetComponent<NumberScript>().setNumber(((number / 10) % 10));
        AffectionNumber_.transform.GetChild(1).GetComponent<NumberScript>().setNumber(Affection);
    }

    public void addHeart(int amount)
    {
        if (HeartAmount + amount == 3)
        {
            HeartAmount = 0;
            StartCoroutine("heartAnimation");
            HandleHeartVisual();
            Affection++;
            if(Affection != 10)
                AffectionNumber_.transform.GetChild(1).GetComponent<NumberScript>().setNumber(Affection);
            else
            {
                AffectionNumber_.transform.GetChild(1).GetComponent<NumberScript>().setNumber(1);
                AffectionNumber_.transform.GetChild(1).GetComponent<NumberScript>().setNumber(0);
                GameLogistics.reset();
            }
        }
        else
        {
            if(HeartAmount == 0 && amount == -1)
            {
                return;
            }
            else
                HeartAmount += amount;

            HandleHeartVisual();
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

    public void deactivateAllMinis()
    {
        foreach(var mini in miniRPS_)
        {
            mini.SetActive(false);
        }
    }

    void activateAllMinis()
    {
        foreach (var mini in miniRPS_)
        {
            mini.SetActive(true);
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

    public void dayReset()
    {
        Affection = 0;
        HeartAmount = 0;
        activateAllMinis();
        SetDailyOdds();
        HandleHeartVisual();
        AffectionNumber_.transform.GetChild(1).GetComponent<NumberScript>().setNumber(0);
        //update number and visuals
    }

    public void HandleHeartVisual()
    {
        switch (HeartAmount)
        {
            case 0:
                foreach(var heart in AnimHearts_)
                {
                    heart.SetActive(false);
                }
                break;
            case 1:
                AnimHearts_[0].SetActive(true);
                AnimHearts_[1].SetActive(false);
                AnimHearts_[2].SetActive(false);
                break;
            case 2:
                AnimHearts_[0].SetActive(true);
                AnimHearts_[1].SetActive(true);
                AnimHearts_[2].SetActive(false);
                break;
            case 3:
                AnimHearts_[0].SetActive(true);
                AnimHearts_[1].SetActive(true);
                AnimHearts_[2].SetActive(true);
                break;
        }
    }

    IEnumerator heartAnimation()
    {
        int i = 0;
        while (i != 8)
        {
            yield return new WaitForSeconds(0.25f);
            switch (i)
            {
                case 0:
                    AnimHearts_[0].SetActive(true);
                    i++;
                    
                    break;
                case 1:
                    AnimHearts_[1].SetActive(true);
                    i++;
                    break;
                case 2:
                    AnimHearts_[2].SetActive(true);
                    i++;
                    break;
                case 3:
                    AnimHearts_[0].SetActive(false);
                    AnimHearts_[1].SetActive(false);
                    AnimHearts_[2].SetActive(false);
                    i++;
                    break;
                case 4:
                    AnimHearts_[0].SetActive(true);
                    i++;
                    break;
                case 5:
                    AnimHearts_[1].SetActive(true);
                    i++;
                    break;
                case 6:
                    AnimHearts_[2].SetActive(true);
                    i++;
                    break;
                case 7:
                    AnimHearts_[0].SetActive(false);
                    AnimHearts_[0].SetActive(false);
                    AnimHearts_[0].SetActive(false);
                    HandleHeartVisual();
                    StopCoroutine("heartAnimation");
                    break;
            }
        }
    }
}
