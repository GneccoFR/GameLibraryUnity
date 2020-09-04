using System;
using UnityEngine;

public class Enemy : Character
{
    //list of it's inherent components
	protected QuestManager questManager;
    
    //this is specific of the implementation
    public AudioClip bonkClip;
    public AudioClip fixClip;
    public ParticleSystem smokeEffect;
    public ParticleSystem hitEffect;
    
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
        questManager = QuestManager.GetInstance();
        timer = changeTime;
    }

    protected void ProjectileCollision(Collision2D collision)
    {
        Projectile projectileCollided = collision.gameObject.GetComponent<Projectile>();
        if (projectileCollided != null)
        {
            audioSource.PlayOneShot(bonkClip);
            Fix();
        }
    }

    protected void PlayerCollision(Collision2D collision)
    {
        RubyController playerCollided = collision.gameObject.GetComponent<RubyController>();
        if (playerCollided != null)
        {
            audioSource.PlayOneShot(bonkClip);
            playerCollided.ChangeHealth(-enemyDamage);
        }
    }

    protected void WallCollision(Collision2D collision)
    {
        Projectile projectileCollided = collision.gameObject.GetComponent<Projectile>();
        RubyController playerCollided = collision.gameObject.GetComponent<RubyController>();
        if (playerCollided == null && projectileCollided == null)
            bonk = true;
    }


}
