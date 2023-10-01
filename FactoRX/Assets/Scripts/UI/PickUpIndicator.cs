using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpIndicator : MonoBehaviourExtended
{
    [SerializeField]
    private GameObject icon;    

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;

        icon.transform.position = transform.position + Vector3.up * (Mathf.PingPong(Time.time * 0.25f, 0.06f) - 0.03f);
        float rotationAmount = Mathf.PingPong(Time.time * 25f, 8f) - 4f;
        icon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAmount));
    }

    public void SetPosition(PickUp pickUp)
    {
        transform.position = pickUp.transform.position + Vector3.up * 0.75f;
    }
}
