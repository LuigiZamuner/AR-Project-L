using System.Collections;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public IEnumerator Lifetime()
    {
        _rb.useGravity = true;
        yield return new WaitForSeconds(4);
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(CatchPokemon(collision));
    }

    private IEnumerator CatchPokemon(Collision collisionGameobject)
    {
        collisionGameobject.gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1.5f);
        collisionGameobject.gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
