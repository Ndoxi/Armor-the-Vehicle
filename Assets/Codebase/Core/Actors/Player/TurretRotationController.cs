namespace Codebase.Core.Actors
{
    public class TurretRotationController : ITurretRotationController
    {
        private readonly ISwipeInput _swipeInput;

        public TurretRotationController(ISwipeInput swipeInput)
        {
            _swipeInput = swipeInput;
        }

        public float GetDirection()
        {
            return _swipeInput.GetHorizontal() * -1f; //inverse direction
        }
    }
}