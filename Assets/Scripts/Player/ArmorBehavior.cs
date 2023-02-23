using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBehavior : MonoBehaviour
{

    public GameObject armorShards;
    private ParticleSystem[] systems;
    [SerializeField] private List<GameObject> armorParts = new List<GameObject>();
    private List<Renderer> armorRenderer = new List<Renderer>(); 

    // Start is called before the first frame update
    void Start()
    {
        systems = armorShards.GetComponentsInChildren<ParticleSystem>();

        ParticleSystem.EmissionModule emissionModule;

        foreach (var system in systems)
        {
            emissionModule = system.emission;
            emissionModule.enabled = false; 
        }

        foreach (var armorPart in armorParts)
        {
            armorRenderer.Add(armorPart.GetComponent<MeshRenderer>());
            
            Debug.Log(armorRenderer.Count);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ParticleSystem.EmissionModule emissionModule;

            foreach (var system in systems)
            {
                emissionModule = system.emission;
                emissionModule.enabled = true; 
            }

            foreach (var armorRenderer in armorRenderer)
            {
                armorRenderer.material.SetFloat("_AlphaThreshold", 1); 
                Debug.Log(armorRenderer.material);
            }
        }
    }
}
