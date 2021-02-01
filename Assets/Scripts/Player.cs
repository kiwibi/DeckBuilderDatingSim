using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private enum direction
    {
        LEFT,
        RIGHT
    }
    public GameObject[] pickDateBorders_;
    public GameObject[] pickCardBorders_;
    public GameObject[] pickUpgradeBorders_;
    [HideInInspector]
    public GameLogistics.chan pickChan;
    [HideInInspector]
    public int cardIndex_;
    [HideInInspector]
    public bool played_;
    private void Start()
    {
        played_ = false;
    }
    void Update()
    {
        switch (GameLogistics.GetCurrentState())
        {
            case GameLogistics.GameState.PickChan:
                PickUpdate();
                break;
            case GameLogistics.GameState.FightChan:
                FightUpdate();
                break;
            case GameLogistics.GameState.SpendChan:
                SpendUpdate();
                break;
        }
    }

    void PickUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioPlayer.PlaySoundEffect(1);
            updatePickedChan(direction.LEFT);
            GameLogistics.DeActivateArrow(pickDateBorders_[cardIndex_]);
            updatePickedCard(direction.LEFT);
            GameLogistics.ActivateArrow(pickDateBorders_[cardIndex_]);
            //pickDateArrow_.transform.position = new Vector3(GameLogistics.GetSpecificChan(pickChan).transform.position.x, pickDateArrow_.transform.position.y);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            AudioPlayer.PlaySoundEffect(1);
            updatePickedChan(direction.RIGHT);
            GameLogistics.DeActivateArrow(pickDateBorders_[cardIndex_]);
            updatePickedCard(direction.RIGHT);
            GameLogistics.ActivateArrow(pickDateBorders_[cardIndex_]);
            //pickDateArrow_.transform.position = new Vector3(GameLogistics.GetSpecificChan(pickChan).transform.position.x, pickDateArrow_.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameLogistics.pickADate(pickChan);
            GameLogistics.DeActivateArrow(pickDateBorders_[cardIndex_]);
            GameLogistics.ActivateArrow(pickCardBorders_[cardIndex_]);
            
        }
    }

    void FightUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioPlayer.PlaySoundEffect(1);
            GameLogistics.DeActivateArrow(pickCardBorders_[cardIndex_]);
            updatePickedCard(direction.LEFT);
            GameLogistics.ActivateArrow(pickCardBorders_[cardIndex_]);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AudioPlayer.PlaySoundEffect(1);
            GameLogistics.DeActivateArrow(pickCardBorders_[cardIndex_]);
            updatePickedCard(direction.RIGHT);
            GameLogistics.ActivateArrow(pickCardBorders_[cardIndex_]);
        }
        if (Input.GetKeyDown(KeyCode.Space) && played_ == false)
        {
            AudioPlayer.PlaySoundEffect(2);
            played_ = true;
            GameLogistics.CalculateRPS(cardIndex_);

        }
    }

    void SpendUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioPlayer.PlaySoundEffect(1);
            if (pickUpgradeBorders_[0].activeSelf)
            {
                pickUpgradeBorders_[0].SetActive(false);
                pickUpgradeBorders_[1].SetActive(true);
            }
            else
            {
                pickUpgradeBorders_[1].SetActive(false);
                pickUpgradeBorders_[0].SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AudioPlayer.PlaySoundEffect(1);
            if (pickUpgradeBorders_[0].activeSelf)
            {
                pickUpgradeBorders_[0].SetActive(false);
                pickUpgradeBorders_[1].SetActive(true);
            }
            else
            {
                pickUpgradeBorders_[1].SetActive(false);
                pickUpgradeBorders_[0].SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(pickUpgradeBorders_[0].activeSelf)
            {
                GameLogistics.AddPlayerCard(pickChan);
            }
            else
            {
                AudioPlayer.PlaySoundEffect(1);
                GameLogistics.ChangeCurrentState();
                pickUpgradeBorders_[1].SetActive(false);
                pickUpgradeBorders_[0].SetActive(false);
                GameLogistics.ActivateArrow(pickDateBorders_[0]);
                cardIndex_ = 0;
            }
        }
    }

    void updatePickedChan(direction dir)
    {
        switch(dir)
        {
            case direction.RIGHT:
                if (pickChan != GameLogistics.chan.Scissor)
                    pickChan++;
                else
                    pickChan = GameLogistics.chan.Rock;
                break;
            case direction.LEFT:
                if (pickChan != GameLogistics.chan.Rock)
                    pickChan--;
                else
                    pickChan = GameLogistics.chan.Scissor;
                break;
        }
    }
    void updatePickedCard(direction dir)
    {
        switch (dir)
        {
            case direction.RIGHT:
                if (cardIndex_ < 2)
                {
                    cardIndex_++;
                }
                else
                    cardIndex_ = 0;
                break;
            case direction.LEFT:
                if (cardIndex_ > 0)
                    cardIndex_--;
                else
                    cardIndex_ = 2;
                break;
        }
    }

    public void enterPickPhase()
    {
        GameLogistics.DeActivateArrow(pickCardBorders_[cardIndex_]);
        GameLogistics.ActivateArrow(pickUpgradeBorders_[0]);
        GameLogistics.GetSpecificChan(pickChan).GetComponent<ChanBehavior>().deactivateAllMinis();
        
    }

    public void Reset()
    {
        GameLogistics.DeActivateArrow(pickCardBorders_[cardIndex_]);

    }
}
