using System;
using UnityEngine;

namespace Gameplay.Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallGravity : MonoBehaviour
    {
        [SerializeField] private Transform _gravityOrigin;
        [SerializeField] private float _gravityScale = 30;
        private Rigidbody _rigidbody;

        private void Awake() =>
            _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate() =>
            _rigidbody.velocity = -_gravityOrigin.transform.up * _gravityScale;
    }
}