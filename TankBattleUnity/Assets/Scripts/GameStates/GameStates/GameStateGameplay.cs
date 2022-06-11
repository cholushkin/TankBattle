using Battle.GUI;
using RandomUtils;
using UnityEngine.UI;

public class GameStateGameplay : IAppState
{
    public Messenger Messenger;
    public ProgressBar LivesProgressBar;
    public ProgressBar EnergyProgressBar;
    public Text TopScore;
    public Text CurrentScore;
    public WeaponCardSwitcher WeaponCardSwitcher;

    public override void StateEnter(bool animated)
    {
        gameObject.SetActive(true);
        Messenger.ShowMessage(new RandomHelper(System.Environment.TickCount).FromArray(Messages.StartMessage), "May the force be with you");
    }

    public override void StateLeave(bool animated)
    {
        gameObject.SetActive(false);
    }
}
