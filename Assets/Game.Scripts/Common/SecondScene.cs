using QtNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondScene : MonoBehaviour
{
    [SerializeField] private QtEventListener listener;
    [SerializeField] private Installer installer;
    
    
    private async void Awake()
    {
        await installer.Install();
        
        listener.StartListening(GameEventID.SelectCharacter, DoneSelectCharacter);
        listener.StartListening(GameEventID.SelectSkin, DoneSelectSkin);

        await QtUIContainer.Instance.LoadContainer(UIName.Wallpaper);
        await QtUIContainer.Instance.LoadContainer(UIName.SelectCharacterBox);
        await QtUIContainer.Instance.LoadContainer(UIName.SelectSkinBox, isOpen: false);
    }

    private async void DoneSelectCharacter()
    {
        var statBox = await QtUIContainer.Instance.LoadContainer(UIName.SelectCharacterBox);
        var skinBox = await QtUIContainer.Instance.LoadContainer(UIName.SelectSkinBox);
        statBox.Close();
        skinBox.Open();
    }

    private void DoneSelectSkin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}