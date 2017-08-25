using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _timeBetweenEachSpawn = 0.5f;
    //[SerializeField] private Text _waveCountdownText;

    private float countdown;
    private List<Enemy> _enemyList;

    public List<Enemy> EnemyList
    {
        get { return _enemyList; }
    }


    private int waveNumber = 1;

    void Awake()
    {
        Assert.IsNotNull(_spawnPoint);
        Assert.IsNotNull(_enemyPrefab);
    }


	void Start ()
	{
        _enemyList = new List<Enemy>();
	    StartCoroutine(SpawnWave());

	}
	
	// Update is called once per frame
	void Update ()
	{


	}

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            Instantiate(_enemyPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
            yield return new WaitForSeconds(_timeBetweenEachSpawn);
        }
        yield return new WaitForSeconds(_timeBetweenWaves);
        waveNumber++;
        StartCoroutine(SpawnWave());
    }

    public void RegisterEnemy(Enemy enemy)
    {
        _enemyList.Add(enemy);
    }

    public void UnRegisterEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
