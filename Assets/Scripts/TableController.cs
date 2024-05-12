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

            // 5- 15 saniye içinde random reload etmek
            Invoke(nameof(ReloadGold), Random.Range(5f,15f));
        }

    }
    private void ReloadGold()
    {
        goldObject.SetActive(true);
    }


    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
}
