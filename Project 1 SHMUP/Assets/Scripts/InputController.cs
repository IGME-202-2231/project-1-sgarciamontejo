using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    MovementController m_MovementController;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_MovementController.direction = context.ReadValue<Vector2>();
        Debug.Log(context.ToString());
    }
}