using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Codebase.Core.Actors
{
    public class ActorsSystem
    {
        public PlayerActor PlayerActor => _playerActor;

        private readonly ActorsFactory _factory;
        private readonly Transform _playerSpawnPoint;
        private readonly List<Actor> _actors;
        private PlayerActor _playerActor;

        public ActorsSystem(ActorsFactory factory, Transform playerSpawnPoint)
        {
            _factory = factory;
            _playerSpawnPoint = playerSpawnPoint;
            _actors = new List<Actor>(100);
        }

        public void CreatePlayer()
        {
            var playerActor = _factory.Create<PlayerActor>();
            playerActor.transform.position = _playerSpawnPoint.position;

            _actors.Add(playerActor);
            _playerActor = playerActor;
        }

        public void CreateEnemy()
        {
            _actors.Add(_factory.Create<Actor>());
        }
    }
}