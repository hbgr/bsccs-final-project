using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MachineBrain/Gem")]
public class GemMachineBrain : MachineBrain
{
    [SerializeField]
    private GemController gemPrefab;

    public override IEnumerator MachineCoroutine(Machine machine, ScriptableArenaProperties arenaProps)
    {
        machine.isActive = true;

        var renderer = machine.GetComponent<SpriteRenderer>();
        var colour = new Color(renderer.color.r, renderer.color.g, renderer.color.b);
        var scale = machine.transform.localScale;

        float t = 0;
        while (t <= 0.5f)
        {
            if (machine.Enabled)
            {
                renderer.color = Color.Lerp(colour, Color.cyan, t / 0.5f);
                machine.transform.localScale = Vector3.Lerp(scale, scale * 1.2f, t / 0.5f);
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        for (int i = 0; i < 3; i++)
        {
            // Vector2 spawnPos = 0.8f * arenaProps.currentRadius * Random.insideUnitCircle;

            Vector2 spawnPos = 2f * Random.insideUnitCircle;
            GemController gem = Instantiate(gemPrefab, spawnPos, Quaternion.identity);
            t = 0;
            while (t <= 0.25f)
            {
                if (machine.Enabled)
                {
                    t += Time.fixedDeltaTime;
                }
                yield return new WaitForFixedUpdate();
            }
        }

        // t = 0;
        // while (t <= 0.5f)
        // {
        //     if (Enabled)
        //     {
        //         t += Time.fixedDeltaTime;
        //     }
        //     yield return new WaitForFixedUpdate();
        // }

        t = 0;
        while (t <= 0.75f)
        {
            if (machine.Enabled)
            {
                renderer.color = Color.Lerp(renderer.color, colour, t / 0.5f);
                machine.transform.localScale = Vector3.Lerp(machine.transform.localScale, scale, t / 0.5f);
                t += Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }

        renderer.color = colour;
        machine.transform.localScale = scale;

        machine.isActive = false;
    }
}
