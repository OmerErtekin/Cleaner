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
        levels.transform.GetChild(PlayerPrefs.GetInt("Level")).gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator FinishTheGame()
    {        
        if (movementScript.isCameToFinish)
        {
            yield return new WaitForSeconds(0.5f);
            movementScript.StopTheStick();
            uiScript.LevelCompleted();
        }
        else
        {
            movementScript.StopTheStick();
            uiScript.LevelFailed();
        }
    }
}
