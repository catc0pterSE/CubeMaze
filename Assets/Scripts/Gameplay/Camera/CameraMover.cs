using System.Collections;
using Model;
using Model.Cube;
using UnityEngine;
using Face = Gameplay.Cube.Face;
using Vector3 = UnityEngine.Vector3;

namespace Gameplay.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Cube.Cube _cube;

        private Face _currentFaceFacing;
        private Coroutine _moveRoutine;


        private void Start()
        {
            _currentFaceFacing = _cube.GetFaceView(CubeFaceType.Front);
            transform.position = _currentFaceFacing.CameraPosition;
            transform.LookAt(_cube.transform, transform.up);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangePosition(transform.up);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ChangePosition(-transform.right);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ChangePosition(-transform.up);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                ChangePosition(transform.right);
            }
        }

        private void ChangePosition(Vector3 direction)
        {
            Face selectedNeighbour = _currentFaceFacing;
            float maxDotProduct = float.MinValue;

            foreach (Face negihbour in _currentFaceFacing.Neigbours)
            {
                Vector3 toNewPosition = negihbour.CameraPosition - transform.position;
                float dotProduct = Vector3.Dot(direction, toNewPosition);

                if (dotProduct > maxDotProduct)
                {
                    selectedNeighbour = negihbour;
                    maxDotProduct = dotProduct;
                }
            }

            _currentFaceFacing = selectedNeighbour;

            if (_moveRoutine!=null)
                StopCoroutine(_moveRoutine);

            _moveRoutine = StartCoroutine(Move(_currentFaceFacing.CameraPosition));
        }

        private IEnumerator Move( Vector3 newPosition)
        {
            while (Vector3.Distance(transform.position, newPosition) > 0.1f)
            {
                transform.LookAt(_cube.transform, transform.up);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 5);
                yield return null;
            }
        }
    }
}