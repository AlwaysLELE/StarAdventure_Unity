using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float jumpForce = 5f;
		public float moveSpeed = 5.0f;
		public float turnSpeed = 100.0f;
		public float gravity = 9.8f;
		private Vector3 velocity;

		private bool isJumping = false;
	    public GameObject flame;
		



    void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		
		}

		void Update (){
        if (Input.GetKey("w")) {
            anim.SetInteger("AnimationPar", 1);
            anim.SetFloat("SpeedCtrl", 1);//正向播放
            flame.SetActive(true);


        } else if (Input.GetKey("s"))
        {
            anim.SetInteger("AnimationPar", 1);
            anim.SetFloat("SpeedCtrl", -1);//倒着播放
        }

        else {
            anim.SetInteger("AnimationPar", 0);
            flame.SetActive(false);

        }


        if (controller.isGrounded)
        {
        Vector3 movement = transform.forward * Input.GetAxis("Vertical") * moveSpeed;
        controller.Move(movement * Time.deltaTime);

         }


        float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

            velocity.y -= gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);

		if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))
		{

                      
            // 应用向上和向前的跳跃速度
            velocity.y = jumpForce;
            controller.Move(Vector3.forward * moveSpeed * Time.deltaTime);
			isJumping = true;
        }
		if (isJumping == false || Input.GetKeyDown(KeyCode.Space)&& controller.isGrounded)
		{
            velocity.y = jumpForce;
            isJumping = true;
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

    }
   

   

    
}

