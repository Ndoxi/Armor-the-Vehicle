namespace Codebase.Core.Actors
{
    public class BulletAttack : IAttack
    {
        private readonly int _damage;

        public BulletAttack(int damage)
        {
            _damage = damage;
        }

        public void Perform(Actor target)
        {
            target.Health.Decrease(_damage);
        }
    }
}