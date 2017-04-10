using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject draggedObject;

    private GameObject location;
    private Vector3 startPosition;

    private void Start()
    {
        location = GameObject.FindGameObjectWithTag("Location");
        location.transform.position = -Vector3.one;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedObject = gameObject;
        draggedObject.GetComponent<Image>().enabled = false;

        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        location.transform.position = MouseCast();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        location.transform.position = -Vector3.one;

        Vector3 hitPoint = MouseCast();

        if(hitPoint != -Vector3.one)
            draggedObject.GetComponent<TroopCard>().SpawnTroop(hitPoint);
        else
            RestoreCard();
    }

    private void RestoreCard()
    {
        draggedObject.transform.position = startPosition;
        draggedObject.GetComponent<Image>().enabled = true;
    }
    
    private Vector3 MouseCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit) && rayHit.collider.CompareTag("Terrain") && !EventSystem.current.IsPointerOverGameObject())
            return rayHit.point;

        return -Vector3.one;
    }
}