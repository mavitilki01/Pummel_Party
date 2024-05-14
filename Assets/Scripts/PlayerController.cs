using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
       
    public float movementSpeed = 5f;
    float rotationSpeed = 10f;
    private Rigidbody rb;
    private Animator animator;
    public List<GameObject> goldList;
    public int carry;

    public float reduceSpeed = 0.5f;
    private float baseMovementSpeed;
    public int carryLimit => goldList.Count;

    public Transform boneParent;
    public bool CanMove = true;
    public Transform spinePosition;
    void Start()
    {
        baseMovementSpeed = movementSpeed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Ragdoll(false);
    }

    void Update()
    { if (!CanMove) return;

        float horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movementDirection = new Vector3(-horizontal, 0, -vertical);

        animator.SetBool("isRunning", movementDirection != Vector3.zero);
        animator.SetBool("isCarrying", carry != 0);

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

    public bool CollectGold()
    {
        if (carry == carryLimit) return false;
        
        goldList[carry].gameObject.SetActive(true);
        carry++;


        movementSpeed -= reduceSpeed;
        


        return true;
    
    }

    public int DropGoldsFromHand()
    {
        var carryingGold = carry;
        if (carryingGold == 0) return 0;

        foreach (var gold in goldList)
            gold.SetActive(false);
        carry = 0;
        movementSpeed = baseMovementSpeed;
        return carryingGold;
    }
    public void Ragdoll(bool isActive)
    {
        animator.enabled = !isActive;
        var colliders = boneParent.GetComponentsInChildren<Collider>();
        var rigidbodies = boneParent.GetComponentsInChildren<Rigidbody>();
       
        foreach (var coll in colliders)
            coll.enabled = isActive;
        foreach ( var rig in rigidbodies)
            rig.isKinematic = !isActive;

        GetComponent<Collider>().enabled = !isActive;
        CanMove = !isActive;

        if (!isActive)
            StartCoroutine(CloseRagdoll());

}
    public IEnumerator CloseRagdoll()
    {
        yield return new WaitForSeconds(5f);
        Ragdoll(false);

        transform.position = new Vector3(spinePosition.position.x, 0, spinePosition.position.z);
    }

}
