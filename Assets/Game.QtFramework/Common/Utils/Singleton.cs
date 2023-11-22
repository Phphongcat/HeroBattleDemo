using Cysharp.Threading.Tasks;
using UnityEngine;

public class Singleton<T> : MonoBehaviour, ISetupFlag where T : Component
{
    [Header("Singleton Config")]
    [SerializeField] private bool useDontDestroyOnLoad;
    
    private static T s_instance;

    public static T Instance
    {
        get
        {
            if (HasInstance() is false)
            {
                // var singletonObject = new GameObject();
                // s_instance = singletonObject.AddComponent<T>();
                // singletonObject.name = nameof(T);
                return null;
            }
            return s_instance;
        }
        private set => s_instance = value;
    }
    
    protected static bool HasInstance()
    {
        return s_instance != null;
    }
    
    public async UniTask<bool> Setup()
    {
        await UniTask.WaitUntil(HasInstance);
        return HasInstance();
    }

    void Awake ()
    {
        // check if there's another instance already exist in scene
        if (s_instance != null && s_instance.GetInstanceID() != GetInstanceID())
        {
            // Destroy this instances because already exist the singleton of EventsDispatcer
            Destroy(gameObject);
        }
        else
        {
            // set instance
            s_instance = this as T;
            
            if(useDontDestroyOnLoad)
                DontDestroyOnLoad(this);
        }
    }
    
    void OnDestroy ()
    {
        // reset this static var to null if it's the singleton instance
        if (Instance == this)
            Instance = null;
    }
}