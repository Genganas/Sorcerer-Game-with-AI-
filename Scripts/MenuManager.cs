using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject creditsScene;
    [SerializeField] GameObject tutorialScene;
    [SerializeField] GameObject settingsScene;
    [SerializeField] GameObject mainmenu;

    public void Start()
    {
        creditsScene.SetActive(false);
        tutorialScene.SetActive(false);
        settingsScene.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ChooseScene()
    {
        SceneManager.LoadScene("ChooseGameScene");
    }
    public void ChooseSinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }
    public void ChooseMultiplePlayer() 
    {
        SceneManager.LoadScene("MultiplayerScene");
    }
    public void Exit()
    {
        Application.Quit(); 
    }
    public void Return()
    {
        mainmenu.SetActive(true);
        creditsScene.SetActive(false);
        settingsScene.SetActive(false);
        tutorialScene.SetActive(false);
    }
    public void Credits()
    {
        creditsScene.SetActive(true);
      
    }
    public void Settings()
    {
        settingsScene.SetActive(true);
        
    }

    public void Tutorial()
    {
        tutorialScene.SetActive(true );
       
    }
}
