﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    PlayerMagic playerMagic;
    Rigidbody2D rb;
    public GameObject magicDispersePrefab; //creates the destruction effect on impact

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("MagicCast");
        rb = GetComponent<Rigidbody2D>();
        playerMagic = GameObject.Find("Player").GetComponent<PlayerMagic>();
        rb.velocity = transform.right * playerMagic.magicSpeed;

        //Destroys this regardless of hitting something after certain time.
        Destroy(this.gameObject, playerMagic.magicShotDestructionTime);
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
