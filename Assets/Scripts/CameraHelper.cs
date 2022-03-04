using System;
using UnityEngine;

namespace Scenes
{
    public class CameraHelper: MonoBehaviour
    {
        public float zoomLevel;
        public float sensitivity = 1;
        public float min = 1;
        [SerializeField] private Camera _camera;
        void Update()
        {
            zoomLevel += Input.mouseScrollDelta.y * sensitivity;
            if (Input.GetKey(KeyCode.Minus))
            {
                zoomLevel += sensitivity;
            }
            else if(Input.GetKey(KeyCode.Equals))
            {
                zoomLevel -= sensitivity;
            }
            if (zoomLevel < min)
                zoomLevel = min;
            _camera.orthographicSize = zoomLevel;
        }
    }
}