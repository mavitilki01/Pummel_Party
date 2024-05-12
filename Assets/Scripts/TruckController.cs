using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    private int currentGold;
    public List<GameObject> golds;
    public GameObject goldsParent;
    void Start()
    {
        golds = new List<GameObject>();
        foreach (Transform gold in goldsParent.transform) 
        {
            golds.Add(gold.gameObject);
            gold.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Player") return;
        var player = other.gameObject.GetComponent<PlayerController>();
        var gold = player.LoadGoldsToTruck();
        currentGold += gold;

        for(int i =0;i< currentGold; i++) 
            golds[i].SetActive(true);

    }
   
}
