using UnityEngine;

namespace Codebase.Core.Actors
{
    public class ActorAnimator : MonoBehaviour
    {
        private static readonly int SpeedHash = Animator.StringToHash("Speed");

        [SerializeField] private Animator _animator;

        public void SetSpeed(float value)
        {
            _animator.SetFloat(SpeedHash, value);
        }
    }
}