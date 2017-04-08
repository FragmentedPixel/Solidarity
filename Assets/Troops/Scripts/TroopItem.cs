using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopItem : MonoBehaviour
{
    public GameObject troopPrefab;

    private TroopInfo troopInfo;
    private Image displayImage;
    private Text displayText;

    private void Start()
    {
        displayImage = transform.GetChild(0).GetComponent<Image>();
        displayText = transform.GetChild(1).GetComponent<Text>();

        if (troopPrefab)
        {
            troopInfo = troopPrefab.GetComponent<TroopInfo>();

            displayImage.sprite = troopInfo.troopSprite;
            displayText.text = troopInfo.count.ToString();
        }
    }



}
