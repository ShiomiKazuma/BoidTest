using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoidParam")]
public class BoidParam : ScriptableObject
{
    [CustomLabel("隣人と判定する距離")] public float NeighborDistance = 3f;
    [CustomLabel("分離の重み")] public float SeparationWeight = 1.5f;
    [CustomLabel("整列の重み")] public float AligmentWeight = 1f;
    [CustomLabel("結合の重み")] public float CohesionWeight = 1f;
    [CustomLabel("リーダーに集まる重み")] public float FollowReaderWeight = 2f;
    [CustomLabel("リーダーへの追従停止距離")] public float ReaderDistance = 2f;
    [CustomLabel("リーダーの後ろのオフセット距離")] private float ReaderBackOffsetDistance = 2f;
}

//テスト用コメント