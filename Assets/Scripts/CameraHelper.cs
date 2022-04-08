using System;
using UnityEngine;

namespace Scenes
{
    public class CameraHelper: MonoBehaviour
    {
        public float zoomLevel;
        public float sensitivity = 0.001f;
        public float moveSensitivity = 0.1f;
        public float min = 1;
        [SerializeField] private Camera _camera;
        private Transform _cameraTransform;
        void Update()
        {
            float sensitivity = this.sensitivity;
            float moveSensitivity = this.moveSensitivity;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                sensitivity *= 0.1f;
                moveSensitivity *= 0.1f;
            }
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

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 position = _cameraTransform.position;
                position.x -= moveSensitivity;
                _cameraTransform.position = position;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 position = _cameraTransform.position;
                position.x += moveSensitivity;
                _cameraTransform.position = position;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 position = _cameraTransform.position;
                position.y += moveSensitivity;
                _cameraTransform.position = position;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 position = _cameraTransform.position;
                position.y -= moveSensitivity;
                _cameraTransform.position = position;
            }
        }

        private void Start()
        {
            _cameraTransform = _camera.transform;
        }
    }
}