using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    //Externally referenced objects
    [SerializeField] public GameObject[] weapons;

    //private variables
    GameObject equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        equippedWeapon = weapons[0];
        equippedWeapon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(2);
        }
    }

    private void SwitchWeapon(int a)
    {
        if(!equippedWeapon.GetComponent<WeaponController>().GetIsReloading())
        {
            a--;
            equippedWeapon.SetActive(false);
            weapons[a].SetActive(true);
            equippedWeapon = weapons[a];
        } 
    }

    public GameObject GetEquippedWeapon()
    {
        return equippedWeapon;
    }
}
