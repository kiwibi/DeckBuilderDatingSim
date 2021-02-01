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
    public GameObject[] stageIndicators_;
    public GameObject[] Chans;
    public GameObject player_;
    private Player playerScript_;
    private int chanIndex_;
    private int phaseIndicatorIndex_;

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
        return instance_.currentGameState;
        
    }
    public static void ChangeCurrentState()
    {
        if (instance_.currentGameState != GameLogistics.GameState.SpendChan)
        {
            instance_.currentGameState++;
            instance_.stageIndicators_[instance_.phaseIndicatorIndex_].SetActive(false);
            instance_.phaseIndicatorIndex_++;
            instance_.stageIndicators_[instance_.phaseIndicatorIndex_].SetActive(true);
        }
        else
        {
            instance_.currentGameState = GameLogistics.GameState.PickChan;
            instance_.stageIndicators_[instance_.phaseIndicatorIndex_].SetActive(false);
            instance_.phaseIndicatorIndex_ = 0;
            instance_.stageIndicators_[instance_.phaseIndicatorIndex_].SetActive(true);
            instance_.enableAllDates();
        }
        AudioPlayer.PlaySoundEffect(4);
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
                ChanBehavior paper = instance_.Chans[1].GetComponent<ChanBehavior>();
                if (paper.Affection > 0)
                {
                    paper.useAffection();
                    instance_.playerDeck_.increaseCardType(type);
                }
                break;
            case chan.Scissor:
                ChanBehavior scissor = instance_.Chans[2].GetComponent<ChanBehavior>();
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
        AudioPlayer.PlaySoundEffect(3);
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
                AudioPlayer.PlayVoiceLine(1);
                instance_.Chans[1].SetActive(false);
                instance_.Chans[2].SetActive(false);
                instance_.chanIndex_ = 0;
                break;
            case chan.Paper:
                AudioPlayer.PlayVoiceLine(2);
                instance_.Chans[0].SetActive(false);
                instance_.Chans[2].SetActive(false);
                instance_.chanIndex_ = 1;
                break;
            case chan.Scissor:
                AudioPlayer.PlayVoiceLine(3);
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
        DeActivateArrow(instance_.Chans[instance_.chanIndex_].GetComponent<ChanBehavior>().Percentages_);
        instance_.playerDeck_.drawHand();
    }

    void enableAllDates()
    {
        foreach(var date in Chans)
        {
            date.SetActive(true);
            date.GetComponent<ChanBehavior>().dayReset();
        }
        ActivateArrow(instance_.Chans[instance_.chanIndex_].GetComponent<ChanBehavior>().Percentages_);
        playerScript_.pickChan = chan.Rock;
    }

    public static void CalculateRPS(int playerCardIndex)
    {
        ChanBehavior chanScript = instance_.Chans[instance_.chanIndex_].GetComponent<ChanBehavior>();
        instance_.StopCoroutine("lossDrawEffect");
        instance_.StopCoroutine("heartAnimation");
        chanScript.HandleHeartVisual();
        instance_.fastClickerHide();
        switch (playerCardIndex)
        {
            case 0:
                if (instance_.playerDeck_.isCardInHandAvailible(0))
                {
                    instance_.playerDeck_.PlayCard(chan.Rock);
                    if (chanScript.pickedCard_ == 0)
                    {
                        AudioPlayer.PlayResult(2);
                        chanScript.WinDrawLoss_[0].SetActive(true);
                        instance_.StartCoroutine("lossDrawEffect");
                    }
                    else if (chanScript.pickedCard_ == 1)
                    {
                        AudioPlayer.PlayResult(3);
                        chanScript.addHeart(-1);
                        chanScript.WinDrawLoss_[1].SetActive(true);
                        instance_.StartCoroutine("lossDrawEffect");
                    }
                    else if (chanScript.pickedCard_ == 2)
                    {
                        AudioPlayer.PlayResult(1);
                        chanScript.addHeart(1);
                    }
                    chanScript.PlayCard();
                }
                break;
            case 1:
                if (instance_.playerDeck_.isCardInHandAvailible(1))
                {
                    instance_.playerDeck_.PlayCard(chan.Paper);
                    if (chanScript.pickedCard_ == 0)
                    {
                        AudioPlayer.PlayResult(1);
                        chanScript.addHeart(1);
                    }
                    else if (chanScript.pickedCard_ == 1)
                    {
                        AudioPlayer.PlayResult(2);
                        chanScript.WinDrawLoss_[0].SetActive(true);
                        instance_.StartCoroutine("lossDrawEffect");
                    }
                    else if (chanScript.pickedCard_ == 2)
                    {
                        AudioPlayer.PlayResult(3);
                        chanScript.addHeart(-1);
                        chanScript.WinDrawLoss_[0].SetActive(true);
                        instance_.StartCoroutine("lossDrawEffect");
                    }
                    chanScript.PlayCard();
                }
                break;
            case 2:
                if (instance_.playerDeck_.isCardInHandAvailible(2))
                {
                    instance_.playerDeck_.PlayCard(chan.Scissor);
                    if (chanScript.pickedCard_ == 0)
                    {
                        AudioPlayer.PlayResult(3);
                        chanScript.addHeart(-1);
                        chanScript.WinDrawLoss_[1].SetActive(true);
                        instance_.StartCoroutine("lossDrawEffect");
                    }
                    else if (chanScript.pickedCard_ == 1)
                    {
                        AudioPlayer.PlayResult(1);
                        chanScript.addHeart(1);
                    }
                    else if(chanScript.pickedCard_ == 2)
                    {
                        AudioPlayer.PlayResult(2);
                        chanScript.WinDrawLoss_[0].SetActive(true);
                        instance_.StartCoroutine("lossDrawEffect");
                    }
                    chanScript.PlayCard();
                }
                break;
        }
        
        
        instance_.playerScript_.played_ = false;

    }

    IEnumerator lossDrawEffect()
    {
        yield return new WaitForSeconds(1f);
        Chans[chanIndex_].GetComponent<ChanBehavior>().WinDrawLoss_[0].SetActive(false);
        Chans[chanIndex_].GetComponent<ChanBehavior>().WinDrawLoss_[1].SetActive(false);
    }

    private void fastClickerHide()
    {
        Chans[chanIndex_].GetComponent<ChanBehavior>().WinDrawLoss_[0].SetActive(false);
        Chans[chanIndex_].GetComponent<ChanBehavior>().WinDrawLoss_[1].SetActive(false);
    }

    public static void reset()
    {
        ActivateArrow(instance_.Chans[instance_.chanIndex_].GetComponent<ChanBehavior>().Percentages_);
        instance_.currentGameState = GameState.PickChan;
        instance_.chanIndex_ = 0;
        instance_.stageIndicators_[1].SetActive(false);
        instance_.phaseIndicatorIndex_ = 0;
        instance_.stageIndicators_[instance_.phaseIndicatorIndex_].SetActive(true);
        instance_.playerDeck_.reset();
        instance_.enableAllDates();
        instance_.playerScript_.Reset();
        
    }
}
