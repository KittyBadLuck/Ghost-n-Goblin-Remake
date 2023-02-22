using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    
    public void EndHit()
    {
        _enemy.isHit = false;
        _enemy.animator.SetBool("IsHit", false);
    }

    public void AttackRef()
    {
        _enemy.Attack();
    }

    public void DieRef()
    {
        _enemy.Die();
    }
}
