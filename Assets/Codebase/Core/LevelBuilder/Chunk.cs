using System.Collections;
using UnityEngine;

namespace Codebase.Core.LevelBuilders
{
    public class Chunk : MonoBehaviour
    {
        public Vector3 StartPoint => _startPointMarker.position;
        public Vector3 EndPoint => _endPointMarker.position;
        public Bounds LocalSpawnArea => _spawnBounds;

        [SerializeField] private Transform _startPointMarker;
        [SerializeField] private Transform _endPointMarker;
        [SerializeField] private Bounds _spawnBounds;
        private Bounds? _bounds;

        public Bounds GetBounds()
        {
            if (!_bounds.HasValue)
                CalculateBounds();
            return _bounds.Value;
        }

        public void RecalculateBounds()
        {
            CalculateBounds();
        }

        private void CalculateBounds()
        {
            Vector3 min = Vector3.Min(StartPoint, EndPoint);
            Vector3 max = Vector3.Max(StartPoint, EndPoint);
            _bounds = new Bounds((min + max) / 2f, max - min);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_spawnBounds.center, _spawnBounds.size);
        }
    }
}