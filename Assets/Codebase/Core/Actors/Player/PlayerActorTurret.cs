using UnityEngine;
using Zenject;

namespace Codebase.Core.Actors
{
    public class PlayerActorTurret : MonoBehaviour
    {
        [SerializeField] private Transform _turretRoot;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _rotationSpeed = 90f;
        [SerializeField] private float _maxAngle = 70f;
        private BulletSpawner _spawner;
        private Quaternion _initialRotation;

        [Inject]
        private void Construct(BulletSpawner spawner)
        {
            _spawner = spawner;
        }

        private void Awake()
        {
            _initialRotation = _turretRoot.localRotation;
        }

        public void Fire()
        {
            var bullet = _spawner.SpawnOne();
            bullet.SetPositionAndRotation(_bulletSpawnPoint.position, _turretRoot.rotation);
            bullet.Fire();
        }

        public void Rotate(float direction, float deltaTime)
        {
            if (direction == 0f)
                return;

            direction = Mathf.Clamp(direction, -1, 1);
            float rotationAmount = direction * _rotationSpeed * deltaTime;
            _turretRoot.Rotate(Vector3.up, rotationAmount, Space.Self);

            float currentAngle = Quaternion.Angle(_initialRotation, _turretRoot.localRotation);
            if (currentAngle > _maxAngle)
                _turretRoot.Rotate(Vector3.up, -rotationAmount, Space.Self);
        }

        public void HardReset()
        {
            _turretRoot.rotation = _initialRotation;
        }
    }
}