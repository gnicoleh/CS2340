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


    public void Play(string myChoice)
    {
        if (betScript.betFlag == true)
        {
            Debug.Log("total bet is " + betScript.playerBetValue + betScript.aiBetValue);
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
                        Debug.Log("case 1");
                            result.text = "You LOST!";
                            betScript.aiCardCount = betScript.aiCardCount + betScript.aiBetValue + betScript.playerBetValue;
                            break;
                        case "Paper":
                        Debug.Log("case 2");
                            result.text = "TIE!";
                            betScript.playerCardCount += betScript.playerBetValue;
                            betScript.aiCardCount += betScript.aiBetValue;
                            break;
                        case "Scissors":
                        Debug.Log("case 3");
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
            betScript.cardsRemainingTextupdate();
            checkEndGame();
        }
        else
        {
            Debug.Log("Bet has not made yet.");
        }
    }

    public void checkEndGame()
        {
            if(betScript.playerCardCount <= 0)
            {
                Debug.Log("You Lose");
            }
            else if(betScript.aiCardCount <= 0)
            {
                Debug.Log("You Won!");
            }
        }
    public TMP_Text getResult()
    {
        return result;
    }
}