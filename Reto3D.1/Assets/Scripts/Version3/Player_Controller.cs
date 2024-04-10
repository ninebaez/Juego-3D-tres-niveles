using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;
    public CharacterController player;
    
    public float playerSpeed;
    public Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    public Camera mainCamera;
    private Vector3 camForWard;
    private Vector3 camRight;
   
    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    private Animator animator;
    private bool move=false;
    private bool enSuelo=true;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        if(Mathf.Abs(horizontalMove) != 0 || Mathf.Abs(verticalMove) != 0){
            move=true;
        }else{
            move=false;
        }

         if(player.isGrounded){
            enSuelo = true;
        }

        animator.SetBool("Mover",move);
        //animator.SetTrigger("Suelo");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();
        
        movePlayer = playerInput.x * camRight + playerInput.z * camForWard;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);
    }

    public void camDirection(){
        camForWard = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForWard.y = 0;
        camRight.y = 0;

        camForWard = camForWard.normalized;
        camRight = camRight.normalized;
    }

    public void SetGravity(){
        

        if(player.isGrounded){
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }else{
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }

        SlideDown();
    }


    public void PlayerSkills(){
        if(player.isGrounded && Input.GetButtonDown("Jump")){
            animator.SetTrigger("Suelo");
            enSuelo = false;
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }

    public void SlideDown(){
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if(isOnSlope){
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity; 
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;
            movePlayer.y += slopeForceDown;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        hitNormal = hit.normal;
    }


}
