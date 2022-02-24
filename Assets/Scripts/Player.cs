using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Flags")]
	[SerializeField] private bool featureFlag = true;

	[Header("Stats")]
	[SerializeField] private float moveSpeed = 2500f;
	[SerializeField] private float jumpPower = 1500f;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask qblockLayer;
	[SerializeField] private LayerMask enemyLayer;

	[Header("Health")]
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private int currentHealth;
	[SerializeField] private bool isAlive;

	[Header("Inputs")]
	[SerializeField] private KeyCode moveLeftInput = KeyCode.LeftArrow;
	[SerializeField] private KeyCode moveRigthInput = KeyCode.RightArrow;
	[SerializeField] private KeyCode jumpInput = KeyCode.Space;

	private Rigidbody2D rigidbody2D;
	private Animator animator = null;
	private Vector2 movementVector = Vector2.zero;
	private SpriteRenderer spriteRenderer;

	private BoxCollider2D myBoxCillider;

	private void Awake()
	{
		if (!featureFlag)
			return;

		rigidbody2D = GetComponent<Rigidbody2D>();
		myBoxCillider = GetComponent<BoxCollider2D>();
		animator = GetComponentInChildren<Animator>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		currentHealth = maxHealth;
		isAlive = true;
	}

	void Update()
	{
		if (!featureFlag)
			return;

		InputHanlder();
		Movement();
		AnimationHanlder();
		JumpOnEnemy();
		JumpInToQBlock();
	}

	private void InputHanlder()
	{
		movementVector = Vector2.zero;

		if (Input.GetKey(moveLeftInput))
		{
			movementVector += new Vector2(-1, 0);
		}

		if (Input.GetKey(moveRigthInput))
		{
			movementVector += new Vector2(1, 0);
		}

		if (GroundCheck() && Input.GetKeyDown(jumpInput))
		{
			Jump();
		}
	}

	private bool GroundCheck()
	{
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;
		float distance = (myBoxCillider.size.y / 2f) * 1.1f;

		Debug.DrawRay(position, new Vector3(0,-distance,0), Color.green);
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
		if (hit.collider != null)
		{
			return true;
		}

		return false;
	}

	private void Movement()
	{
		float movementDir = movementVector.x * moveSpeed * Time.deltaTime;
		rigidbody2D.velocity = new Vector2(movementDir, rigidbody2D.velocity.y);
	}

	private void Jump()
	{
		rigidbody2D.AddForce(new Vector2(0, jumpPower));
	}

	private void AnimationHanlder()
	{
		animator.SetBool("OnGround", GroundCheck());
		animator.SetFloat("MoveSpeed", Mathf.Abs(movementVector.x));

		if (movementVector.x != 0)
		{
			spriteRenderer.flipX = movementVector.x == 1 ? false : true;
		}
	}

	private void JumpOnEnemy()
	{
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;
		float distance = (myBoxCillider.size.y / 2f) * 1.1f;

		Debug.DrawRay(position, new Vector3(0, -distance, 0), Color.green);
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, enemyLayer);
		if (hit.collider != null && hit.collider.tag == "Enemy")
		{
			hit.collider.gameObject.GetComponent<EnemyAI>().Death();
			Jump();
		}
	}

	private void JumpInToQBlock()
	{
		Vector2 position = transform.position;
		Vector2 direction = Vector2.up;
		float distance = (myBoxCillider.size.y / 2f) * 1.1f;

		Debug.DrawRay(position, new Vector3(0, distance, 0), Color.green);
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, qblockLayer);
		if (hit.collider != null && hit.collider.tag == "QBlock")
		{
			hit.collider.gameObject.GetComponent<QBlock>().Die();
		}
	}

	public void TakeDamage(int damageAmount)
    {
		currentHealth -= damageAmount;
		if(currentHealth <= 0)
        {
			Die();
        }
    }

	public void SetPowerUp()
    {
		// TODO: set powerup

		currentHealth += 1;

		if(currentHealth > maxHealth)
        {
			currentHealth = maxHealth;
        }
    }

	private void Die()
    {
		isAlive = false;
		// TODO: reset level?
	}
}
