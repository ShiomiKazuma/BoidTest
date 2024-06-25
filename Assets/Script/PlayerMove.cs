using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Move
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        /// <summary>プレイヤーのRigidbody </summary>
        private Rigidbody _rb;
        /// <summary>プレイヤーの移動方向 </summary>
        private Vector3 _dir;
        /// <summary>プレイヤーの進行方向を保存する </summary>
        private Quaternion _targetRotation;
        [Header("プレイヤーの回転速度")]
        [SerializeField, Tooltip("プレイヤーの回転速度")]
        private float _rotateSpeed = 10f;

        [Header("プレイヤーの移動速度")]
        [SerializeField, Tooltip("プレイヤーの移動速度")]
        private float _moveSpeed = 10f;
        
        private float _h, _v;
        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            _h = Input.GetAxisRaw("Horizontal");
            _v = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            if(_h != 0 || _v != 0)
            {
                //移動処理をする
                Vector2 velocity = new Vector2(_h, _v);
                OnMove(velocity);
            }
            else
            {
                _dir = Vector3.zero;
                _targetRotation = this.transform.rotation;
            }
        }

        /// <summary>
        /// プレイヤーの移動処理のメソッド
        /// </summary>
        /// <param name="vec">移動のインプット処理</param>
        private void OnMove(Vector2 vec)
        {
            //プレイヤーの移動方向を決める
            _dir = new Vector3(vec.x, 0, vec.y);
            _dir = Camera.main.transform.TransformDirection(_dir);
            _dir.y = 0;
            _dir = _dir.normalized;

            //滑らかに進行方向に回転させる
            if(_dir.magnitude > 0)
            {
                _targetRotation = Quaternion.LookRotation(_dir, Vector3.up);
            }

            //回転をさせる
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed);
            
            //移動させる
            _rb.AddForce(_dir * _moveSpeed, ForceMode.Force);
        }
    }

}