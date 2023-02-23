using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject currentWeapon;

    public Transform firePoint;
    private CharaController _charaController;
    private PlayerManager _playerManager;
    public ArmorBehavior armorBehavior;

    // Start is called before the first frame update
    private void Awake()
    {
        _charaController = GetComponentInParent<CharaController>();
        _playerManager = GetComponentInParent<PlayerManager>();
    }

    public void Attack()
    {
        WeaponBehaviour weaponBehaviour = currentWeapon.GetComponent<WeaponBehaviour>();
        if (weaponBehaviour.arcThrow == false)
        {
            GameObject attack = Instantiate(currentWeapon, firePoint.position, transform.rotation);
            attack.GetComponent<WeaponBehaviour>().charaController = this.GetComponent<CharaController>();
            float speed = weaponBehaviour.speed;
            attack.GetComponent<Rigidbody>().AddForce(attack.transform.forward * speed);

            _charaController.weaponInScene++;
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(-30f, 0f, 0f));
            GameObject attack = Instantiate(currentWeapon, firePoint.position, transform.rotation * rotation);
            float speed = weaponBehaviour.speed;
            attack.GetComponent<Rigidbody>().AddForce(attack.transform.forward * speed);
            attack.GetComponent<WeaponBehaviour>().charaController = this.GetComponent<CharaController>();
            _charaController.weaponInScene++;
        }
    }

    public void EndThrow()
    {
        this.GetComponent<Animator>().SetBool("IsThrowing", false);
    }

    public void Hit()
    {
        _playerManager.damageTaken++;
        this.GetComponent<Animator>().SetBool("IsHit", false);
    }

    public void LooseArmor()
    {
        armorBehavior.LooseArmor();
    }


    public void Die()
    {
        _playerManager.damageTaken++;
        _playerManager.Die();
    }
}
