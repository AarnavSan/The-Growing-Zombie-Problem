using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{

    //Externally Referenced Objects
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] Transform player;
    [SerializeField] private ParticleSystem growthVFX;
    [SerializeField] private AudioClip growthSound;

    //private variables
    [SerializeField] float lowerSpawnDelay = 30.0f;
    [SerializeField] float higherSpawnDelay = 60.0f;

    bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
        SpawnZombies();
    }

    //public function to spawn zombies randomly
    public void SpawnZombies()
    {
        int smallZombies, mediumZombies, normalZombies, giantZombies;
        smallZombies = Mathf.FloorToInt(Random.Range(1.0f, 3f));
        mediumZombies = Mathf.FloorToInt(Random.Range(1.0f, 3.0f));
        normalZombies = Mathf.FloorToInt(Random.Range(1.0f, 4.0f));
        giantZombies = Mathf.FloorToInt(Random.Range(1.0f, 2.0f));

        SpawnMultipleZombies(smallZombies, 0);
        SpawnMultipleZombies(mediumZombies, 1);
        SpawnMultipleZombies(normalZombies, 2);
        SpawnMultipleZombies(giantZombies, 3);
    }

    //Spawn multiple zombies
    private void SpawnMultipleZombies(int numOfZombies, int zombieType)
    {
        for (int i = 1; i <= numOfZombies; i++)
        {
            switch(zombieType)
            {
                case 0:
                    SpawnZombieSmall();
                    break;

                case 1:
                    SpawnZombieMedium();
                    break;

                case 2:
                    SpawnZombieNormal();
                    break;

                case 3:
                    SpawnZombieGiant();
                    break;
            }
        }
    }

    //Spawn the smallest zombie
    private void SpawnZombieSmall()
    {
        ZombieController spawnedZombie = Instantiate(zombiePrefab, transform).GetComponent<ZombieController>();
        float attackRange = 0, damage = 0, speed = 0, animationSpeed = 0, attackDelay = 0, zombieHealth = 0, zombieScale = 0;
        attackRange = 1.0f;
        damage = 5.0f;
        speed = 30.0f;
        animationSpeed = 2f;
        attackDelay = 0.5f;
        zombieHealth = 20.0f;
        zombieScale = 0.3f;
        spawnedZombie.IntitializeZombieStats(attackRange, damage, speed, animationSpeed, attackDelay, zombieHealth, player);
        spawnedZombie.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * zombieScale;
        int delay = Mathf.FloorToInt(Random.Range(lowerSpawnDelay, higherSpawnDelay));
        StartCoroutine(GrowZombieDelayed(delay, spawnedZombie));
        StartCoroutine(GrowZombieVFXDelay(delay - 5, spawnedZombie));
    }

    //Spawn the medium zombie
    private void SpawnZombieMedium()
    {
        ZombieController spawnedZombie = Instantiate(zombiePrefab, transform).GetComponent<ZombieController>();
        float attackRange = 0, damage = 0, speed = 0, animationSpeed = 0, attackDelay = 0, zombieHealth = 0, zombieScale = 0;
        attackRange = 1.25f;
        damage = 10.0f;
        speed = 20.0f;
        animationSpeed = 1.25f;
        attackDelay = 1.0f;
        zombieHealth = 50.0f;
        zombieScale = 0.5f;
        spawnedZombie.IntitializeZombieStats(attackRange, damage, speed, animationSpeed, attackDelay, zombieHealth, player);
        spawnedZombie.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * zombieScale;
        int delay = Mathf.FloorToInt(Random.Range(lowerSpawnDelay, higherSpawnDelay));
        StartCoroutine(GrowZombieDelayed(delay, spawnedZombie));
        StartCoroutine(GrowZombieVFXDelay(delay - 5, spawnedZombie));
    }

    //Spawn the normal-sized zombie
    private void SpawnZombieNormal()
    {
        ZombieController spawnedZombie = Instantiate(zombiePrefab, transform).GetComponent<ZombieController>();
        float attackRange = 0, damage = 0, speed = 0, animationSpeed = 0, attackDelay = 0, zombieHealth = 0, zombieScale = 0;
        attackRange = 1.25f;
        damage = 20.0f;
        speed = 10.0f;
        animationSpeed = 1f;
        attackDelay = 1.5f;
        zombieHealth = 100.0f;
        zombieScale = 1.0f;
        spawnedZombie.IntitializeZombieStats(attackRange, damage, speed, animationSpeed, attackDelay, zombieHealth, player);
        spawnedZombie.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * zombieScale;
        int delay = Mathf.FloorToInt(Random.Range(lowerSpawnDelay, higherSpawnDelay));
        StartCoroutine(GrowZombieDelayed(delay, spawnedZombie));
        StartCoroutine(GrowZombieVFXDelay(delay - 5, spawnedZombie));
    }

    //Spawn the giant zombie
    private void SpawnZombieGiant()
    {
        ZombieController spawnedZombie = Instantiate(zombiePrefab, transform).GetComponent<ZombieController>();
        float attackRange = 0, damage = 0, speed = 0, animationSpeed = 0, attackDelay = 0, zombieHealth = 0, zombieScale = 0;
        attackRange = 1.5f;
        damage = 40.0f;
        speed = 5.0f;
        animationSpeed = 0.4f;
        attackDelay = 5f;
        zombieHealth = 300.0f;
        zombieScale = 3.0f;
        spawnedZombie.IntitializeZombieStats(attackRange, damage, speed, animationSpeed, attackDelay, zombieHealth, player);
        spawnedZombie.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * zombieScale;
    }

    //Zombie Growing Recursive Script
    private void GrowZombie(ZombieController zombie)
    {
        int dmg = (int)zombie.GetZombieDamage();
        ZombieController spawnedZombie = Instantiate(zombiePrefab, zombie.transform.position, zombie.transform.rotation).GetComponent<ZombieController>();
        float attackRange = 0, damage = 0, speed = 0, animationSpeed = 0, attackDelay = 0, zombieHealth = 0, zombieScale = 0;
        switch (dmg)
        {
            case 5: //medium zombie
                attackRange = 1.25f;
                damage = 10.0f;
                speed = 20.0f;
                animationSpeed = 1.25f;
                attackDelay = 1.0f;
                zombieHealth = 50.0f;
                zombieScale = 0.5f;
                break;

            case 10: //normal sized zombie
                attackRange = 1.25f;
                damage = 20.0f;
                speed = 10.0f;
                animationSpeed = 1f;
                attackDelay = 1.5f;
                zombieHealth = 100.0f;
                zombieScale = 1.0f;
                break;

            case 20: //Giant zombie
                attackRange = 1.5f;
                damage = 40.0f;
                speed = 5.0f;
                animationSpeed = 0.4f;
                attackDelay = 5f;
                zombieHealth = 300.0f;
                zombieScale = 3.0f;
                break;
        }
        spawnedZombie.IntitializeZombieStats(attackRange, damage, speed, animationSpeed, attackDelay, zombieHealth, player);
        spawnedZombie.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * zombieScale;
        Destroy(zombie.gameObject);
        int delay = Mathf.FloorToInt(Random.Range(lowerSpawnDelay, higherSpawnDelay));
        StartCoroutine(GrowZombieDelayed(delay, spawnedZombie));
        StartCoroutine(GrowZombieVFXDelay(delay - 5, spawnedZombie));
        Debug.Log("Zombie grown");
    }

    //Couroutine to delay GrowZombie
    private IEnumerator GrowZombieDelayed(float t, ZombieController zombie)
    {
        yield return new WaitForSeconds(t);
        GrowZombie(zombie);
    }

    //Couroutine to delay GrowZombie VFX
    private IEnumerator GrowZombieVFXDelay(float t, ZombieController zombie)
    {
        yield return new WaitForSeconds(t);
        PlayGrowthVFX(zombie);
    }

    private void PlayGrowthVFX(ZombieController zombie)
    {
        ParticleSystem particleSystem = Instantiate(growthVFX, zombie.transform).GetComponent<ParticleSystem>();
        particleSystem.transform.localPosition = new Vector3(0, 0, 0);
        particleSystem.Play();  
        zombie.transform.GetComponent<AudioSource>().PlayOneShot(growthSound);
        Destroy(particleSystem, 10.0f);
    }
}
