using Cysharp.Threading.Tasks;
using System.Threading;

namespace Codebase.Core.Actors
{
    public interface IVisualEffect
    {
        UniTask Play(CancellationToken cancellationToken);
    }
}