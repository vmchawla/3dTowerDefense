using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float range = 15f;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private int _cost;
    [SerializeField] private int _upgradeCost;

    [Header("Unity Setup Fields")]
    [SerializeField] private Transform _partToRotate;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Vector3 offset;


    [Header("Use Laser")]
    [SerializeField] private bool useLaser = false;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private Light impactLight;
    [SerializeField] private int damageoverTime = 30;
    [SerializeField] private float slowPerct = 0.5f;


    private float _fireCountdown = 0f;
    private Transform _target;
    private Enemy targetEnemy;

    public int Cost
    {
        get { return _cost; }
    }

    public int UpgradeCost
    {
        get { return _upgradeCost; }
    }

    void Start () {
        GameManager.Instance.RegisterTurret(this);
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        

	    if (_target == null)
	    {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
	        return;
	    }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        } else
        {
            if (_fireCountdown <= 0f)
            {
                Shoot();
                _fireCountdown = 1f / _fireRate;
            }
            _fireCountdown -= Time.deltaTime;
        }


	}

    void LockOnTarget()
    {
        Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(_partToRotate.rotation, lookRotation, Time.deltaTime * _rotationSpeed).eulerAngles;
        _partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageoverTime * Time.deltaTime, targetEnemy);
        targetEnemy.Slow(slowPerct);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, transform.position + offset);
        lineRenderer.SetPosition(1, _target.position);

        Vector3 dir = (transform.position + offset) - _target.position;
        impactEffect.transform.position = _target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        
    } 

    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
        
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
            targetEnemy = nearestEnemy.GetComponent<Enemy>();

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
