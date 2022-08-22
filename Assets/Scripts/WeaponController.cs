using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //Externally Referenced Objects
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip shootingSound;

    //Weapon Stats
    [SerializeField] private int totalMagAmmo = 24;
    [SerializeField] private int maxTotalAmmo = 36;
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private float range = 400.0f;
    [SerializeField] private float fireRate = 10.0f;
    [SerializeField] private float reloadTime = 3.0f;
    
    //private variables
    private int ammoInMag;
    private int totalAmmo;
    private bool canShoot = true;
    private bool isReloading = false;

    private Camera mainCam;
    private AudioSource audioSource;

    WeaponController()
    {
        InitialiseWeaponController();
    }
    void InitialiseWeaponController()
    {
        ammoInMag = totalMagAmmo;
        totalAmmo = maxTotalAmmo;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        InitialiseWeaponController();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Reload"))
        {
            ReloadGun();
        }
    }

    private void GetReferences()
    {
        mainCam = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    private void Shoot()
    {
        if(canShoot)
        {
            if(CheckAmmo())
            {
                canShoot = false;

                //subtract ammo count
                ammoInMag -= 1;

                //Play effects
                muzzleFlash.Play();
                audioSource.PlayOneShot(shootingSound);

                //Raycast to damage
                RaycastHit hit;
                if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        hit.transform.GetComponent<ZombieController>().TakeDamage(damage);
                    }
                }
            }
            else
            {
                ReloadGun();
            }
        }
        else if(!isReloading)
        {
            StartCoroutine(ShootDelay());
        }
    }


    //Couroutine to wait for fire rate
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1.0f/fireRate);
        canShoot = true;
    }

    //Check if player has ammo in mag
    private bool CheckAmmo()
    {
        return ammoInMag > 0;
    }
    
    //Reload Gun
    private void ReloadGun()
    {
        if(ammoInMag < totalMagAmmo)
        {
            canShoot = false;
            isReloading = true;
            if (totalAmmo >= totalMagAmmo)
            {
                ammoInMag = totalMagAmmo;
                totalAmmo -= ammoInMag;
            }
            else
            {
                ammoInMag += totalAmmo;
                totalAmmo = 0;
            }
            StartCoroutine(ReloadDelay());
        }
    }

    //Couroutine to wait for reload
    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        isReloading = false;
    }

    //Add total ammo on pickup
    private void AmmoPickup(int ammoPicked) 
    {
        if(totalAmmo + ammoPicked <= maxTotalAmmo)
        {
            totalAmmo += ammoPicked;
        }
    }

    //Getters and setters
    public int GetAmmoInMag()
    {
        return ammoInMag;
    }

    public int GetTotalAmmo()
    {
        return totalAmmo;
    }
    
    public bool GetIsReloading()
    {
        return isReloading;
    }
}
