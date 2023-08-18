using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moves : MonoBehaviour
{

	[SerializeField] float		                    speed = 20;
    [SerializeField] float                          jumpForce = 15;
    [SerializeField] private InputActionReference   actionToUse;
    [SerializeField] private InputActionReference   jump;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float gravityScale = 5;
    private float                                   velocity;
    private bool                                    isGrounded;

    // Start is called before the first frame update
    void Start()
    {}

	void Update()
	{
        velocity += gravity * gravityScale * Time.deltaTime;
        Vector2 moveDirection = actionToUse.action.ReadValue<Vector2>();
		Vector3 xMove = Camera.main.transform.right * moveDirection.x;
		Vector3 yMove = Camera.main.transform.forward * moveDirection.y;

		xMove = new Vector3(xMove.x, 0, xMove.z);
		yMove = new Vector3(yMove.x, 0, yMove.z);

        if (jump.action.triggered){
            velocity = jumpForce;
            isGrounded = false;
        }
        if (velocity < 0 && isGrounded)
            velocity = 0;
        Vector3 move = new Vector3((xMove.x + yMove.x) * speed, velocity, (xMove.z + yMove.z) * speed) * Time.deltaTime;
		transform.Translate(move.x, move.y, move.z, Camera.main.transform.parent.transform);
        if(moveDirection.x!= 0 || moveDirection.y!= 0)
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 40f);
        //print((move + new Vector3(0, velocity, 0)) * Time.deltaTime);
        //print(velocity);
    }

    void OnCollisionEnter(Collision col) {
        if (col.collider.name == "Ground")
            isGrounded = true;
    }
	
}