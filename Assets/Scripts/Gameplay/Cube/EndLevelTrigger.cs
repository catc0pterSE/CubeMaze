using System;
using Gameplay.Ball;
using UnityEngine;

namespace Gameplay.Cube
{
    [RequireComponent(typeof(BoxCollider))]
    public class EndLevelTrigger : MonoBehaviour
    {
        public event Action BallReachedEnd;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BallGravity>(out _))
            {
                BallReachedEnd?.Invoke();
            }
        }
    }
}