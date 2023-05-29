using System.Collections;
using Model;
using UnityEngine;
using View;

namespace test
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private CubeView _cube;
        [SerializeField] private float _offsetMultiplier = 1.5f;
        [SerializeField] private CameraOrientator _orientator;

        private readonly CubeFacesRelations _cubeFacesRelations = new CubeFacesRelations();

        /*private CubeFaceType _currentlyFacingFaceType;*/

        private FaceView _currentFaceFacing;
        private CameraPlace _currentCameraPlace;

        private Coroutine _moveRoutine;


        private void Start()
        {
            _currentFaceFacing = _cube.GetFaceView(CubeFaceType.Front);
            _currentCameraPlace = _currentFaceFacing.CameraPlace;
            /*_currentlyFacingFaceType = CubeFaceType.Front;*/
            transform.position = GetPositionAgainst(_currentFaceFacing);
            transform.LookAt(_cube.transform, transform.up);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangePosition(DirectionNew.UpNew);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                ChangePosition(DirectionNew.LeftNew);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ChangePosition(DirectionNew.DownNew);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                ChangePosition(DirectionNew.RightNew);
            }
        }

        private void ChangePosition(DirectionNew direction)
        {
            /*if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);

            _moveRoutine = StartCoroutine(Move(direction));*/

           int nextPositionIndex = (int)direction - _orientator.GetOrientation();

            if (nextPositionIndex < 0) nextPositionIndex += 4;

            if (nextPositionIndex > 3) nextPositionIndex -= 4;
            Debug.Log($"{nextPositionIndex}  {(DirectionNew)nextPositionIndex}");

            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);
            _currentCameraPlace = _currentCameraPlace.GetCameraPlace(nextPositionIndex);
            

            _moveRoutine = StartCoroutine(Move(_currentCameraPlace.transform.position));
        }

        private Vector3 GetPositionAgainst(FaceView targetFace)
        {
            return targetFace.CameraPlace.transform.position;
        }

        /*private IEnumerator Move(Direction direction)
        {
            transform.position = GetPositionAgainst(_currentFaceFacing);

            CubeFaceType nextFaceType = _cubeFacesRelations.DirectedNeighbors[_currentlyFacingFaceType][direction];
            _currentlyFacingFaceType = nextFaceType;
            FaceView nextFace = _cube.GetFaceView(nextFaceType);
            _currentFaceFacing = nextFace;
            Vector3 newPosition = GetPositionAgainst(nextFace);

            while (Vector3.Distance(transform.position, newPosition) > 1f)
            {
                transform.LookAt(_cube.transform, transform.up);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 5);
                yield return null;
            }
        }*/

        private IEnumerator Move(Vector3 newPosition)
        {
            while (Vector3.Distance(transform.position, newPosition) > 1f)
            {
                transform.LookAt(_cube.transform, transform.up);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 5);
                yield return null;
            }
        }
    }
}