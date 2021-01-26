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
    public GameObject pickDateArrow_;
    public GameObject pickCardArrow_;
    public GameObject[] pickCardAnchors_;
    private GameLogistics.chan pickChan;
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
            updatePickedChan(direction.LEFT);
            pickDateArrow_.transform.position = new Vector3(GameLogistics.GetSpecificChan(pickChan).transform.position.x, pickDateArrow_.transform.position.y);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            updatePickedChan(direction.RIGHT);
            pickDateArrow_.transform.position = new Vector3(GameLogistics.GetSpecificChan(pickChan).transform.position.x, pickDateArrow_.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameLogistics.pickADate(pickChan);
            GameLogistics.DeActivateArrow(pickDateArrow_);
            GameLogistics.ActivateArrow(pickCardArrow_);
        }
    }

    void FightUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            updatePickedCard(direction.LEFT);
            pickCardArrow_.transform.position = new Vector3(pickCardAnchors_[cardIndex_].transform.position.x, pickCardArrow_.transform.position.y);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            updatePickedCard(direction.RIGHT);
            pickCardArrow_.transform.position = new Vector3(pickCardAnchors_[cardIndex_].transform.position.x, pickCardArrow_.transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && played_ == false)
        {
            played_ = true;
            GameLogistics.CalculateRPS(cardIndex_);

        }
    }

    void SpendUpdate()
    {

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

}
