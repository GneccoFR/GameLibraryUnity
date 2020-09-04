using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //Essential components
	Rigidbody2D myRigidBody;
    AudioSource audioSource;
    Animator animator;

    //Movement properties
    Vector2 lookDirection = new Vector2(1, 0);
    float horizontal;
    float vertical;
    public float speed = 3f;

    //Health properties
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;
    bool alive;

    //Invincibility properties
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;


    //Builds it's main components
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        alive = true;
    }

    //Once per frame
    void Update()
    {
        //if the character is not alive halts the function
        if (!alive) return;

        //Executes Animation movement
        MoveAnimation();

        //Checks it's invincibility status
        InvincibleCheck();
    }

    //Once per physical tick
    void FixedUpdate()
    {
        if (!alive) return;
        //Execute it's physical movement
        PhysicalMove();
    }

    //Reduces it's invincible timer if active, or shuts it off if reached the fixed time
    public void InvincibleCheck()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    //Sets the animator float values with the asked direction
    public void MoveAnimation()
    {
        Vector2 move = new Vector2(horizontal, vertical);

        CheckDirection(move);

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }

    //checks the direction the character is facing
    public void CheckDirection(Vector2 direction)
    {
        if (!Mathf.Approximately(direction.x, 0.0f) || !Mathf.Approximately(direction.y, 0.0f))
        {
            lookDirection.Set(direction.x, direction.y);
            lookDirection.Normalize();
        }
    }

    //takes the given values for speed and direction and assigns it as movement to it's body
    public void PhysicalMove()
    {

        Vector2 position = rigidbody2D.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }

    public abstract void OnCollisionEvent(Collision2D collision);
}
