using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Boid/Param")]
public class BoidParam : ScriptableObject
{
    [CustomLabel("隣人と判定する距離")] public float NeighborDistance = 3f;
    [CustomLabel("分離の重み")] public float SeparationWeight = 1.5f;
    [CustomLabel("整列の重み")] public float AligmentWeight = 1f;
    [CustomLabel("結合の重み")] public float CohesionWeight = 1f;
    [CustomLabel("リーダーに集まる重み")] public float FollowReaderWeight = 2f;
    [FormerlySerializedAs("ReaderDistance")] [CustomLabel("リーダーへの追従停止距離")] public float LeaderDistance = 2f;
    [CustomLabel("リーダーの後ろのオフセット距離")] public float ReaderOffsetDistance = 2f;
    [CustomLabel("プレイヤーに近づき過ぎたら避ける重み")] public float AvoidLeaderWeight = 2f;
    [CustomLabel("最低速度")] public float MinSpeed = 0f;
    [CustomLabel("最高速度")] public float MaxSpeed = 5f;
}
