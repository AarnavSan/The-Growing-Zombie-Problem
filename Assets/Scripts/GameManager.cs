using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerData player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((player.GetHealth() <= 0) && Input.GetKeyDown(KeyCode.F))
        {
            LoadSceneNumber(0);
        }
    }

    //Pause Game
    public void PauseGame()
    {
        Time.timeScale = 0;
        player.transform.GetComponent<FirstPersonController>().enabled = false;
    }

    //Resume Game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        player.transform.GetComponent<FirstPersonController>().enabled = true;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void LoadSceneNumber(int n)
    {
        SceneManager.LoadScene(n);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }
}
