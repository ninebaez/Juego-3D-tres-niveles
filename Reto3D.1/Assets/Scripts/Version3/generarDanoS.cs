using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generarDanoS : MonoBehaviour
{
    public float cantidadDano;
    public Vida vida;

    private void OnCollisionEnter(Collision other){

        if(other.gameObject.CompareTag("Player"))
        {
            vida.TomarVida(cantidadDano);
            //vida.efectoDano(new Vector2(transform.position.x,transform.position.y), transform.localScale.x);
        }
    }
}
