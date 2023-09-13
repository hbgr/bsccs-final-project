using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private TransformVariable targetTransform;

    [SerializeField]
    private float followSpeed;

    [SerializeField]
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = targetTransform.value.position + offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform.value.position + offset, followSpeed * Time.fixedDeltaTime);
    }
}
