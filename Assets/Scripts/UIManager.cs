using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] Text health;
    [SerializeField] Text ammo;
    [SerializeField] GameObject redPanel;
    [SerializeField] GameObject playerDeadPanel;
    [SerializeField] GameObject reloadingText;
    [SerializeField] SwitchWeapons weaponSwitcher;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        health.text = player.GetHealth().ToString();
        ammo.text = player.weaponHolder.GetEquippedWeapon().GetComponent<WeaponController>().GetAmmoInMag() + " " +
        player.weaponHolder.GetEquippedWeapon().GetComponent<WeaponController>().GetTotalAmmo();
        reloadingText.SetActive(weaponSwitcher.GetEquippedWeapon().GetComponent<WeaponController>().GetIsReloading());
    }

    public void FlashRed()
    {
        redPanel.SetActive(true);
        redPanel.GetComponent<Animator>().Play("Red Damage Flash");
        Invoke("DisableRedPanel", 0.35f);
    }

    private void DisableRedPanel()
    {
        redPanel.SetActive(false);
    }

    public void PlayerDeadPanel()
    {
        playerDeadPanel.SetActive(true);
    }
}
