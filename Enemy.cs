using System;
using UnityEngine;

public class Enemy : Character
{
    //Damage value for contact attacks
    public int enemyDamage;
    
    //general AI properties to implement in further variety of enemies
    public float changeTime = 3.0f;
    protected float timer;
    protected bool bonk;


    protected void Start()
    {
        //Builds its essential components from the Character Class
        base.Start();

        //builds its own components
        timer = changeTime;
    }

    protected void PhysicalMove()
    {
        if (!alive)
            return;

        Vector2 position = rigidbody2D.position;

        MovementPattern(bonk);

        AnimationMove();

        position.x = position.x + enemySpeed * Time.deltaTime * direction.x;
        position.y = position.y + enemySpeed * Time.deltaTime * direction.y;
        rigidbody2D.MovePosition(position);
        bonk = false;
    }

    //List of the different collisions the enemy can have 
    protected void ProjectileCollision(Collision2D collision)
    {
        Projectile projectileCollided = collision.gameObject.GetComponent<Projectile>();
        if (projectileCollided != null)
        {
            //here you code what your enemy need to do when a projectile-like object hits it
        }
    }

    protected void PlayerCollision(Collision2D collision)
    {
        RubyController playerCollided = collision.gameObject.GetComponent<RubyController>();
        if (playerCollided != null)
        {
            //as default, the enemy deals an amount of damage to the player by direct contact. If needed, this can be not used
            playerCollided.ChangeHealth(-enemyDamage);

            //here you code what your enemy need to do when a player-like object hits it
        }
    }

    protected void WallCollision(Collision2D collision)
    {
        Projectile projectileCollided = collision.gameObject.GetComponent<Projectile>();
        RubyController playerCollided = collision.gameObject.GetComponent<RubyController>();
        if (playerCollided == null && projectileCollided == null)
            bonk = true;

        //here the enemy recognizes an obstacle(or better said, a non-projectile, non-player object) and states that it has collided for it's movement instructions 
        //If needed, more code can be written here
    }

    public abstract void MovementPattern(bool bonk);
}
