using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager managerScript;
    public GameObject levels;
    private StickMovement movementScript;
    private GeneralUI uiScript;
    void Awake()
    {
        managerScript = this;    
    }
    void Start()
    {
        SetLevelAtStart();
        movementScript = StickMovement.movementScript;
        uiScript = GeneralUI.uiScript;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            RestartLevel();
        }
    }

    public void SetLevelAtStart()
    {
        if(PlayerPrefs.GetInt("Level") >= levels.transform.childCount)
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        if(PlayerPrefs.GetInt("LevelNo") == 0)
        {
            PlayerPrefs.SetInt("LevelNo", 1);
        }
        levels.transform.GetChild(PlayerPrefs.GetInt("Level")).gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        PlayerPrefs.SetInt("LevelNo", PlayerPrefs.GetInt("LevelNo") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishTheGame()
    {
        movementScript.isGameFinished = true;
        if (movementScript.isCameToFinish)
        {
            uiScript.LevelCompleted();
        }
        else
        {
            uiScript.LevelFailed();
        }
    }
}
