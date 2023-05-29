using UnityEngine;

namespace test
{
    public class CameraRotator: MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 3;

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                float rotation = Input.GetAxis("Mouse X");
                Vector3 currentRotation = transform.rotation.eulerAngles;
                currentRotation.z += rotation*_rotationSpeed;
                transform.rotation = Quaternion.Euler(currentRotation);
            }
        }
        
    }
}