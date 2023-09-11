using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss : MonoBehaviour
{
    [SerializeField]
    private float growthRate;

    [SerializeField]
    private float size;


    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector2.one * size;
    }

    // Update is called once per frame
    void Update()
    {
        size += growthRate * Time.deltaTime;
        transform.localScale = Vector2.one * size;
    }    

    public void Shrink()
    {
        size -= 0.1f;
        transform.localScale = Vector2.one * size;
    }
}
