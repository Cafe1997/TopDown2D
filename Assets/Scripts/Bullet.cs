using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D bullet;

    void FixedUpdate()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, bullet.rotation);
        bullet.MovePosition(bullet.position + speed * Time.fixedDeltaTime * (Vector2)(rotation * Vector2.right));
    }
}
