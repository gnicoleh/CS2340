using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RockPaperScissorsController : MonoBehaviour
{
    public Bet betScript;

    [Header("AI Prompt")]
    [SerializeField] private TMP_Text result;
    public Image aiChoice;

    [Header("Choices")]
    public string[] choices;
    [SerializeField] private Sprite Rock;
    [SerializeField] private Sprite Paper;
    [SerializeField] private Sprite Scissors;

    [Header("Player Cards Left")]
    [SerializeField] private TMP_Text playerCardLeft = null;

    [Header("AI Cards Left")]
    [SerializeField] private TMP_Text aiCardLeft = null;

    [Header("Warning")]
    [SerializeField] private GameObject placeBetWarning = null;

    [Header("Game OVER")]
    [SerializeField] private GameObject youLost = null;
    [SerializeField] private GameObject youWon = null;


    public void Play(string myChoice)
    {
        if (betScript.betFlag == true)
        {
            Debug.Log("player bet = " + betScript.playerBetValue + ". " +
                "ai bet is" + betScript.aiBetValue + ". total bet is " + (betScript.playerBetValue + betScript.aiBetValue));
            
            string randomAIChoice = choices[Random.Range(0, choices.Length)];
            switch (randomAIChoice)
            {
                case "Rock":
                    aiChoice.sprite = Rock;
                    switch (myChoice)
                    {
                        case "Rock":
                            result.text = "TIE!";
                            betScript.playerCardCount += betScript.playerBetValue;
                            betScript.aiCardCount += betScript.aiBetValue;
                            break;
                        case "Paper":
                            result.text = "You WIN!";
                            betScript.playerCardCount = betScript.playerCardCount + betScript.playerBetValue + betScript.aiBetValue;
                            break;
                        case "Scissors":
                            result.text = "You LOST!";
                            betScript.aiCardCount = betScript.aiCardCount + betScript.aiBetValue + betScript.playerBetValue;
                            break;
                    }
                    break;
                case "Paper":
                    aiChoice.sprite = Paper;
                    switch (myChoice)
                    {
                        case "Rock":
                            //Debug.Log("case 1");
                            result.text = "You LOST!";
                            betScript.aiCardCount = betScript.aiCardCount + betScript.aiBetValue + betScript.playerBetValue;
                            break;
                        case "Paper":
                            //Debug.Log("case 2");
                            result.text = "TIE!";
                            betScript.playerCardCount += betScript.playerBetValue;
                            betScript.aiCardCount += betScript.aiBetValue;
                            break;
                        case "Scissors":
                            //Debug.Log("case 3");
                            result.text = "You WIN!";
                            betScript.playerCardCount = betScript.playerCardCount + betScript.playerBetValue + betScript.aiBetValue;
                            break;
                    }
                    break;
                case "Scissors":
                    aiChoice.sprite = Scissors;
                    switch (myChoice)
                    {
                        case "Rock":
                            result.text = "You WIN!";
                            betScript.playerCardCount = betScript.playerCardCount + betScript.playerBetValue + betScript.aiBetValue;
                            break;
                        case "Paper":
                            result.text = "You LOST!";
                            betScript.aiCardCount = betScript.aiCardCount + betScript.aiBetValue + betScript.playerBetValue;
                            break;
                        case "Scissors":
                            result.text = "TIE!";
                            betScript.playerCardCount += betScript.playerBetValue;
                            betScript.aiCardCount += betScript.aiBetValue;
                            break;
                    }
                    break;
            }
            betScript.betFlag = false;
            //recalculateCardLeft();
            betScript.cardsRemainingTextupdate();
            checkEndGame();
        }
        else
        {
            StartCoroutine(warningScreen());
            Debug.Log("Bet has not made yet.");
        }
    }

    public void checkEndGame()
    {
        if(betScript.playerCardCount <= 0)
        {
            youLost.SetActive(true);
            Debug.Log("You Lose");
        }
        else if(betScript.aiCardCount <= 0)
        {
            youWon.SetActive(true);
            Debug.Log("You Won!");
        }
    }
    

    public void recalculateCardLeft()
    {
        if (result.text == "You WIN!")
        {
            betScript.playerCardCount += betScript.aiBetValue;
            playerCardLeft.text = betScript.playerCardCount.ToString();
        } 
        else if (result.text == "You LOST!")
        {
            betScript.aiCardCount += betScript.playerCardCount;
            aiCardLeft.text = betScript.aiCardCount.ToString();
        }
        else if (result.text == "TIE!")
        {
            playerCardLeft.text = betScript.playerCardCount.ToString();
            aiCardLeft.text = betScript.aiCardCount.ToString();
        }
    }
  
    public IEnumerator warningScreen()
    {
        placeBetWarning.SetActive(true);
        yield return new WaitForSeconds(1);
        placeBetWarning.SetActive(false);
    }
}