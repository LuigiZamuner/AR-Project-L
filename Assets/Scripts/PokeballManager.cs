using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeballManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject PrefabPokeball;

    private List<GameObject> instantiatedPokeballs = new List<GameObject>();    

    public GameObject SpawnPokeball()
    {
        GameObject pokeball = GetPokeball();

        pokeball.SetActive(true);

        return pokeball;
    }

    private GameObject GetPokeball()
    {
        GameObject pokeball = instantiatedPokeballs.Find(p => !p.gameObject.activeSelf) ?? CreatePokeball();
        return pokeball;
    }

    private GameObject CreatePokeball()
    {
        GameObject pokeball = Instantiate(PrefabPokeball);
        instantiatedPokeballs.Add(pokeball);
        return pokeball;
    }
}
