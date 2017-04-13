using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public GameObject[] troops;
    public TroopCard[] cards;

    private void Start()
    {
        SetCards();
    }

    private void SetCards()
    {
        for(int i = 0; i < troops.Length; i++)
        {
            cards[i].SetTroop(troops[i]);
        }
    }

}
