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

    private void Start()
    {
        anim = GetComponent<Animator>();
        attackPoint = GameObject.Find("AttackPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    void Attack()
    {
        //Plays the attack animation
        anim.Play("Player_Attack");

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
