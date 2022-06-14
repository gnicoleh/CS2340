using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [Header("Levels to Load")]
    public string _newGameLevel;

    // go to the specified level
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }
}
