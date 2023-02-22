using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    public GameObject[] weaponLoot;
    public GameObject dollLoot;
    public GameObject AccessoryLoot;
    private int lootInt = 0;
   

    public void Loot(Vector3 position)
    {
        lootInt++;
        if (lootInt % 2 != 0)
        {
            Instantiate(dollLoot, position, dollLoot.transform.rotation);
        }
        else if(lootInt == 4)
        {
            Instantiate(AccessoryLoot, position, AccessoryLoot.transform.rotation);
        }
        else
        {
            int i = (int)Random.Range( 0 ,  weaponLoot.Length );
            Instantiate(weaponLoot[i], position, weaponLoot[i].transform.rotation);
        }

        if (lootInt >= 7)
        {
            lootInt = 0;
        }
    }
}
