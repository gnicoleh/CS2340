using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUpdated : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] public TMP_Text volumeTextValue = null;
    [SerializeField] public Slider volumeSlider = null;
    [SerializeField] public static float volumeCount = PegSolitaireController.volumeValue;
    [Space(10)]
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private bool fullscreenBool = true;
    
    private bool isFullScreen;
        void start()
    {
        if (volumeCount == 0f)
        {
            volumeCount = PlayerPrefs.GetFloat("masterVolume");
        }
    }
       /**
     * This method applies the exit button.
     */
    public void ExitButton()
    {
        Application.Quit();
    }
        public void setVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0");
    }
    public void volumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox()); //everytime we press apply, we will have a confirmation
    }
    
    public void prevVolume()
    {
        volumeCount = volumeSlider.value;
    }
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1); //show for two seconds
        confirmationPrompt.SetActive(false);
    }
    
}
