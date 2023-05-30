using System;
using UnityEngine;

namespace Gameplay.Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallGravity : MonoBehaviour
    {
        [SerializeField] private float _gravityScale = 30;
        
        private Transform _gravityOrigin;
        private Rigidbody _rigidbody;

        public void Initialize(Transform gravityOrigin) =>
            _gravityOrigin = gravityOrigin;
        
        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate() =>
            _rigidbody.velocity = -_gravityOrigin.transform.up * _gravityScale;
    }
}