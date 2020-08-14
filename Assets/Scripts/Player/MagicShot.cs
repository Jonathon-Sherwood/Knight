using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    PlayerMagic playerMagic;
    Rigidbody2D rb;
    public LayerMask obstacles;
    public GameObject magicDispersePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMagic = GameObject.Find("Player").GetComponent<PlayerMagic>();
        rb.velocity = transform.right * playerMagic.magicSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Deals damage to enemy if hit
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyTakeDamage>().TakeDamage(playerMagic.magicDamage);
            Instantiate(magicDispersePrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

        //Detects if the bullet hits the ground layer.
        if(collision.gameObject.layer == 8)
        {
            Instantiate(magicDispersePrefab, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
