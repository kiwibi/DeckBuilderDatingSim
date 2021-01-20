﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    struct Hand
    {
        public int handsize_;
        public int rocks_;
        public int papers_;
        public int scissors_;

    }
    int maxRocks_;
    int maxPapers_;
    int maxScissors_;
    int usedCards_;
    int Rocks_;
    int Papers_;
    int Scissors_;
    int deckSize_;
    public int startValue;
    Hand playerHand_;

    void Start()
    {
        playerHand_.handsize_ = 5;
        usedCards_ = 0;
        Rocks_ = startValue;
        Papers_ = startValue;
        Scissors_ = startValue;
        maxRocks_ = startValue;
        maxPapers_ = startValue;
        maxScissors_ = startValue;
        deckSize_ = maxPapers_ + maxRocks_ + maxScissors_;
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            draw();
        }

    }

    void increaseCardType(GameManager.chan type)
    {
        switch (type)
        {
            case GameManager.chan.Paper:
                maxPapers_++;
                break;
            case GameManager.chan.Rock:
                maxRocks_++;
                break;
            case GameManager.chan.Scissor:
                maxScissors_++;
                break;
        }
    }

    void draw()
    {
        for (int i = 0; i < playerHand_.handsize_ ; i++)
        {
            if (deckSize_ == 0)
            {
                resetDeck();
                break;
            }

            switch (Random.Range(0, 3))
            {
                case 0:
                    if(Rocks_ != 0)
                    {
                        playerHand_.rocks_++;
                        Rocks_--;
                        deckSize_--;
                        Debug.Log("rock drawn");
                        break;
                    }
                    i--;
                    break;
                case 1:
                    if (Papers_ != 0)
                    {
                        playerHand_.papers_++;
                        Papers_--;
                        deckSize_--;
                        Debug.Log("paper drawn");
                        break;
                    }
                    i--;
                    break;
                case 2:
                    if (Scissors_ != 0)
                    {
                        playerHand_.scissors_++;
                        Scissors_--;
                        deckSize_--;
                        Debug.Log("scissor drawn");
                        break;
                    }
                    i--;
                    break;
            }
        }
    }

    bool isCardAvailible(int cardNumber)
    {
        switch (cardNumber)
        {
            case 0:
                if (Rocks_ == 0)
                    return false;
                    break;
            case 1:
                if (Papers_ == 0)
                    return false;
                break;
            case 2:
                if (Scissors_ == 0)
                    return false;
                break;
        }
        return true;
    }

    void resetDeck()
    {
        Rocks_ = maxRocks_;
        Papers_ = maxPapers_;
        Scissors_ = maxScissors_;
        deckSize_ = maxPapers_ + maxRocks_ + maxScissors_;
        Debug.Log("deck reset");
    }
}
