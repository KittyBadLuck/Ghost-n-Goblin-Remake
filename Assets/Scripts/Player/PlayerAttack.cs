using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject currentWeapon;

    public Transform firePoint;
    public int maxWeaponsLaunched =3;

    public int weaponInScene =0;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        WeaponBehaviour weaponBehaviour = currentWeapon.GetComponent<WeaponBehaviour>();

        if (Input.GetButtonUp("Fire1") && weaponInScene < maxWeaponsLaunched && Time.timeScale == 1)
        {
            if (weaponBehaviour.arcThrow == false)
            {
                GameObject attack = Instantiate(currentWeapon, firePoint.position, transform.rotation );
                attack.GetComponent<WeaponBehaviour>().playerattackScript = this.GetComponent<PlayerAttack>();
                float speed = weaponBehaviour.speed;
                attack.GetComponent<Rigidbody>().AddForce(attack.transform.forward * speed);
                
                weaponInScene++;
            }
            else
            {
                Quaternion rotation = Quaternion.Euler(new Vector3(-30f, 0f, 0f));
                GameObject attack = Instantiate(currentWeapon, firePoint.position, transform.rotation*rotation ); 
                float speed = weaponBehaviour.speed;
                attack.GetComponent<Rigidbody>().AddForce(attack.transform.forward * speed);
                attack.GetComponent<WeaponBehaviour>().playerattackScript = this.GetComponent<PlayerAttack>();
                weaponInScene++;
            }
            
        }
        
        
        
    }
}
