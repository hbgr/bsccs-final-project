using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviourExtended, IPickUpBehaviour
{
    [SerializeField]
    private float cooldown;

    [SerializeField]
    private PowerOrbController powerOrbPrefab;    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PowerCoroutine(cooldown));
    }

    void OnEnable()
    {
        //StartCoroutine(PowerCoroutine(cooldown));
    }

    private IEnumerator PowerCoroutine(float cooldown)
    {
        float t = 0;
        while (t <= 0.25f * cooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new 
            WaitForFixedUpdate();
        }
        // Shoot power orb
        var powerOrb = Instantiate(powerOrbPrefab, transform.position, Quaternion.identity);
        powerOrb.SetProperties(transform.rotation * Vector2.up, gameObject);
        
        t = 0;
        while (t <= 0.75f * cooldown)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
            }

            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(PowerCoroutine(cooldown));
    }

    public bool CanBePickedUp()
    {
        return true;
    }

    public void OnPickUp()
    {
        StopAllCoroutines();
    }

    public void OnDrop()
    {
        StartCoroutine(PowerCoroutine(cooldown));
    }
}
