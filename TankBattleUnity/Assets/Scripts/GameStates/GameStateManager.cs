using Alg;
using System;
using System.Linq;
using UnityEngine.Assertions;

public class GameStateManager : Singleton<GameStateManager>
{

    public IAppState StartMode;
    public IAppState[] Modes;

    private IAppState _currentMode;

    void Start ()
    {
        Start(StartMode.GetType(), false);
    }

    public void Start(Type mode, bool animated)
    {
        if (null != _currentMode && _currentMode.GetType() == mode)
            return;

        var next = Modes.FirstOrDefault(s => s.GetType() == mode);
        Assert.IsNotNull(next);

        // hope StateLeave won't call Start
        if (null != _currentMode)
            _currentMode.StateLeave(animated);

        _currentMode = next;
        _currentMode.StateEnter(animated);
    }
}
