using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Boid : MonoBehaviour
{
    [NonSerialized] public GameObject Leader;
    [SerializeField] private float _speed = 5f;
    public BoidParam Param;
    private Vector3 _velocity;
    private Vector3 _offsetPosition; //オフセットの位置を保存する
    private Rigidbody _rb;
    private List<Boid> _neighbors = new List<Boid>();

    private Vector3 _alignment;
    private Vector3 _cohesion;
    private Vector3 _separation;
    private Vector3 _leaderDirction;
    private Vector3 _leaderDirection;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _velocity = transform.forward * _speed;
        _offsetPosition = transform.position;
    }

    private void Update()
    {
        _alignment = Vector3.zero;
        _cohesion = Vector3.zero;
        _separation = Vector3.zero;
        _leaderDirction = Vector3.zero;
        //プレイヤーの後ろに並ぶようにする
        _offsetPosition = Leader.transform.position - Leader.transform.forward * Param.ReaderOffsetDistance;
        SerchNeighbors();
        GetAverageSeparation();
        GetAverageAlignment();
        GetCohesion();
        float leaderDistanceSqr = (Leader.transform.position - transform.position).sqrMagnitude;
        
        if (leaderDistanceSqr > Param.LeaderDistance * Param.LeaderDistance)
        {
            _leaderDirction = _offsetPosition - transform.position;
        }
        else if(leaderDistanceSqr <= Param.LeaderDistance * Param.LeaderDistance || _neighbors.Count(n => n._velocity == Vector3.zero) > 0)
        {
            // リーダーに近すぎる場合はプレイヤーに押されないようにするために、Boidがプレイヤーから離れる方向に少し移動する
            // Vector3 awayFromLeader = (transform.position - Leader.transform.position).normalized;
            // _leaderDirection = awayFromLeader * Param.AvoidLeaderWeight;
            _velocity = Vector3.zero;
            _rb.velocity = Vector3.zero;
            return;
        }// リーダーに近すぎる場合はプレイヤーに押されないようにするために、Boidがプレイヤーから離れる方向に少し移動する
        Vector3 vlc = _alignment * Param.AligmentWeight + _cohesion * Param.CohesionWeight +
                      _separation * Param.SeparationWeight + _leaderDirction * Param.FollowReaderWeight;
        var dt = Time.deltaTime;
        
        var dir = (dt * vlc).normalized;
        var speed = vlc.magnitude;
        _velocity = dir * Mathf.Clamp(speed, Param.MinSpeed, Param.MaxSpeed);
        _rb.velocity = _velocity;
        transform.forward = _velocity.normalized;
    }

    /// <summary>
    /// 近隣個体の情報を取得する処理
    /// </summary>
    private void SerchNeighbors()
    {
        _neighbors.Clear();
        //近隣の個体を検索
        Collider[] colliders = Physics.OverlapSphere(transform.position, Param.NeighborDistance);
        foreach (Collider collider in colliders)
        {
            Boid boid = collider.GetComponent<Boid>();
            if (boid != null && boid != this)
            {
                _neighbors.Add(boid);
            }
        }
    }

    /// <summary>
    /// 分離の処理
    /// </summary>
    private void GetAverageSeparation()
    {
        if (_neighbors.Count == 0) return;
        foreach (var neighbor in _neighbors)
        {
            _separation += transform.position - neighbor.transform.position;
        }

        _separation /= _neighbors.Count;
    }

    /// <summary>
    /// 整列の処理（平均的な向きを取る）
    /// </summary>
    private void GetAverageAlignment()
    {
        if (_neighbors.Count == 0) return;
        foreach (var neighbor in _neighbors)
        {
            _alignment += neighbor._velocity;
        }

        _alignment /= _neighbors.Count;
    }

    /// <summary>
    /// リーダーの個体に向かう処理
    /// </summary>
    private void GetCohesion()
    {
        _cohesion = _offsetPosition - transform.position;
    }
}
