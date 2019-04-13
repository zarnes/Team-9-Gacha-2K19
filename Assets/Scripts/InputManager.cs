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
    public event MoveBackwardKeyboardButtonReleasedEventHandler OnKeyboardMoveBackwardButtonReleased;

    public void Update()
    {
        TriggerKeyboardMoveForwardButtonPressed();
        TriggerKeyboardMoveForwardButtonReleased();
    }


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

}
