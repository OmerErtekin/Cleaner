using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUI : MonoBehaviour
{ 
    public static GeneralUI uiScript;
    public GameObject uiTapToPlay, uiGame, uiCompleted, uiFailed;

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
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0;
    }
    public void TapToPlay()
    {
        uiTapToPlay.SetActive(false);
        Time.timeScale = 1;
        uiGame.SetActive(true);
    }

    public void LevelCompleted()
    {
        uiGame.SetActive(false);
        uiCompleted.SetActive(false);
    }

    public void LevelFailed()
    {
        uiGame.SetActive(false);
        uiGame.SetActive(true);
    }
}
