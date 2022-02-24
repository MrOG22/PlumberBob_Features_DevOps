using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Flags")]
    [SerializeField] private bool featureFlag = true;
    [SerializeField] private bool directionGoingRight;
    private Vector2 movement;
    [SerializeField] private float speed = 400;
    private Rigidbody2D rigidbody;
    private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    private BoxCollider2D myBoxCollider;
    [SerializeField] private LayerMask playerLayer;


    private void Awake()
    {
        if (!featureFlag)
            return;

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();

    }
    // Update is called once per frame
    void Update()
    {
        if (!featureFlag)
            return;

        Movement();
        DamagePlayer();
    }

    void Movement()
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
        float distance = (myBoxCollider.size.x/2f)*1.4f;

        Debug.DrawRay(position, new Vector3((float)(directionGoingRight ? distance: -distance),0,0), Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    void DamagePlayer()
    {
        Vector2 position = transform.position;
        Vector2 direction = directionGoingRight == true ? Vector2.right : Vector2.left;
        float distance = (myBoxCollider.size.x / 2f) * 1.4f;

        Debug.DrawRay(position, new Vector3( distance , 0, 0), Color.green);
        Debug.DrawRay(position, new Vector3( -distance , 0, 0), Color.green);
        RaycastHit2D hitRight = Physics2D.Raycast(position, Vector2.right, distance, playerLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(position, Vector2.left, distance, playerLayer);

        if (hitRight.collider != null && hitRight.collider.tag == "Player")
        {
            Player player = hitRight.collider.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(1);
            }
        }       
        if (hitLeft.collider != null && hitLeft.collider.tag == "Player")
        {
            Player player = hitLeft.collider.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(1);
            }
        }

    }

    public void Death()
    {
        Destroy(this.gameObject);

    }

    void AnimationHandler()
    {

    }
}
