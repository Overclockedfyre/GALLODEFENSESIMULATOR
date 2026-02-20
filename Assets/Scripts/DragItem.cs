using UnityEngine;
using UnityEngine.EventSystems; 

// Click shop item
// A copy is created
// That copy immediately follows the mouse (no hold required)
// Click again to place
// Once placed → cannot go back

//ShopItem → handles click → tells system to spawn
//PlacementController → handles movement + click to place

public class DragItem : MonoBehaviour, IBeginDragHandler,  IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    private Vector3 originalSize;
    public bool GalloShopped = false;
    public void OnBeginDrag(PointerEventData eventData)
{
    Debug.Log("start drag");
    parentAfterDrag = transform.parent;
    transform.SetParent(transform.root);
    transform.SetAsLastSibling();
    
}
    public void OnDrag(PointerEventData eventData)
    {
        //drag items and move with mouse
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector3 globalMousePos
            );

        transform.position = globalMousePos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        transform.SetParent(parentAfterDrag);

        // The tower resize bigger when it is pulled outside of the inventory
        if (eventData.pointerEnter == null || !eventData.pointerEnter.CompareTag("Inventory")){ 
            transform.localScale = originalSize * 2;
        } else {
        transform.localScale = originalSize; 
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalSize = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
