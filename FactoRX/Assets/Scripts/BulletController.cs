using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float bulletLifetime;

    private float moveSpeed;

    private Vector2 moveDirection;

    private Vector2 target;

    private bool Enabled => GameStateManager.IsState(GameState.Game);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;

        transform.position += (Vector3)(moveSpeed * Time.deltaTime * moveDirection);
        if (Vector2.Distance(transform.position, target) < 0.25f)
        {
            Destroy(gameObject);
        }
    }

    public void SetProperties(float speed, Vector2 targetPos)
    {
        moveSpeed = speed;
        moveDirection = ((Vector3)targetPos - transform.position).normalized;
        target = targetPos;
    }
}
