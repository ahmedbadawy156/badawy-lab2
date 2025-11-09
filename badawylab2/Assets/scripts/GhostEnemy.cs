using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : EnemyController
{
    private float flickerTime = 0f;
    private float flickerDuration = 2f;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = true;
        SpriteFlicker();
    }

    void SpriteFlicker()
    {
        if (flickerTime < flickerDuration)
        {
            flickerTime = flickerTime + Time.deltaTime;
        }
        else if (flickerTime >= flickerDuration)
        {
            sr.enabled = !(sr.enabled);
            flickerTime = 0;
        }
    }

    void FixedUpdate()
    {
        // if (this.isFacingRight == true)
        if (sr.flipX == true)
        {
            this.GetComponent<Rigidbody2D>().velocity =
                new Vector2(-maxSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity =
                new Vector2(maxSpeed, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    // Because ghosts can pass through walls, the Trigger function is overridden from the Superclass
    // to remove the flip function upon hitting a wall
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
            Flip();
        }
    }
}
