using System;
using Model;
using UnityEngine;

namespace test
{
    public class CameraOrientator: MonoBehaviour
    {
        [SerializeField] private DirectionNew _directionNew;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            _directionNew = (DirectionNew)GetOrientation();
        }

        public int GetOrientation()
        {
            float zRotation = transform.localRotation.eulerAngles.z +45;

            if (zRotation < 0) zRotation +=  360;

            if (zRotation > 360) zRotation -= 360;
            
            int index = Mathf.FloorToInt(zRotation / 90f);
            return index;
        }
    }
}