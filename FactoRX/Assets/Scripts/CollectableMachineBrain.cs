using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MachineBrain/Collectable")]
public class CollectableMachineBrain : MachineBrain
{
    [SerializeField]
    private Collectable collectable;

    [SerializeField]
    private float radius;

    public override IEnumerator MachineCoroutine(Machine machine, ScriptableArenaProperties arenaProps)
    {
        machine.isActive = true;

        var renderer = machine.GetComponent<SpriteRenderer>();
        var colour = new Color(renderer.color.r, renderer.color.g, renderer.color.b);
        var scale = machine.Scale;

        float t = 0;
        while (t <= 0.1f)
        {
            if (machine.Enabled)
            {
                renderer.color = Color.Lerp(colour, Color.white, t / 0.1f);
                machine.transform.localScale = Vector3.Lerp(scale, scale * 1.2f, t / 0.1f);
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        Vector2 spawnPos = machine.transform.position + (Vector3)(radius * Random.Range(0.5f, 1.0f) * Random.insideUnitCircle.normalized);
        Collectable col = Instantiate(collectable, spawnPos, Quaternion.identity);

        t = 0;
        while (t <= 0.1f)
        {
            if (machine.Enabled)
            {
                renderer.color = Color.Lerp(renderer.color, colour, t / 0.1f);
                machine.transform.localScale = Vector3.Lerp(machine.transform.localScale, scale, t / 0.1f);
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        renderer.color = colour;
        machine.transform.localScale = scale;

        machine.isActive = false;
    }
}
