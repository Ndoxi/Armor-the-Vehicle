using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Codebase.Core.Actors
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ActorParticleVFX : MonoBehaviour, IVisualEffect
    {
        private ParticleSystem _particleSystem;
        private UniTaskCompletionSource _taskCompletionSource;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnParticleSystemStopped()
        {
            _taskCompletionSource?.TrySetResult();
        }

        public UniTask Play(CancellationToken cancellationToken)
        {
            _taskCompletionSource = new UniTaskCompletionSource();
            cancellationToken.Register(() => Cancell(cancellationToken));

            _particleSystem.Play();

            return _taskCompletionSource.Task;
        }

        private void Cancell(CancellationToken cancellationToken)
        {
            if (_particleSystem != null)
                _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            _taskCompletionSource.TrySetCanceled(cancellationToken);
        }
    }
}