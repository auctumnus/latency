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
        [SerializeField] private new Camera camera;
        private Transform _cameraTransform;
        void Update()
        {
            float level = sensitivity;
            float f = moveSensitivity;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                level *= 0.1f;
                f *= 0.1f;
            }
            if (Input.GetKey(KeyCode.Minus))
            {
                zoomLevel += level;
            }
            else if(Input.GetKey(KeyCode.Equals))
            {
                zoomLevel -= level;
            }
            if (zoomLevel < min)
                zoomLevel = min;
            camera.orthographicSize = zoomLevel;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 position = _cameraTransform.position;
                position.x -= f;
                _cameraTransform.position = position;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 position = _cameraTransform.position;
                position.x += f;
                _cameraTransform.position = position;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 position = _cameraTransform.position;
                position.y += f;
                _cameraTransform.position = position;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 position = _cameraTransform.position;
                position.y -= f;
                _cameraTransform.position = position;
            }
        }

        private void Start()
        {
            _cameraTransform = camera.transform;
        }
    }
}