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
        private PlayerActor _playerActor;

        public ActorsSystem(ActorsFactory factory, 
                            Transform playerSpawnPoint)
        {
            _factory = factory;
            _playerSpawnPoint = playerSpawnPoint;
        }

        public void CreatePlayer()
        {
            var playerActor = _factory.Create<PlayerActor>();
            playerActor.transform.position = _playerSpawnPoint.position;
            _playerActor = playerActor;
        }
    }
}