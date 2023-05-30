using System.Collections;
using Infrastructure.Input;
using Model.Cube;
using UnityEngine;
using Face = Gameplay.Cube.Face;

namespace Gameplay.Camera
{
    public class CameraMover : MonoBehaviour
    {
        private Cube.Cube _cube;
        private Face _currentFaceFacing;
        private Coroutine _moveRoutine;
        private IInputService _inputService;

        public void Initialize(Cube.Cube cube, IInputService inputService)
        {
            _inputService = inputService;
            _cube = cube;
            ToStartPosition();
            SubscribeOnInputService();
        }

        private void OnDisable() =>
            UnsubscribeFromInputService();

        private void SubscribeOnInputService()
        {
            _inputService.UpButtonPressed += ChangePositionUp;
            _inputService.DownButtonPressed += ChangePositionDown;
            _inputService.LeftButtonPressed += ChangePositionLeft;
            _inputService.RightButtonPressed += ChangePositionRight;
        }

        private void UnsubscribeFromInputService()
        {
            _inputService.UpButtonPressed -= ChangePositionUp;
            _inputService.DownButtonPressed -= ChangePositionDown;
            _inputService.LeftButtonPressed -= ChangePositionLeft;
            _inputService.RightButtonPressed -= ChangePositionRight;
        }

        private void ChangePositionUp() =>
            ChangePosition(transform.up);

        private void ChangePositionDown() =>
            ChangePosition(-transform.up);

        private void ChangePositionRight() =>
            ChangePosition(transform.right);

        private void ChangePositionLeft() =>
            ChangePosition(-transform.right);

        private void ToStartPosition()
        {
            _currentFaceFacing = _cube.GetFaceView(CubeFaceType.Front);
            transform.position = _currentFaceFacing.CameraPosition;
            transform.LookAt(_cube.transform, transform.up);
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

            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);

            _moveRoutine = StartCoroutine(Move(_currentFaceFacing.CameraPosition));
        }

        private IEnumerator Move(Vector3 newPosition)
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