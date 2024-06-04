using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseGrab : MonoBehaviour
{
    ActionMap actions;
    IAttachable attachable;

    bool isRemoved;

    public LayerMask mask;

    Transform lastHit;

    public Material clickedTower;

    private void Awake()
    {
        if (actions == null)
        {
            actions = new ActionMap();
        }
        actions.Enable();
        lastHit = transform;
    }
    private void OnEnable()
    {

        actions.Mouse.Left.performed += Grab;
    }

    private void Grab(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        //mousePos.y = Camera.main.nearClipPlane;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        Debug.DrawRay(mouseRay.origin, mouseRay.direction * 100, Color.red, 2);
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, ~mask))
        {
            if (isRemoved && hit.transform != lastHit)
            {
                
                attachable.Remove(hit.point);
                attachable = null;
                isRemoved = false;

            }

            else if (hit.transform.TryGetComponent(out TowerPlacer towerPlacer))
            {
                if (attachable != null)
                {
                   
                    attachable.Attach(hit.transform, towerPlacer);
                    attachable = null;
                }
            }

            else if (hit.transform.TryGetComponent<IAttachable>(out var x))
            {
                if (x.Placer != null && attachable != null && lastHit != hit.transform)
                {
                    attachable.Attach(hit.transform, x.Placer);
                    attachable = null;
                }
                else if (x.Placer != null)
                {


                    attachable = x;
                    isRemoved = true;

                }

                else
                {
                    attachable = x;
                }
                //hit.transform.TryGetComponent(out attachable);
            }
            lastHit = hit.transform;
        }
    }

    private void OnDisable()
    {
        actions.Mouse.Left.performed -= Grab;
    }

}
