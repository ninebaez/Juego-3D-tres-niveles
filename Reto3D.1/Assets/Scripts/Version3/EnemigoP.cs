using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoP : MonoBehaviour
{
    public Rigidbody platformRB;
    public Transform[] platformPositions;
    public float platformSpeed;

    private int actualPosition = 0;
    private int nextPosition = 1;

    public bool moveToTheNext = true;
    public float waitTime;



    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        Vector3 posEnemigo = new Vector3(platformPositions[nextPosition].position.x,transform.position.y, platformPositions[nextPosition].position.z);
        transform.LookAt(posEnemigo);
        if(moveToTheNext){
            StopCoroutine(WaitForMove(0));
            platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, posEnemigo, platformSpeed * Time.deltaTime));
        }
        

        if(Vector3.Distance(platformRB.position, posEnemigo) <= 0){
            StartCoroutine(WaitForMove(waitTime));
            actualPosition = nextPosition;
            nextPosition++;

            if(nextPosition > platformPositions.Length -1){
                nextPosition = 0;
            }
        }

    }

    IEnumerator WaitForMove(float time){
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext = true;
    }
}
