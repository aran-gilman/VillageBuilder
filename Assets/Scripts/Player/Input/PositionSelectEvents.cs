using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem.HID;

[CreateAssetMenu(menuName = "Input Events/Position Select")]
public class PositionSelectEvents : InputEvents
{
    [SerializeField]
    private Vector3GameEvent highlightPositionEvent;

    [SerializeField]
    private Vector3GameEvent selectPositionEvent;

    [SerializeField]
    private LayerMask checkedLayers;

    private InputAction clickAction;
    private InputAction pointAction;

    private InputActionMap playerActionMap;
    private Camera mainCamera;
    private InputSystemUIInputModule uiInputModule;

    private Vector3 mostRecentPoint = Vector3.zero;

    public override void Disable()
    {
        clickAction.performed -= OnPlayerClick;
        pointAction.performed -= OnPlayerPoint;
    }

    public override void Enable()
    {
        mainCamera = Camera.main;
        playerActionMap.Enable();
        clickAction.performed += OnPlayerClick;
        pointAction.performed += OnPlayerPoint;
    }

    private void OnPlayerPoint(InputAction.CallbackContext context)
    {
        if (IsOverUI())
        {
            return;
        }
        Vector2 screenPos = context.ReadValue<Vector2>();
        if (Physics.Raycast(mainCamera.ScreenPointToRay(screenPos), out RaycastHit hit, Mathf.Infinity, checkedLayers, QueryTriggerInteraction.Collide))
        {
            mostRecentPoint = hit.point;
            highlightPositionEvent.Raise(mostRecentPoint);
        }
    }

    private void OnPlayerClick(InputAction.CallbackContext context)
    {
        if (IsOverUI())
        {
            return;
        }
        selectPositionEvent.Raise(mostRecentPoint);
    }

    private bool IsOverUI()
    {
        // We can't fetch this in Enable() because it may not be fully initialized yet
        if (uiInputModule == null)
        {
            EventSystem eventSystem = EventSystem.current;
            uiInputModule = (InputSystemUIInputModule)eventSystem.currentInputModule;
        }
        RaycastResult result = uiInputModule.GetLastRaycastResult(Mouse.current.deviceId);
        return result.gameObject != null;
    }

    private void OnEnable()
    {
        playerActionMap = asset.FindActionMap("Player");
        clickAction = playerActionMap.FindAction("Click");
        pointAction = playerActionMap.FindAction("Point");
    }
}
