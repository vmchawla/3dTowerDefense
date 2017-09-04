using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _health = 100;
    [SerializeField] private int _value = 50;

    [Header("Unity Setup")]
    [SerializeField] private Transform[] _wayPointsTransforms;
    [SerializeField] private GameObject _deathEffect;

    private int _targetWayPoint = 0;
    private Rigidbody rb;
 

    void Awake()
    {
        Assert.IsNotNull(_wayPointsTransforms);
    }
    void Start()
    {
        GameManager.Instance.RegisterEnemy(this);
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        //if (_targetWayPoint < _wayPointsTransforms.Length)
        //{
            
        //    transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
        //}

        transform.LookAt(_wayPointsTransforms[_targetWayPoint]);

    }

    void FixedUpdate()
    {
        if (_targetWayPoint < _wayPointsTransforms.Length)
        {
            Vector3 dir = _wayPointsTransforms[_targetWayPoint].position - transform.position;
            rb.MovePosition(transform.position + dir.normalized * _speed * Time.deltaTime);
            //rb.MoveRotation(transform.position + dir.normalized * _speed);
        }

    }





    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "waypoint")
        {
            _targetWayPoint++;
            if (_targetWayPoint >= _wayPointsTransforms.Length)
            {
                GameManager.Instance.UnRegisterAndDestroyEnemy(this);
                PlayerStats.Instance.LoseALife();
            }
        }
    }

    public void TakeDamage(int amount, Enemy enemy)
    {
        _health -= amount;
        if (_health <= 0)
        {
            Die(enemy);
        }

    }

    void Die(Enemy enemy)
    {
        GameObject deathEffect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffect, 5f);
        GameManager.Instance.UnRegisterAndDestroyEnemy(enemy);
        PlayerStats.Instance.AddMoney(_value);

    }

}


