using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : Ball
{

    public override void Drop()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball hitBall = collision.collider.GetComponent<Ball>();

        if (hitBall != null)
        {
            hitBall.Cut(collision.contacts[0].normal);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball hitBall = collision.GetComponent<Ball>();

        if (hitBall != null)
        {
            Vector3 dir = transform.position - collision.transform.position;
            hitBall.Cut(dir.normalized);
        }
    }
}
