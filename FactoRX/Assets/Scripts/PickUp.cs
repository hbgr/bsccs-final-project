using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
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

        var renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.33f);
    }

    public void OnDrop()
    {
        var pickUpBehavious = GetComponents<IPickUpBehaviour>();
        foreach (var pickUpBehaviour in pickUpBehavious)
        {
            pickUpBehaviour.OnDrop();
        }

        var renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
    }
}
