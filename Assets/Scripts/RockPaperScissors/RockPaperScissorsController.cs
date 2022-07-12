using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RockPaperScissorsController : MonoBehaviour
{
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
        string randomAIChoice = choices[Random.Range(0, choices.Length)];

        switch (randomAIChoice)
        {
            case "Rock":
                aiChoice.sprite = Rock;
                switch (myChoice)
                {
                    case "Rock":
                        result.text = "TIE!";
                        break;
                    case "Paper":
                        result.text = "You WIN!";
                        break;
                    case "Scissors":
                        result.text = "You LOST!";
                        break;
                }
                break;
            case "Paper":
                aiChoice.sprite = Paper;
                switch (myChoice)
                {
                    case "Rock":
                        result.text = "You LOST!";
                        break;
                    case "Paper":
                        result.text = "TIE!";
                        break;
                    case "Scissors":
                        result.text = "You WIN!";
                        break;
                }
                break;
            case "Scissors":
                aiChoice.sprite = Scissors;
                switch (myChoice)
                {
                    case "Rock":
                        result.text = "You WIN!";
                        break;
                    case "Paper":
                        result.text = "You LOST!";
                        break;
                    case "Scissors":
                        result.text = "TIE!";
                        break;
                }
                break;
        }
    }

    public TMP_Text getResult()
    {
        return result;
    }
}