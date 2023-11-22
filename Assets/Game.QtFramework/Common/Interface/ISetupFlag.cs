using Cysharp.Threading.Tasks;

public interface ISetupFlag
{
    public UniTask<bool> Setup();
}