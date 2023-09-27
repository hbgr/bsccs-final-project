using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssalOrb : MonoBehaviourExtended
{
    [SerializeField]
    private float lifetime;

    [SerializeField]
    private float speed;

    private Vector3 direction;

    private void Start()
    {
        StartCoroutine(PulseCoroutine());
    }

    private void FixedUpdate()
    {
        if (!Enabled) return;

        transform.position += speed * Time.fixedDeltaTime * direction;
        lifetime -= Time.fixedDeltaTime;

        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private IEnumerator PulseCoroutine()
    {
        var scale = transform.localScale;

        float t = 0;
        while (t <= 0.33f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.localScale = Vector3.Lerp(scale, scale * 1.2f, t / 0.5f);
            }

            yield return new WaitForFixedUpdate();
        }

        t = 0;
        while (t <= 0.33f)
        {
            if (Enabled)
            {
                t += Time.fixedDeltaTime;
                transform.localScale = Vector3.Lerp(scale * 1.2f, scale, t / 0.5f);
            }

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = scale;

        StartCoroutine(PulseCoroutine());
        yield return null;
    }
}
