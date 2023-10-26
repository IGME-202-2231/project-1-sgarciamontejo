using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputController : MonoBehaviour
{
    [SerializeField]
    MovementController m_MovementController;
    [SerializeField]
    GameObject missileManager;
    [SerializeField]
    GameObject missile;

    float cooldown = .25f;
    float cooldownTimestamp;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_MovementController.direction = context.ReadValue<Vector2>();
        //Debug.Log(context.ToString());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //shooting
        if (context.performed)
        {
            Debug.Log("performed");
            if (!(Time.time < cooldownTimestamp))
            {
                cooldownTimestamp = Time.time + cooldown; //firerate - cooldown
                Instantiate(missile, transform.position, Quaternion.identity, missileManager.transform);
            }
        }
    }
}