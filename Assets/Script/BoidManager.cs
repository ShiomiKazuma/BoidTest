using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [SerializeField, CustomLabel("生成するプレハブ")] private GameObject _boidPrefab;
    [SerializeField, CustomLabel("生成人数")] private int _boidCount = 100;
    [SerializeField, CustomLabel("生成範囲")] private float _spawnRadius = 50f;
    public GameObject Leader;
    List<Boid> boids = new List<Boid>();
    public List<Boid> Boids => boids;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBoids();
    }

    /// <summary>
    /// 群衆の生成メソッド
    /// </summary>
    private void SpawnBoids()
    {
        //群衆の生成
        for (int i = 0; i < _boidCount; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * _spawnRadius;
            GameObject boid = Instantiate(_boidPrefab, new Vector3(spawnPosition.x, 2, spawnPosition.z), Quaternion.identity);
            var boidScript = boid.GetComponent<Boid>();
            boidScript.Leader = Leader;
            boids.Add(boidScript);
        }
    }
}
