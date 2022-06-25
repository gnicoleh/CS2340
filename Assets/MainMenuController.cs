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

    [Header("Graphics Settings")]
    [SerializeField] private TMP_Text controllerSensitivityValue = null;
    [SerializeField] private Slider controllerSensitivitySlider = null;
    [SerializeField] private int defaultSensitivity = 4;
    [SerializeField] private float sensitivityCount = 0f;
    public int mainSensitivityController = 4;

    [Space(10)]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessValue = null;
    [SerializeField] private float defaultBrightness = 1;
    [SerializeField] private float brightnessCount = 0;

    [Space(10)]
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private bool fullscreenBool = true;

    

    private bool isFullScreen;
    private float brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

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
     * This method sets the sensitivity for our Gameplay settings.
     * We should convert float sensitivity value to int.
     * 
     * @param sensitivity value. 
     */
    public void setControllerSensitivity(float sensitivity)
    {
        mainSensitivityController = Mathf.RoundToInt(sensitivity);
        controllerSensitivityValue.text = sensitivity.ToString("0");
    }
    /**
     * This method is used to set a new brightness level.
     * 
     * @param brightness the brightness value we want to set
     */
    public void setBrightness(float brightness)
    {
        brightnessLevel = brightness;
        brightnessValue.text = brightness.ToString("0.0");
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
    public void graphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);

        PlayerPrefs.SetInt("masterFullScreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;

        PlayerPrefs.SetFloat("masterSensitivity", mainSensitivityController);
        StartCoroutine(ConfirmationBox());

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

        if (menuType == "Graphics")
        {
            //reset brightness vale
            brightnessSlider.value = defaultBrightness;
            brightnessValue.text = defaultBrightness.ToString("0.0");

            fullScreenToggle.isOn = true;
            Screen.fullScreen = true;

            controllerSensitivityValue.text = defaultSensitivity.ToString("0");
            controllerSensitivitySlider.value = defaultSensitivity;
            mainSensitivityController = defaultSensitivity;

            graphicsApply();
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

        if (menuType == "Graphics")
        {
            //reset brightness vale
            brightnessSlider.value = brightnessCount;
            brightnessValue.text = brightnessCount.ToString("0.0");

            fullScreenToggle.isOn = fullscreenBool;
            Screen.fullScreen = fullscreenBool;

            controllerSensitivityValue.text = sensitivityCount.ToString("0");
            controllerSensitivitySlider.value = sensitivityCount;
            mainSensitivityController = (int) sensitivityCount;

            graphicsApply();
        }
    }
    /**
     * This method is to create a simple color routine when we save/apply our changes from above.
     */
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1); //show for two seconds
        confirmationPrompt.SetActive(false);
    }
    /**
     * record the volume when the setting panel is opened
     */
    public void prevVolume()
    {
        volumeCount = volumeSlider.value;
    }
    /**
     * record the volume when the setting panel is opened
     */
    public void prevGraphics()
    {
        sensitivityCount = controllerSensitivitySlider.value;
        brightnessCount = brightnessSlider.value;
        fullscreenBool = fullScreenToggle.isOn;

    }
}