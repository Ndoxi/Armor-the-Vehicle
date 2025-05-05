using Cysharp.Threading.Tasks;

namespace Codebase.Core.Views
{
    public interface IAnimatedPanel
    {
        UniTask Show();
        UniTask Hide();
    }
}