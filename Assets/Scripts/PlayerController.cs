using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
       
    public float movementSpeed = 5f;
    float rotationSpeed = 10f;
    private Rigidbody rb;
    private Animator animator;
 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movementDirection = new Vector3(-horizontal, 0, -vertical);

        animator.SetBool("isRunning", movementDirection != Vector3.zero);

        if (movementDirection == Vector3.zero)
        {
            Debug.Log("Input yok");
            rb.velocity = Vector3.zero;
            return;
        }


        
        rb.velocity = movementDirection * movementSpeed;

        var rotationDireciton = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDireciton, rotationSpeed * Time.deltaTime);
    }
}
