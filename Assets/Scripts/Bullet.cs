using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Bullet : MonoBehaviour {
    [Header("Attributes")]
    [SerializeField] private float _speed = 20f;

    [Header("Unity Setup")]
    [SerializeField] private GameObject impactEffect;



    private Transform _target;

    void Awake()
    {
        Assert.IsNotNull(impactEffect);
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (_target == null)
        {
            print("Target is null");
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;

        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

	}

    public void Seek(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {

        HitTarget();
    }

    public void HitTarget()
    {
        GameObject effectInst = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInst, 2f);

        Destroy(gameObject);
    }
}
