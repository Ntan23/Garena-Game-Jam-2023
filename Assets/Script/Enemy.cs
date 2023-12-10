using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public enum Type {
        chaser, tanker, boss
    }

    [SerializeField] private Type enemyType;
    private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float damage;
    private float randomValue;
    private float randomOrbSpawnValue;
    private bool canBeDamaged = true;
    private bool isKilled;
    private GameManager gm;
    private AudioManager am;
    private PlayerShoot playerShoot;
    [SerializeField] private GameObject[] orbs;
    [SerializeField] private ParticleSystem explosionParticleEffect;

    void Start()
    {
        gm = GameManager.instance;
        am = AudioManager.instance;

        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null) playerShoot = player.GetComponent<PlayerShoot>();

        if(enemyType == Type.chaser) speed = gm.GetEnemyChaserSpeed();
        if(enemyType == Type.tanker) health = gm.GetEnemyTankerHealth();
        if(enemyType == Type.boss)
        {
            speed = gm.GetBossSpeed();
            health = gm.GetBossHealth();
            damage = gm.GetBossDamage();
        }
    }

    void Update() => MoveToPlayer();

    private void MoveToPlayer()
    {
        if (player == null) return;
            
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if(Vector3.Distance(player.transform.position, transform.position) > 0.2f) transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet")) 
        {
            Destroy(other.gameObject);
            if(canBeDamaged) StartCoroutine(Delay());
            
            if(health <= 0 && !isKilled)
            {
                isKilled = true;
                transform.localScale = Vector3.zero;
                am.PlayEnemyExplodeSFX();
                explosionParticleEffect.Play();
                DropOrb();

                if(enemyType == Type.chaser) gm.AddScore(10);
                if(enemyType == Type.tanker) gm.AddScore(20);
                if(enemyType == Type.boss) 
                {
                    gm.AddScore(50);
                    gm.CameraShakeEffect();
                }

                Destroy(gameObject, 0.3f);
            } 
        }
    }

    void OnDestroy()
    {
        gm.CheckEnemy();
    }

    public float GetDamage() 
    {
        return damage;
    }

    IEnumerator Delay()
    {
        am.PlayEnemyHitSFX();
        canBeDamaged = false;
        health -= playerShoot.GetBulletDmg();
        yield return new WaitForSeconds(0.05f);
        canBeDamaged = true;
    }

    private void DropOrb()
    {
        randomOrbSpawnValue = Random.value;

        if(randomOrbSpawnValue < 0.7f)
        {
            randomValue = Random.value;

            if(randomValue < 0.9f) Instantiate(orbs[0], transform.position, Quaternion.identity);
            else if(randomValue >= 0.9f) Instantiate(orbs[1], transform.position, Quaternion.identity);
        }
    } 
}
