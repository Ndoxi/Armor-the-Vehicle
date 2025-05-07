using Zenject;

namespace Codebase.Core.Actors
{
    public class StickmanAttack : IAttack
    {
        private readonly LazyInject<StickmanActor> _parentRef;
        private readonly int _damage;

        public StickmanAttack(LazyInject<StickmanActor> parentRef, int damage)
        {
            _parentRef = parentRef;
            _damage = damage;
        }

        public void Perform(Actor target)
        {
            var parent = _parentRef.Value;
            parent.Health.Decrease(parent.Health.Current);
            target.Health.Decrease(_damage);
        }
    }
}