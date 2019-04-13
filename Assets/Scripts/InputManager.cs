using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private static InputManager m_instance_;

    public static InputManager GetInstance()
    {
        return m_instance_ ?? (m_instance_ = new InputManager());
    }

    #region Mouse
    // Mouse Left
    public delegate void LeftMouseButtonEventHandler();
    public event LeftMouseButtonEventHandler OnClickLeftMouseButton;

    // Mouse Right
    public delegate void RightMouseButtonEventHandler();
    public event RightMouseButtonEventHandler OnClickRightMouseButton;

    // MousePostion
    public delegate void MouseEventHandler(float _fxValue, float _fyValue, float _fWheelValue);
    public event MouseEventHandler OnMouse;


    #endregion

    #region Keyboard
    // move forward pressed
    public delegate void MoveForwardKeyboardButtonPressedEventHandler();
    public event MoveForwardKeyboardButtonPressedEventHandler OnKeyboardMoveForwardButtonPressed;

    // move forward Released 
    public delegate void MoveForwardKeyboardButtonReleasedEventHandler();
    public event MoveForwardKeyboardButtonReleasedEventHandler OnKeyboardMoveForwardButtonReleased;

    // move backward pressed
    public delegate void MoveBackwardKeyboardButtonPressedEventHandler();
    public event MoveBackwardKeyboardButtonPressedEventHandler OnKeyboardMoveBackwardButtonPressed;

    // move backward Released
    public delegate void MoveBackwardKeyboardButtonReleasedEventHandler();
    public event MoveBackwardKeyboardButtonReleasedEventHandler OnKeyboardMoveBackwardButtonReleased
    #endregion


    public void Update()
    {
        TriggerMouseEventHandler();
        TriggerLeftMouseButtonEventHandler();
        TriggerRightMouseButtonEventHandler();
        TriggerKeyboardMoveForwardButtonPressed();
        TriggerKeyboardMoveForwardButtonReleased();
        TriggerKeyboardMoveBackwardButtonPressed();
        TriggerKeyboardMoveBackwardButtonReleased();
    }

    #region MouseTrigger
    public void TriggerMouseEventHandler()
    {
        if (OnMouse != null)
            OnMouse(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));
    }

    public void TriggerLeftMouseButtonEventHandler()
    {
        if (OnClickLeftMouseButton != null && Input.GetKeyDown(KeyCode.Mouse0))
            OnClickLeftMouseButton();
    }

    public void TriggerRightMouseButtonEventHandler()
    {
        if (OnClickRightMouseButton != null && Input.GetKeyDown(KeyCode.Mouse1))
            OnClickRightMouseButton();
    }
    #endregion


    #region KeyboardTrigger
    public void TriggerKeyboardMoveForwardButtonPressed()
    {
        if (OnKeyboardMoveForwardButtonPressed != null && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W)))
            OnKeyboardMoveForwardButtonPressed();
    }

    public void TriggerKeyboardMoveForwardButtonReleased()
    {
        if (OnKeyboardMoveForwardButtonReleased != null && (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W)))
            OnKeyboardMoveForwardButtonReleased();
    }

    public void TriggerKeyboardMoveBackwardButtonPressed()
    {
        if (OnKeyboardMoveBackwardButtonPressed != null && Input.GetKeyDown(KeyCode.S))
            OnKeyboardMoveBackwardButtonPressed();
    }

    public void TriggerKeyboardMoveBackwardButtonReleased()
    {
        if (OnKeyboardMoveBackwardButtonReleased != null && Input.GetKeyUp(KeyCode.S))
            OnKeyboardMoveBackwardButtonReleased();
    }
    #endregion

}
