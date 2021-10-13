using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager managerScript;
    public GameObject levels;
    void Awake()
    {
        managerScript = this;    
    }
    void Start()
    {
        SetLevelAtStart();
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
        for(int i = 0;i<levels.transform.childCount;i++)
        {
             levels.transform.GetChild(i).gameObject.SetActive(false);
        }
        levels.transform.GetChild(PlayerPrefs.GetInt("Level")).gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SetLevelAtStart();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
