using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pausePanelRect, pauseButtonRect, starRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        PausePanelIntro();  
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public async void Resume()
    {
        await PausePanelOutro();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    void PausePanelIntro()
    {
        audioManager.PlaySFX(audioManager.pausePanel);
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
        pauseButtonRect.DOAnchorPosX(65, tweenDuration).SetUpdate(true);
        starRect.DOAnchorPosX(-215, tweenDuration).SetUpdate(true);
    }

    async Task PausePanelOutro()
    {
        audioManager.PlaySFX(audioManager.pausePanel);
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        pauseButtonRect.DOAnchorPosX(-90, tweenDuration).SetUpdate(true);
        starRect.DOAnchorPosX(10, tweenDuration).SetUpdate(true);
    }
}
