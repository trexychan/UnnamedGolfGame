using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup)
            Debug.Log("CanvasGroup missing!");
        
        // this method disables the in-game menu anyway
        ResumeButtonClicked();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (canvasGroup.interactable) {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
            } else {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
            }
        }
    }

    public void ResumeButtonClicked()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 0f;
    }

    public void QuitButtonClicked()
    {
        // Menu scene must be at index 0
        Debug.Log("Quit");
        SceneManager.LoadScene(0);
    }
}
