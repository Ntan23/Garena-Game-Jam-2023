using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootPoint; 
    [SerializeField] private Transform target;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float nextFireTime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDmg;
    private float shootCount;
    private GameManager gm;
    private AudioManager am;
    private PlayerHealth playerHealth;

    void Start() 
    {
        gm = GameManager.instance;
        am = AudioManager.instance;

        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // pivot arrow di tengah player
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;

            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            shootPoint.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    void FixedUpdate()
    {
        if (Time.time > nextFireTime && shootCount < 6 && playerHealth.GetHP() > 0)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; 
            
            if(shootCount == 5) StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        shootCount = 0;
        nextFireTime = Time.time + 1f / fireRate; 
    }

    public void UpgradeDamage() 
    {
        bulletDmg += 2;

        gm.CloseLevelUpUI();
    }
    
    void Shoot()
    {
        shootCount++; 
        if(shootCount < 6) am.PlayPlayerShootSFX();
        if(shootCount == 6) am.PlayEndShootSFX();

        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Camera.main.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 shootDirection = (targetPosition - shootPoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

        bullet.transform.right = shootDirection;

        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

        Destroy(bullet, 1.0f);

    }

    public float GetBulletDmg() 
    {
        return bulletDmg;
    }
}
