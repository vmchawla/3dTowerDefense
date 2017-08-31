using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPointsTransforms;
    [SerializeField] private float _speed = 5f;

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
                GameManager.Instance.UnRegisterEnemy(this);
            }
        }
    }

}


