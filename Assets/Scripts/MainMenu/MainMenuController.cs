using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 50f;
    [SerializeField] private float volumeCount = 0f;
    public AudioSource AudioSource;

    [Space(10)]
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private bool fullscreenBool = true;
    private bool isFullScreen;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    void start()
    {
        volumeCount = PlayerPrefs.GetFloat("masterVolume");
    }
    
    /**
     * This method applies the exit button.
     */
    public void ExitButton()
    {
        Application.Quit();
    }    
    /**
     * This method is to control the audio volume level.
     * AudioListener changes every single audio in the game. AudioListener has a value
     * between 0 and 1. I set the default volume as 0.5/half.
     * This method also changes the text displayed for on our volume controller.
     * 
     * @param volume - the volume level passed in.
     */
    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0");
    }

    /**
     * This method is to save the volume the user prefers.
     */
    public void volumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox()); //everytime we press apply, we will have a confirmation
    }
    /**
     * This method checks whether the game is fullscreen or not.
     * 
     * @param fullScreen is or isn't fullscreen
     */
    public void setFullScreen(bool fullScreen)
    {
        isFullScreen = fullScreen;
    }
    /**
     * This method applies the brightness and quality of our game
     * according to the parameters passed in above.
     */
    public void fullScreenApply()
    {
        PlayerPrefs.SetInt("masterFullScreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;
        StartCoroutine(ConfirmationBox());
    }

    /**
     * This method resets
     * 
     * @param menuType whether it is for audio button or gameplay button
     */
    public void resetButton(string menuType)
    {
        if (menuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0");
            volumeApply();
        }
    }
    /**
     * This method keeps previous values
     * 
     * @param menuType whether it is for audio button or gameplay button
     */
    public void noButton(string menuType)
    {
        if (menuType == "Audio")
        {
            AudioListener.volume = volumeCount;
            volumeSlider.value = volumeCount;
            volumeTextValue.text = volumeCount.ToString("0");
            volumeApply();
        }
    }
    /**
     * This method is to create a simple color routine when we save/apply our changes from above.
     */
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1); //show for one seconds
        confirmationPrompt.SetActive(false);
    }
    /**
     * record the volume when the setting panel is opened
     */
    public void prevVolume()
    {
        volumeCount = volumeSlider.value;
    }
}