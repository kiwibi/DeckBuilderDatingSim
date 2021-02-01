using System.Collections;
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
    public GameObject[] ActiveCardNumbers_;
    public GameObject[] DeckCardNumbers_;
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
        setAllDeckNumbers();
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.N))
        {

        }

    }

    public void increaseCardType(GameLogistics.chan type)
    {
        switch (type)
        {
            case GameLogistics.chan.Paper:
                maxPapers_++;
                Papers_ = maxPapers_;
                setDeckNumber(GameLogistics.chan.Paper, maxPapers_);
                break;
            case GameLogistics.chan.Rock:
                maxRocks_++;
                Rocks_ = maxRocks_;
                setDeckNumber(GameLogistics.chan.Rock, maxRocks_);
                break;
            case GameLogistics.chan.Scissor:
                maxScissors_++;
                Scissors_ = maxScissors_;
                setDeckNumber(GameLogistics.chan.Scissor, maxScissors_);
                break;
        }
    }

    public void drawHand()
    {
        for (int i = 0; i < playerHand_.handsize_ ; i++)
        {
            if (deckSize_ == 0)
            {
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
                        break;
                    }
                    i--;
                    break;
            }
        }
        setAllHandNumbers();

        setAllDeckNumbers();
    }

    public bool isCardInDeckAvailible(int cardNumber)
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

    public bool isCardInHandAvailible(int cardNumber)
    {
        switch (cardNumber)
        {
            case 0:
                if (playerHand_.rocks_ == 0)
                    return false;
                break;
            case 1:
                if (playerHand_.papers_ == 0)
                    return false;
                break;
            case 2:
                if (playerHand_.scissors_ == 0)
                    return false;
                break;
        }
        return true;
    }

    void resetDeck()
    {
        usedCards_ = 0;
        Rocks_ = maxRocks_;
        Papers_ = maxPapers_;
        Scissors_ = maxScissors_;
        deckSize_ = maxPapers_ + maxRocks_ + maxScissors_;
        setAllDeckNumbers();
    }

    public void PlayCard(GameLogistics.chan type)
    {
        usedCards_++;
        switch (type)
        {
            case GameLogistics.chan.Rock:
                playerHand_.rocks_--;
                setCardNumber(GameLogistics.chan.Rock, playerHand_.rocks_);
                break;
            case GameLogistics.chan.Paper:
                playerHand_.papers_--;
                setCardNumber(GameLogistics.chan.Paper, playerHand_.papers_);
                break;
            case GameLogistics.chan.Scissor:
                playerHand_.scissors_--;
                setCardNumber(GameLogistics.chan.Scissor, playerHand_.scissors_);
                break;
        }
        if(isHandEmpty())
        {
            if(deckSize_ == 0)
            {
                StartCoroutine("goToPick");
                return;
            }
            drawHand();
        }
    }

    private void setDeckNumber(GameLogistics.chan type, int number)
    {
        switch (type)
        {
            case GameLogistics.chan.Rock:
                if(number > 10)
                {
                    DeckCardNumbers_[0].transform.GetChild(0).GetComponent<NumberScript>().setNumber(((number / 10) % 10));
                    DeckCardNumbers_[0].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                else
                {
                    DeckCardNumbers_[0].transform.GetChild(0).GetComponent<NumberScript>().setNumber(0);
                    DeckCardNumbers_[0].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                break;
            case GameLogistics.chan.Paper:
                if (number > 10)
                {
                    DeckCardNumbers_[1].transform.GetChild(0).GetComponent<NumberScript>().setNumber(((number / 10) % 10));
                    DeckCardNumbers_[1].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                else
                {
                    DeckCardNumbers_[1].transform.GetChild(0).GetComponent<NumberScript>().setNumber(0);
                    DeckCardNumbers_[1].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                break;
            case GameLogistics.chan.Scissor:
                if (number > 10)
                {
                    DeckCardNumbers_[2].transform.GetChild(0).GetComponent<NumberScript>().setNumber(((number / 10) % 10));
                    DeckCardNumbers_[2].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                else
                {
                    DeckCardNumbers_[2].transform.GetChild(0).GetComponent<NumberScript>().setNumber(0);
                    DeckCardNumbers_[2].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                break;
        }
    }

    private void setCardNumber(GameLogistics.chan type, int number)
    {
        switch (type)
        {
            case GameLogistics.chan.Rock:
                if (number > 10)
                {
                    ActiveCardNumbers_[0].transform.GetChild(0).GetComponent<NumberScript>().setNumber(((number / 10) % 10));
                    ActiveCardNumbers_[0].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                else
                {
                    ActiveCardNumbers_[0].transform.GetChild(0).GetComponent<NumberScript>().setNumber(0);
                    ActiveCardNumbers_[0].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                break;
            case GameLogistics.chan.Paper:
                if (number > 10)
                {
                    ActiveCardNumbers_[1].transform.GetChild(0).GetComponent<NumberScript>().setNumber(((number / 10) % 10));
                    ActiveCardNumbers_[1].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                else
                {
                    ActiveCardNumbers_[1].transform.GetChild(0).GetComponent<NumberScript>().setNumber(0);
                    ActiveCardNumbers_[1].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                break;
            case GameLogistics.chan.Scissor:
                if (number > 10)
                {
                    ActiveCardNumbers_[2].transform.GetChild(0).GetComponent<NumberScript>().setNumber(((number / 10) % 10));
                    ActiveCardNumbers_[2].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                else
                {
                    ActiveCardNumbers_[2].transform.GetChild(0).GetComponent<NumberScript>().setNumber(0);
                    ActiveCardNumbers_[2].transform.GetChild(1).GetComponent<NumberScript>().setNumber((number % 10));
                }
                break;
        }
    }

    public bool isHandEmpty()
    {
        if(playerHand_.rocks_ == 0)
        {
            if(playerHand_.papers_ == 0)
            {
                if(playerHand_.scissors_ == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void setAllDeckNumbers()
    {
        setDeckNumber(GameLogistics.chan.Rock, Rocks_);
        setDeckNumber(GameLogistics.chan.Paper, Papers_);
        setDeckNumber(GameLogistics.chan.Scissor, Scissors_);
    }

    private void setAllHandNumbers()
    {
        setCardNumber(GameLogistics.chan.Rock, playerHand_.rocks_);
        setCardNumber(GameLogistics.chan.Paper, playerHand_.papers_);
        setCardNumber(GameLogistics.chan.Scissor, playerHand_.scissors_);
    }

    IEnumerator goToPick()
    {
        yield return new WaitForSeconds(0.5f);
        GameLogistics.ChangeCurrentState();
        gameObject.GetComponent<Player>().enterPickPhase();
        resetDeck();
    }

    public void reset()
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
        playerHand_.rocks_ = 0;
        playerHand_.papers_ = 0;
        playerHand_.scissors_ = 0;
        setAllDeckNumbers();
        setAllHandNumbers();
    }
}
