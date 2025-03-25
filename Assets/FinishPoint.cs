using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(HandleFinish());
        }
    }

    IEnumerator HandleFinish()
    {
        audioManager.PlaySFX(audioManager.checkpoint);

        // Čekaj 2 sekunde
        yield return new WaitForSeconds(2f);

        // Otključavanje novog nivoa
        UnlockNewLevel();

        // Prijelaz na sljedeći nivo
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Provjera da li je sljedeći nivo unutar opsega postojećih scena
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Prijelaz na sljedeći nivo
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    /*private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            audioManager.PlaySFX(audioManager.checkpoint);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }*/

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
