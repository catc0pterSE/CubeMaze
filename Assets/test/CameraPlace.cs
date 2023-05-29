using System;
using System.Collections.Generic;
using Model;
using Modules;
using UnityEngine;

namespace test
{
    public class CameraPlace : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<DirectionNew, CameraPlace> _transitions;

        public CameraPlace GetCameraPlace(int index)
        {
            return _transitions.Get((DirectionNew)index);
        }
    }
}