using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Codebase.Core
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;

        [Inject]
        private void Construct(Camera camera)
        {
            _camera = camera;
        }

        private void LateUpdate()
        {
            Quaternion rotation = _camera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}