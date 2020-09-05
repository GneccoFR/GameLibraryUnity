using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //Essential components
    protected Rigidbody2D myRigidBody;
    protected AudioSource audioSource;
    protected Animator animator;

    //Movement properties
    protected Vector2 lookDirection = new Vector2(1, 0);
    protected float horizontal;
    protected float vertical;
    public float speed = 3f;

    //Health properties
    public int maxHealth = 5;
    protected int currentHealth;
    public int health { get { return currentHealth; } }
    protected bool alive;

    //Invincibility properties
    public float timeInvincible = 2.0f;
    protected bool isInvincible;
    protected float invincibleTimer;


    //Builds it's main components
    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        alive = true;
    }

    //Once per frame
    protected void Update()
    {
        //if the character is not alive halts the function
        if (!alive) return;

        //Executes Animation movement
        MoveAnimation();

        //Checks it's invincibility status
        InvincibleCheck();
    }

    //Once per physical tick
    protected void FixedUpdate()
    {
        if (!alive) return;
        //Execute it's physical movement
        PhysicalMove();
    }

    //Reduces it's invincible timer if active, or shuts it off if reached the fixed time
    protected public void InvincibleCheck()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    //Sets the animator float values with the asked direction
    protected void MoveAnimation()
    {
        Vector2 move = new Vector2(horizontal, vertical);

        CheckDirection(move);

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }

    //checks the direction the character is facing
    protected void CheckDirection(Vector2 direction)
    {
        if (!Mathf.Approximately(direction.x, 0.0f) || !Mathf.Approximately(direction.y, 0.0f))
        {
            lookDirection.Set(direction.x, direction.y);
            lookDirection.Normalize();
        }
    }

    //takes the given values for speed and direction and assigns it as movement to it's body
    protected abstract void PhysicalMove();

    public abstract void OnCollisionEvent(Collision2D collision);
}
