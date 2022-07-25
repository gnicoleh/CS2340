using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bet : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Button betButton;
    [SerializeField] private Slider betSlider = null;
    [SerializeField] private TMP_Text betTextValue = null;
    [SerializeField] private int defaultBet;
    public int playerBetValue = 3;
    public ButtonInteraction buttonInteraction;

    [Header("Player Cards Left")]
    [SerializeField] private TMP_Text playerCardLeft = null;
    public int playerCardCount = 10;

    [Header("AI Cards Left")]
    [SerializeField] private TMP_Text aiBetValueText = null;
    [SerializeField] private TMP_Text aiCardLeft = null;
    [SerializeField] private Image aiImage;
    [SerializeField] private Sprite QuestionMark;
    public int aiBetValue = 0;
    public int aiCardCount = 10;

    [Header("Warning")]
    [SerializeField] private GameObject decreaseBetWarning = null;

    [Header("Result Text")]
    [SerializeField] private TMP_Text resultText = null;
    
    public bool betFlag = false;
    private int defaultCard = 10;


    public void setBet(float bet)
    {
        betTextValue.text = bet.ToString("");
        playerBetValue = (int) bet;
    }
    /**
     * When player presses "Bet" btn, this will activate. It will decrease the
     * cards that the player has and will make the bet.
     */
    public void applyPlayerBet()
    {
        
        if (betFlag == false && playerBetValue <= playerCardCount)
        {
            buttonInteraction.disableBetButton();
            playerCardCount -= playerBetValue;
            cardsRemainingTextupdate();
            betFlag = true;
            applyAIBet();
        }
        else if (betFlag == false && playerBetValue > playerCardCount)
        {
            betFlag = false;
            betButton.interactable = true;
            buttonInteraction.enableBetButton();
            StartCoroutine(warningBet());
            Debug.Log("INVALID bet.");
        }
    }

    public IEnumerator warningBet()
    {
        decreaseBetWarning.SetActive(true);
        yield return new WaitForSeconds(1);
        decreaseBetWarning.SetActive(false);
    }

    public void applyAIBet()
    {
        if (aiCardCount < 5)
        {
            aiBetValue = Random.Range(1, aiCardCount);
        }
        else
        {
            aiBetValue = Random.Range(1, 5);
        }
        aiCardCount -= aiBetValue;

        aiBetValueText.text = aiBetValue.ToString();
        cardsRemainingTextupdate();
    }

    public void resetBtn(string order)
    {
        if (order == "Reset")
        {
            betSlider.value = defaultBet;
            aiBetValue = defaultBet;
            playerCardCount = defaultCard;
            playerCardLeft.text = playerCardCount.ToString();
            aiCardCount = defaultCard;
            aiCardLeft.text = aiCardCount.ToString();
            aiBetValueText.text = 3 + "";
            resultText.text = "Results";
            betButton.interactable = true;
            aiImage.sprite = QuestionMark;
        }
    }

    public void cardsRemainingTextupdate()
    {
        playerCardLeft.text = playerCardCount.ToString();
        aiCardLeft.text = aiCardCount.ToString();
    }
}
