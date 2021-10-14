using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUI : MonoBehaviour
{ 
    public static GeneralUI uiScript;
    public GameObject uiTapToPlay, uiGame, uiCompleted, uiFailed;
    private StickMovement movementScript;

    void Awake()
    {
        uiScript = this;
    }
    void Start()
    {
        StartCoroutine(SetUIAtStart());
        movementScript = StickMovement.movementScript;
    }

    void Update()
    {
        
    }

    IEnumerator SetUIAtStart()
    {
        uiTapToPlay.SetActive(true);
        yield return new WaitForEndOfFrame();
        movementScript.isGameStarted = false;
        Time.timeScale = 0;
    }
    public void TapToPlay()
    {
        movementScript.isGameStarted = true;
        uiTapToPlay.SetActive(false);
        Time.timeScale = 1;
        uiGame.SetActive(true);
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
