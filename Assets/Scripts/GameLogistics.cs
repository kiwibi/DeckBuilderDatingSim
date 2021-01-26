using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogistics : MonoBehaviour
{
    static GameLogistics instance_;
    public enum chan
    {
        Rock,
        Paper,
        Scissor
    };
    public enum GameState
    {
        PickChan,
        FightChan,
        SpendChan
    }

    public GameState currentGameState;
    public GameObject[] Chans;
    public GameObject player_;
    private Player playerScript_;
    private int chanIndex_;

    private Deck playerDeck_;
    void Awake()
    {
        instance_ = this;
        currentGameState = GameState.PickChan;
        playerDeck_ = player_.GetComponent<Deck>();
        playerScript_ = player_.GetComponent<Player>();
        chanIndex_ = 0;
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

    public static GameState GetCurrentState()
    {
        if (instance_.currentGameState != null)
            return instance_.currentGameState;
        else
            return GameLogistics.GameState.PickChan;
    }
    public static void ChangeCurrentState()
    {
        if (instance_.currentGameState != GameLogistics.GameState.SpendChan)
            instance_.currentGameState++;
        else
            instance_.currentGameState = GameLogistics.GameState.PickChan;
        
    }

    public static void AddPlayerCard(chan type)
    {
        switch (type)
        {
            case chan.Rock:
                ChanBehavior rock = instance_.Chans[0].GetComponent<ChanBehavior>();
                if (rock.Affection > 0)
                {
                    rock.useAffection();
                    instance_.playerDeck_.increaseCardType(type);
                }
                break;
            case chan.Paper:
                ChanBehavior paper = instance_.Chans[0].GetComponent<ChanBehavior>();
                if (paper.Affection > 0)
                {
                    paper.useAffection();
                    instance_.playerDeck_.increaseCardType(type);
                }
                break;
            case chan.Scissor:
                ChanBehavior scissor = instance_.Chans[0].GetComponent<ChanBehavior>();
                if (scissor.Affection > 0)
                {
                    scissor.useAffection();
                    instance_.playerDeck_.increaseCardType(type);
                }
                break;
            default:
                Debug.Log("faulty input");
                break;
        }
        ChangeCurrentState();
    }

    public static void ActivateArrow(GameObject arrow)
    {
        arrow.SetActive(true);
        //arrow.GetComponent<Blinking>().ActivateArrow();
    }

    public static void DeActivateArrow(GameObject arrow)
    {
        arrow.SetActive(false);
    }

    public static void pickADate(GameLogistics.chan type)
    {
        switch (type)
        {
            case chan.Rock:
                instance_.Chans[1].SetActive(false);
                instance_.Chans[2].SetActive(false);
                instance_.chanIndex_ = 0;
                break;
            case chan.Paper:
                instance_.Chans[0].SetActive(false);
                instance_.Chans[2].SetActive(false);
                instance_.chanIndex_ = 1;
                break;
            case chan.Scissor:
                instance_.Chans[0].SetActive(false);
                instance_.Chans[1].SetActive(false);
                instance_.chanIndex_ = 2;
                break;
            default:
                Debug.Log("faulty input");
                break;
        }
        ChangeCurrentState();
        instance_.Chans[instance_.chanIndex_].GetComponent<ChanBehavior>().PlayCard();
    }

    void enableAllDates()
    {
        foreach(var date in Chans)
        {
            date.SetActive(true);
        }
    }

    public static void CalculateRPS(int playerCardIndex)
    {
        ChanBehavior chanScript = instance_.Chans[instance_.chanIndex_].GetComponent<ChanBehavior>();
        switch (playerCardIndex)
        {
            case 0:
                if(chanScript.pickedCard_ == 0)
                {
                    Debug.Log("Draw");
                }else if(chanScript.pickedCard_ == 1)
                {
                    Debug.Log("Loss");
                }
                else
                {
                    Debug.Log("Win");
                }
                break;
            case 1:
                if (chanScript.pickedCard_ == 0)
                {
                    Debug.Log("Win");
                }
                else if (chanScript.pickedCard_ == 1)
                {
                    Debug.Log("Draw");
                }
                else
                {
                    Debug.Log("Loss");
                }
                break;
            case 2:
                if (chanScript.pickedCard_ == 0)
                {
                    Debug.Log("Loss");
                }
                else if (chanScript.pickedCard_ == 1)
                {
                    Debug.Log("Win");
                }
                else
                {
                    Debug.Log("Draw");
                }
                break;
        }
        
        chanScript.PlayCard();
        instance_.playerScript_.played_ = false;

    }
}
