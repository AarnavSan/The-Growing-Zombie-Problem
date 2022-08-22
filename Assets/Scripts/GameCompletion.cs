using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompletion : MonoBehaviour
{
    public GameObject completionPanel;
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            completionPanel.SetActive(true);
            gameManager.PauseGame();
        }
    }
}
