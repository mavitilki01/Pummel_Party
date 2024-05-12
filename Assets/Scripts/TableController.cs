using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{

    public GameObject goldObject;

    public bool isGoldCollectable => goldObject.activeSelf;
    private void OnCollisionEnter(Collision other)
    {
        if (!isGoldCollectable) return;
       if (other.gameObject.tag != "Player")  return;

       var player = other.gameObject.GetComponent<PlayerController>();
       
        if (player.CollectGold())
        {
            goldObject.SetActive(false);
        }

    }


  
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
}
