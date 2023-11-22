using Cysharp.Threading.Tasks;

namespace QtNameSpace
{
    public interface IInstaller
    {
        public UniTask<bool> Install();
    }
}