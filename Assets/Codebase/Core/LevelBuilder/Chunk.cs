using System.Collections;
using UnityEngine;

namespace Codebase.Core.LevelBuilders
{
    public class Chunk : MonoBehaviour
    {
        public Vector3 StartPoint => _startPointMarker.position;
        public Vector3 EndPoint => _endPointMarker.position;
        public Vector3 Center => (StartPoint + EndPoint) / 2f;

        [SerializeField] private Transform _startPointMarker;
        [SerializeField] private Transform _endPointMarker;
        private Bounds? _bounds;

        public Bounds GetBounds()
        {
            if (_bounds.HasValue)
                return _bounds.Value;

            Vector3 min = Vector3.Min(StartPoint, EndPoint);
            Vector3 max = Vector3.Max(StartPoint, EndPoint);
            _bounds = new Bounds((min + max) / 2f, max - min);
            return _bounds.Value;
        }
    }
}