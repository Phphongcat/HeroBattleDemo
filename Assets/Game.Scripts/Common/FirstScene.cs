using System;
using Cysharp.Threading.Tasks;
using QtNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    [SerializeField] private float setupTiming = 0.25f;
    [SerializeField] private Installer installer;
    
    
    private async void Awake()
    {
        await installer.Install();
        await UniTask.Delay(TimeSpan.FromSeconds(setupTiming));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}