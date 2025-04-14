using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();

            SceneManager.LoadScene("Main");

            DontDestroyOnLoad(this);
        }
    }
}