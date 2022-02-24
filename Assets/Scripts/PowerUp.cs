using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool directionGoingRight;

    private BoxCollider2D myBoxCollider;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (WallCheck())
        {
            directionGoingRight = !directionGoingRight;
        }

        float direction = directionGoingRight == true ? 1 : -1;
        rigidbody.velocity = new Vector2(direction * speed * Time.deltaTime, rigidbody.velocity.y);
    }

    bool WallCheck()
    {
        Vector2 position = transform.position;
        Vector2 direction = directionGoingRight == true ? Vector2.right : Vector2.left;
        float distance = (myBoxCollider.size.x / 2f) * 1.4f;

        Debug.DrawRay(position, new Vector3((float)(directionGoingRight ? distance : -distance), 0, 0), Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null && hit.collider.gameObject != this.gameObject)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Player>().SetPowerUp();
            Destroy(this.gameObject);
        }
    }
}
