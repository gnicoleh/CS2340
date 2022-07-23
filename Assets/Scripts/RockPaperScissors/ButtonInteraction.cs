using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button betButton;

    public void disableBetButton()
    {
        betButton.interactable = false;
    }

    public void enableBetButton()
    {
        betButton.interactable = true;
    }
}
