using System;
using UnityEngine;

namespace test
{
    public class Rotator : MonoBehaviour
    {
        private bool newDeltaObtained;
        private Quaternion rotateBy;

        public void Rotate(float rotateLeftRight, float rotateUpDown)
        {
            Camera cam = Camera.main;
            Vector3 relativeUp = cam.transform.TransformDirection(Vector3.forward);
            Vector3 relativeRight = cam.transform.TransformDirection(Vector3.right);
            Vector3 objectRelativeUp = transform.InverseTransformDirection(relativeUp);
            Vector3 objectRelaviveRight = transform.InverseTransformDirection(relativeRight);
            rotateBy = Quaternion.AngleAxis(rotateLeftRight / gameObject.transform.localScale.x * 3f, objectRelativeUp)
                       * Quaternion.AngleAxis(-rotateUpDown / gameObject.transform.localScale.x * 3f,
                           objectRelaviveRight);

            newDeltaObtained = true;
        }
        
        void Update()
        {
            if (Input.GetMouseButton(0) == false)
                return;

            Rotate(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));

            if (newDeltaObtained)
            {
               transform.localRotation *= rotateBy;

               newDeltaObtained = false;
            }
        }
    }
}