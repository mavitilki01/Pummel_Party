using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    private int currentGold;
    public List<GameObject> golds;
    public GameObject goldsParent;
    public TextMeshProUGUI scoreText;
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

        var gold = player.DropGoldsFromHand();
        currentGold += gold;
        scoreText.SetText("Collected Gold: " + currentGold);

        for(int i =0;i< currentGold; i++) 
            golds[i].SetActive(true);

    }
   
}
