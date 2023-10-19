using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    MovementController m_MovementController;
    [SerializeField]
    GameObject missile;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_MovementController.direction = context.ReadValue<Vector2>();
        Debug.Log(context.ToString());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Instantiate(missile, transform.position, Quaternion.identity);
        }
    }
}