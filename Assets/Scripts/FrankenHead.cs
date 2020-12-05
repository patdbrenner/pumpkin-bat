using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FrankenHead : MonoBehaviour
{
    Rigidbody2D rigidFranken;

    [SerializeField]
    Transform player;

    public float aggroRange;
    public float moveSpeed;
    public float jumpDistance;

    private void Start()
    {
        rigidFranken = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            rigidFranken.velocity = new Vector2(rigidFranken.velocity.x, 0);
            rigidFranken.AddForce(new Vector2(rigidFranken.velocity.x, jumpDistance), ForceMode2D.Impulse);
        }

        /*if (other.tag == "Player")    Commented because Player script should control player death.
        {
            Destroy(other.gameObject);
        }*/
    }

    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer <= aggroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x - .5)
        {
            rigidFranken.velocity = new Vector2(moveSpeed, rigidFranken.velocity.y);
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else if (transform.position.x > player.position.x + .5)
        {
            rigidFranken.velocity = new Vector2(-moveSpeed, rigidFranken.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void StopChasingPlayer()
    {
        rigidFranken.velocity = new Vector2(0, rigidFranken.velocity.y);
    }
}
