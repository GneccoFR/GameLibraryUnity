using System;
using UnityEngine;


public class SideScrollingEnemy : Enemy
{
    //here executes it's collision inherited events
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        ProjectileCollision(collision);
        PlayerCollision(collision);
        WallCollision(collision);
    }

    //here executes it's physical component movements
    void FixedUpdate()
    {
        PhysicalMove();
    }

    //here is described it's movement pattern. In this case, a back and forward walk with wall collides
    public override void MovementPattern(bool mybonk)
    {

        if (mybonk == true)
        {
            Debug.Log("BONK!");
            timer = 0;
            mybonk = false;
        }

        if (direction == Vector2.right && timer <= 0)
        {
            direction = Vector2.left;
            timer = changeTime;
        }

        if (direction == Vector2.left && timer <= 0)
        {
            direction = Vector2.right;
            timer = changeTime;
        }

        timer -= Time.deltaTime;
        Debug.Log(timer);
    }
}
