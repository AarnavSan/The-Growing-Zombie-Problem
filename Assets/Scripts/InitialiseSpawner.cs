using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombieSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Range");
            zombieSpawner.SetActive(true);
        }
    }
}
