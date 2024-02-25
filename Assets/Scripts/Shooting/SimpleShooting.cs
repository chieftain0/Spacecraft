using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;

public class SimpleShooting : MonoBehaviour
{
    [SerializeField] Transform gunPoint;
    [SerializeField] LayerMask enemyMask;

    [SerializeField] float maxHealth;

    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] GameObject laserObject;
    [SerializeField] Transform laserPlaceHolder;

    [SerializeField] EnemyAi enemyManager;

    float health;

    void Start()
    {
        health = maxHealth;

        explosionEffect.Stop();

        //laserEffect.Stop();
    }

    void Update()
    {
        if(Input.GetAxis("RT") > 0.9f)
        {
            Vector3 origin = gunPoint.position;
            Vector3 direction = transform.right;

            LaserEffect();

            RaycastHit hitInfo;
            bool hit = Physics.Raycast(origin, direction, out hitInfo, Mathf.Infinity, enemyMask);

            Debug.DrawRay(origin, direction * 50f, Color.red);

            if (hit)
            {
                Debug.Log("Hit something: " + hitInfo.collider.gameObject.name);
                EnemyShot(hitInfo);
            }
        }
       
    }

    void EnemyShot(RaycastHit hitInfo)
    {
      BoidController boid = hitInfo.transform.gameObject.GetComponent<BoidController>();
      boid.swarmManager._boids.Remove(boid);
      Destroy(boid.gameObject);
      Debug.LogError("Killed enemy!");
      explosionEffect.transform.position = hitInfo.transform.position;
      //explosionEffect.gameObject.SetActive(true);
      explosionEffect.Play();

        enemyManager.enemiesKilled += 1;
        enemyManager.totalScore += 100;
    }

    public void LaserEffect()
    {
        //laserEffect.Play();
        GameObject go = Instantiate(laserObject, laserPlaceHolder.position, transform.localRotation * Quaternion.Euler(0,0,90f));
       
        go.GetComponent<LaserProjectile>().startVelocity = transform.right * 3500f;
        

    }

    

    public void GotHit(float damage)
    {
        health -= damage;
    }
}
