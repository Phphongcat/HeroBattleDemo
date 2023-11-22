using QtNameSpace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class EndGamePopup : QtPopup
    {
        [SerializeField] private Button acceptButton;


        private void Awake()
        {
            acceptButton.onClick.AddListener(OnAccept);
        }

        private void OnAccept()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}