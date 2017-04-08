using UnityEngine;
using UnityEngine.UI;

public class TroopCard : MonoBehaviour
{
    public GameObject troopPrefab;

    private Image troopImage;
    private Text troopCount;

    private void Awake()
    {
        troopImage = GetComponent<Image>();
        troopCount = transform.GetChild(0).GetComponent<Text>();

        troopImage.enabled = false;
        troopCount.enabled = false;
    }

    public void SetTroop(GameObject newTroop)
    {
        troopImage.enabled = true;
        troopCount.enabled = true;

        troopPrefab = newTroop;
        TroopInfo troopInfo = newTroop.GetComponent<TroopInfo>();

        troopImage.sprite = troopInfo.sprite;
        troopCount.text = troopInfo.count.ToString();

    }

    public void SpawnTroop(Vector3 position)
    {
        troopImage.enabled = false;
        troopCount.enabled = false;
        Instantiate(troopPrefab, position, Quaternion.identity);
        troopPrefab = null;
    }

}
