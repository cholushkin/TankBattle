using Battle;
using Battle.GUI;
using UnityEngine;
using RandomUtils;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateResult : IAppState
{
    public Text TextCongratMessage;
    public Text TextYourScore;
    public Text TextTopScore;
    public GameObject NewHighscore;


    public override void StateEnter(bool animated)
    {
        TextCongratMessage.text = new RandomHelper(System.Environment.TickCount).FromArray(Messages.AfterGameMessage);
        TextTopScore.text = "Top score: " + SimpleUserAccount.UserData.TopScore;
        TextYourScore.text = "Your score: " + SimpleUserAccount.UserData.LastScore;
        NewHighscore.SetActive(GameProcessor.Instance.IsNewTopScore);
        gameObject.SetActive(true);
    }

    public override void StateLeave(bool animated)
    {
    }

    public void OnFightButtonTap()
    {
        SceneManager.LoadScene(1);
    }
}
