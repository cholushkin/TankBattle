using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Battle.GUI
{
    public class MainMenu : MonoBehaviour
    {
        private const string TopScoreFormatString = "Top Score: {0}";
        private const string YourScoreFormatString = "Your Score: {0}";
        public Text TopScoreText;
        public Text LastScoreText;


        private void Start()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            SimpleUserAccount.Load();
            TopScoreText.text = string.Format(TopScoreFormatString, SimpleUserAccount.UserData.TopScore);
            LastScoreText.text = string.Format(YourScoreFormatString, SimpleUserAccount.UserData.LastScore);
        }

        public void OnStartTap()
        {
            SceneManager.LoadScene(1);
        }
    }
}