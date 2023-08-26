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

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DestroyBulletCoroutine(bulletLifetime));
    }

    // Update is called once per frame
    void Update()
    {
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

    // private IEnumerator DestroyBulletCoroutine(float bulletLifetime)
    // {
    //     yield return new WaitForSeconds(bulletLifetime);
    //     Destroy(gameObject);
    //     yield return null;
    // }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.GetComponent<PlayerController>())
    //     {
    //         Debug.Log("You died!");
    //     }
    // }
}
