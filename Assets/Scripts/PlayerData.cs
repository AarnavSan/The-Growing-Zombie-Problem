using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //Externally referenced Variables
    [SerializeField]public SwitchWeapons weaponHolder;
    [SerializeField] public UIManager canvas;
    [SerializeField] public GameManager gameManager;

    //public variables
    public float hitForce = 10.0f;

    //private variables
    private float health;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        health = 500.0f;
        rb = GetComponent<Rigidbody>();
    }

    //Damage player health
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (damage >= 40.0f)
        {
            rb.AddForce(transform.forward * -1 * hitForce);
        }
        canvas.FlashRed();
        if(health <= 0)
        {
            health = 0;
            KillPlayer();
        }
    }

    //Function on PlayerDeath
    private void KillPlayer()
    {
        canvas.PlayerDeadPanel();
        transform.GetComponent<FirstPersonController>().enabled = false;
        transform.GetComponent<FirstPersonController>().lockCursor = false;
    }

    //Getters and Setters
    public int GetHealth()
    {
        return Mathf.FloorToInt(health/5.0f);
    }
}
