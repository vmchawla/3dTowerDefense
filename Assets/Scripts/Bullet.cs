using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Bullet : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _explosionRadius = 0f;
    [SerializeField] private int _damage = 20;

    [Header("Unity Setup")]
    [SerializeField] private GameObject impactEffect;



    private Transform _target;
    private Rigidbody rb;

    void Awake()
    {
        Assert.IsNotNull(impactEffect);
    }


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (_target == null)
        {
            print("Target is null");
            Destroy(gameObject);
            return;
        }

        
        transform.LookAt(_target);
        //transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

	}

    public void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 dir = _target.position - transform.position;
            rb.MovePosition(transform.position + dir.normalized * _speed * Time.fixedDeltaTime);
        }


    }

    public void Seek(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        HitTarget(other);
    }

    public void HitTarget(Collider enemy)
    {
        GameObject effectInst = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInst, 5f);

        if (_explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(enemy);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var myCollider in colliders)
        {
            if (myCollider.tag == "Enemy")
            {
                
                Damage(myCollider);

            }
        }
    }

    void Damage(Collider enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        e.TakeDamage(_damage, e);
        
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
