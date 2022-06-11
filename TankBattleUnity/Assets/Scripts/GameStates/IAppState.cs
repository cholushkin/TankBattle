using UnityEngine;

public abstract class IAppState : MonoBehaviour
{
    public abstract void StateEnter(bool animated);
    public abstract void StateLeave(bool animated);
}
