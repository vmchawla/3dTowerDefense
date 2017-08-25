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
 

    void Awake()
    {
        Assert.IsNotNull(_wayPointsTransforms);
    }
    void Start()
    {
        GameManager.Instance.RegisterEnemy(this);
   
    }

    // Update is called once per frame
    void Update()
    {

        if (_targetWayPoint < _wayPointsTransforms.Length)
        {
            Vector3 dir = _wayPointsTransforms[_targetWayPoint].position - transform.position;
            transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);
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


