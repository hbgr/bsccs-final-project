using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PickUp : MonoBehaviour
{
    public bool CanBePickedUp()
    {
        var pickUpBehavious = GetComponents<IPickUpBehaviour>();
        foreach (var pickUpBehaviour in pickUpBehavious)
        {
            if (!pickUpBehaviour.CanBePickedUp())
            {
                return false;
            }
        }
        return true;
    }

    public void OnPickUp()
    {
        var pickUpBehavious = GetComponents<IPickUpBehaviour>();
        foreach (var pickUpBehaviour in pickUpBehavious)
        {
            pickUpBehaviour.OnPickUp();
        }
    }

    public void OnDrop()
    {
        var pickUpBehavious = GetComponents<IPickUpBehaviour>();
        foreach (var pickUpBehaviour in pickUpBehavious)
        {
            pickUpBehaviour.OnDrop();
        }
    }
}
