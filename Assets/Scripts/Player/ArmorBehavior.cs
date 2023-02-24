using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBehavior : MonoBehaviour
{

    public GameObject armorShards;
    private ParticleSystem[] systems;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        systems = armorShards.GetComponentsInChildren<ParticleSystem>();
        _animator = GetComponent<Animator>();

        ParticleSystem.EmissionModule emissionModule;

        foreach (var system in systems)
        {
            emissionModule = system.emission;
            emissionModule.enabled = false; 
        }
    }
    public void LooseArmor()
    {
        _animator.SetBool("Desintegrate", true);
        _animator.SetBool("Appear", false);
        ParticleSystem.EmissionModule emissionModule;

        foreach (var system in systems)
        {
            emissionModule = system.emission;
            emissionModule.enabled = true;
            system.Play();
        }
    }
}
