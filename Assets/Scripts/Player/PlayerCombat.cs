using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    private Transform attackPoint;

    public float attackRange;
    public LayerMask enemyLayers;

    public int attackDamage;

    public float attackRate;
    float nextAttackTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
        attackPoint = GameObject.Find("AttackPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        //Plays the attack animation
        anim.Play("Player_Attack");


        int randomSound = Random.Range(1, 3); //Used to swap between sword swing sounds

        if (randomSound == 1)
        {
            AudioManager.instance.Play("Swing1");
        } else if (randomSound == 2)
        {
            AudioManager.instance.Play("Swing2");
        }

        //Detects all enemies within range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damages enemies in hitenemy range
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyTakeDamage>().TakeDamage(attackDamage);
        }
    }

    //Used to see attack range in inspector
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
