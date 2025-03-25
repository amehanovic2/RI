using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;


    private void Awake()
    {
        // Provjera da li je igra pokrenuta prvi put
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1); // Postavljanje na 1
            PlayerPrefs.SetInt("FirstRun", 1); // Oznaka da više nije prvi put
            PlayerPrefs.Save(); // Čuvanje promjena
        }

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        unlockedLevel = Mathf.Clamp(unlockedLevel, 1, buttons.Length); // Osiguranje granica

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }


    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);  
    }
}
