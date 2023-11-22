using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace QtNameSpace
{
    public class Installer : MonoBehaviour, IInstaller
    {
        public async UniTask<bool> Install()
        {
            var setupFlags = FindObjectsOfType<MonoBehaviour>().OfType<ISetupFlag>().ToList();
            await UniTask.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            foreach (var setupFlag in setupFlags)
            {
                if(await setupFlag.Setup())
                    continue;
            
                DebugEditor.LogWarning("Some setupFlag failed");
            }

            return true;
        }
    }
}