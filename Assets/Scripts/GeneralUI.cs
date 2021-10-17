using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralUI : MonoBehaviour
{ 
    public static GeneralUI uiScript;
    public TMP_Text tapLevelText, gameLevelText;
    public GameObject uiTapToPlay, uiGame, uiCompleted, uiFailed;
    private StickMovement movementScript;

    void Awake()
    {
        uiScript = this;
    }
    void Start()
    {
        StartCoroutine(SetUIAtStart());
    }

    void Update()
    {
        
    }

    IEnumerator SetUIAtStart()
    {
        uiTapToPlay.SetActive(true);
        tapLevelText.text = "Level " + PlayerPrefs.GetInt("LevelNo").ToString();
        yield return new WaitForEndOfFrame();
        movementScript = StickMovement.movementScript;
        movementScript.isGameStarted = false;
        Time.timeScale = 0;
    }
    public void TapToPlay()
    {
        movementScript.isGameStarted = true;
        uiTapToPlay.SetActive(false);
        Time.timeScale = 1;
        uiGame.SetActive(true);
        gameLevelText.text = "Level " + PlayerPrefs.GetInt("LevelNo").ToString();
    }

    public void LevelCompleted()
    {
        uiGame.SetActive(false);
        uiCompleted.SetActive(true);
    }

    public void LevelFailed()
    {
        uiGame.SetActive(false);
        uiFailed.SetActive(true);
    }

}
