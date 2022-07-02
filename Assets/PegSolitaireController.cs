using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
// does not update
public class PegSolitaireController : MonoBehaviour
{
    // note that buttons available does not update but pegplaces does
    // peg places is a null filled array with corresponding entries for buttonsavailble
    int[] buttonsAvailable = new int[] {25, 26, 27,
                                        36, 37, 38,
                                45, 46, 47, 48, 49, 50, 51,
                                56, 57, 58, 59, 60, 61, 62,
                                67, 68, 69, 70, 71, 72, 73,
                                        80, 81, 82,
                                        91, 92, 93};
    public Button[] pegPlaces;
    public Sprite[] pegIcons;
    public int[] userChoosedPair = new int[2];
    public int pegsLeft;
    public int movesAvailable;
    public TMP_Text pegsLeftText;
    public GameObject gameOverPopout;
    public TMP_Text gameOverTitle;
    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        pegsLeft = 32;
        pegsLeftText.text = pegsLeft.ToString();

        for (int i = 0; i < 33; i++)
        {  
            pegPlaces[buttonsAvailable[i]].image.sprite = pegIcons[1];
            if (buttonsAvailable[i] == 59)
            {
                pegPlaces[59].image.sprite = pegIcons[0];
            }
            pegPlaces[buttonsAvailable[i]].interactable = true;
        }
        //pegPlaces[59].image.sprite = pegIcons[0];
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeUserChoicePair(int choice)
    {
        // check if the chpice is already made. If it has, clear
        if (userChoosedPair[0] != 0 && userChoosedPair[1] != 0)
        {
            userChoosedPair = new int[2];
        }
        // if user hasn't made any choice
        if (userChoosedPair[0] == 0 && userChoosedPair[1] == 0)
        {
            // if the first user choice is valid
            if (pegPlaces[choice].image.sprite == pegIcons[1])
            {
                userChoosedPair[0] = choice;
            }
        }
        // if user has made only one choice 
        else if (userChoosedPair[0] != 0 && userChoosedPair[1] == 0)
            {
                // if the second user choice is valid
                if (pegPlaces[choice].image.sprite == pegIcons[0])
                {
                    userChoosedPair[1] = choice;
                    isValidMove(userChoosedPair);
                } else {
                    userChoosedPair[0] = choice;
                }
            }
    }
    public void isValidMove(int[] userInput)
    {
        bool flag = false;
        // if user has made full choice
        if (userInput.Length == 2)
        {
            int from = userInput[0];
            int to = userInput[1];
            // check if it is valid east move
            if (from - to == -2 && pegPlaces[from + 1].image.sprite == pegIcons[1])
            {
                Debug.Log("Valid east move.");
                flag = true;
                pegPlaces[from + 1].image.sprite = pegIcons[0];
                pegPlaces[from].image.sprite = pegIcons[0];
                pegPlaces[to].image.sprite = pegIcons[1];
                pegsLeft -= 1;
                pegsLeftText.text = pegsLeft.ToString();
            }
            // check if it is valid west move
            else if (from - to == 2 && pegPlaces[from - 1].image.sprite == pegIcons[1])
            {
                Debug.Log("Valid west move.");
                flag = true;
                pegPlaces[from - 1].image.sprite = pegIcons[0];
                pegPlaces[from].image.sprite = pegIcons[0];
                pegPlaces[to].image.sprite = pegIcons[1];
                pegsLeft -= 1;
                pegsLeftText.text = pegsLeft.ToString();
            }
            // check if it is valid north move
            else if (from - to == 22 && pegPlaces[from - 11].image.sprite == pegIcons[1])
            {
                Debug.Log("Valid north move.");
                flag = true;
                pegPlaces[from - 11].image.sprite = pegIcons[0];
                pegPlaces[from].image.sprite = pegIcons[0];
                pegPlaces[to].image.sprite = pegIcons[1];
                pegsLeft -= 1;
                pegsLeftText.text = pegsLeft.ToString();
            }
            // check if it is valid south move
            else if (from - to == -22 && pegPlaces[from + 11].image.sprite == pegIcons[1])
            {
                Debug.Log("Valid south move.");
                flag = true;
                pegPlaces[from + 11].image.sprite = pegIcons[0];
                pegPlaces[from].image.sprite = pegIcons[0];
                pegPlaces[to].image.sprite = pegIcons[1];
                pegsLeft -= 1;
                pegsLeftText.text = pegsLeft.ToString();
            }
            userChoosedPair = new int[2];
        }
        if (flag == true)
        {
            checkGameEnd(pegsLeft, buttonsAvailable);
        }
    }

    public void checkGameEnd(int pegsLeft, int[] buttonsAvailable) 
    {
        Debug.Log("checking for game end.");
        movesAvailable = 0;
        if (pegsLeft <= 17)
        {
            for (int i = 0; i < buttonsAvailable.Length; i++)
            {
                if (pegPlaces[buttonsAvailable[i]].image.sprite == pegIcons[1])
                {
                    if (pegPlaces[buttonsAvailable[i] + 1] != null && pegPlaces[buttonsAvailable[i] + 2] != null)
                    {
                        if (pegPlaces[buttonsAvailable[i] + 1].image.sprite == pegIcons[1] && pegPlaces[buttonsAvailable[i] + 2].image.sprite == pegIcons[0])
                        {
                            movesAvailable += 1;
                        }
                    }
                    if (pegPlaces[buttonsAvailable[i] - 1] != null && pegPlaces[buttonsAvailable[i] - 2] != null)    
                    {    
                        if (pegPlaces[buttonsAvailable[i] - 1].image.sprite == pegIcons[1] && pegPlaces[buttonsAvailable[i] - 2].image.sprite == pegIcons[0])
                        {
                            movesAvailable += 1;
                        }
                    }
                    if (pegPlaces[buttonsAvailable[i] + 11] != null && pegPlaces[buttonsAvailable[i] + 22] != null)
                    {    
                        if (pegPlaces[buttonsAvailable[i] + 11].image.sprite == pegIcons[1] && pegPlaces[buttonsAvailable[i] + 22].image.sprite == pegIcons[0])
                        {
                            movesAvailable += 1;
                        }
                    }
                    if (pegPlaces[buttonsAvailable[i] - 11] != null && pegPlaces[buttonsAvailable[i] - 22] != null)
                    {    
                        if (pegPlaces[buttonsAvailable[i] - 11].image.sprite == pegIcons[1] && pegPlaces[buttonsAvailable[i] - 22].image.sprite == pegIcons[0])
                        {
                            movesAvailable += 1;
                        }
                    }
                }
            }
            if (movesAvailable == 0)
            {
                Debug.Log("game over.");
                showGameOverPopout();
            }
        }
    }

    public void showGameOverPopout() {
        for (int i = 0; i < 33; i++)
        {
            pegPlaces[buttonsAvailable[i]].interactable = false;
        }
        if (pegsLeft == 1)
        {  
            gameOverTitle.text = "You Win!";
            gameOverPopout.SetActive(true);
        } else
        {
            gameOverTitle.text = "Game Over";
            gameOverPopout.SetActive(true);
        }
    }

    public void restartScene()
    {
        GameSetup();
    }

    public void sceneSwitcher()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
