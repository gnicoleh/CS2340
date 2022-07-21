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
    [SerializeField] private int defaultBet = 3;
    public int playerBetValue = 0;

    [Header("Player Cards Left")]
    [SerializeField] private TMP_Text playerCardLeft = null;
    public float playerCardCount = 25;

    [Header("AI Cards Left")]
    [SerializeField] private TMP_Text aiBetValueText = null;
    [SerializeField] private TMP_Text aiCardLeft = null;
    public int aiBetValue = 0;
    public float aiCardCount = 25;

    
    public bool betFlag = false;
    private int defaultCard = 25;

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
            playerCardCount -= playerBetValue;
            cardsRemainingTextupdate();
            betFlag = true;
        }
        else
        {
            Debug.Log("The bet is already made.");
        }
    }

    public void applyAIBet()
    {
        if (aiCardCount == 1)
        {
            aiBetValue = 1;
        } else if (aiCardCount == 2)
        {
            aiBetValue = Random.Range(1, 2);
        } else if (aiCardCount == 3)
        {
            aiBetValue = Random.Range(1, 3);
        } else if (aiCardCount == 4)
        {
            aiBetValue = Random.Range(1, 4);
        } else
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
            aiCardCount = defaultCard;
        }
    }

    public void cardsRemainingTextupdate()
    {
        playerCardLeft.text = playerCardCount.ToString();
        aiCardLeft.text = aiCardCount.ToString();
    }
}
