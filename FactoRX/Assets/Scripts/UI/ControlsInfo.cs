using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsInfo : MonoBehaviour
{
    [SerializeField]
    private ScriptableInputEvents inputEvents;

    // Start is called before the first frame update
    void Start()
    {
        inputEvents.OnMoveEvent += OnMove;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        inputEvents.OnMoveEvent -= OnMove;
    }

    private void OnMove(object sender, InputAction.CallbackContext e)
    {
        gameObject.SetActive(false);
    }
}
