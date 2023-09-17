using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "MachineBrain/Start")]
public class StartMachineBrain : MachineBrain
{
    public override IEnumerator MachineCoroutine(Machine machine)
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

        activationAudio.Play(machine.gameObject);

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

        SceneManager.LoadScene("Game");

        machine.isActive = false;
    }
}
