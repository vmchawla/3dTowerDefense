using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float range = 15f;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _fireRate = 1f;

    [Header("Unity Setup Fields")]
    [SerializeField] private Transform _partToRotate;
    [SerializeField] private Bullet bulletPrefab;

    private float _fireCountdown = 0f;
    private Transform _target;

    void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {

	    if (_target == null)
	    {
	        return;
	    }

	    Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
	    Vector3 rotation = Quaternion.Lerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed).eulerAngles;
	    _partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	    if (_fireCountdown <= 0f)
	    {
	        Shoot();
	        _fireCountdown = 1f / _fireRate;
	    }
	    _fireCountdown -= Time.deltaTime;
	}

    void Shoot()
    {
        Vector3 offset = new Vector3(0f, 1f, 0f);
        Bullet bullet = Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
        print(bullet.transform.position);

        
        if (bullet != null)
        {
            bullet.Seek(_target);
        }

    }

    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;
        foreach (var enemy in GameManager.Instance.EnemyList)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            _target = nearestEnemy.transform;

        }
        else
        {
            _target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
