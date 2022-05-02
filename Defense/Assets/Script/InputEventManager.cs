using UnityEngine;

public class InputEventManager : MonoSingleton<InputEventManager>
{
    public delegate void TouchEvent(Touch touchInfo);
    private event TouchEvent touchEvent;

    public bool IsTouchEnable
    {
        get;set;
    }

    public bool AddTouchEvent(TouchEvent touchFunc)
    {
        foreach(var delegateFunc in touchEvent.GetInvocationList())
            if (delegateFunc.Equals(touchFunc)) return false;

        touchEvent += touchFunc;
        return true;
    }

    public void RemoveTouchEvent(TouchEvent touchFunc)
    {
        touchEvent -= touchFunc;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
            touchEvent(Input.GetTouch(0));
    }
}

