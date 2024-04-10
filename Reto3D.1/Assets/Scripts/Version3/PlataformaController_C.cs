using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaController_C : MonoBehaviour
{
    private Rigidbody rb;
    public float tiempoEspera = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("Toco plataforma");
        if(other.gameObject.CompareTag("Player")){
            StartCoroutine(Caida());
        }
    }

    private IEnumerator Caida(){
        yield return new WaitForSeconds(tiempoEspera);
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;

    }
}
