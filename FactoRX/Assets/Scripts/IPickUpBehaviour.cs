using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUpBehaviour
{
    bool CanBePickedUp();

    void OnPickUp();

    void OnDrop();
}
