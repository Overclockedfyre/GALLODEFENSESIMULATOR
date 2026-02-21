using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public Canvas canvas;

    private GameObject currentItem;
    private bool isPlacing = false;
    private bool moving = false;
    private Vector3 originalScale;

    public void TakeItem(GameObject prefab)
    {
        if (isPlacing) return;

        currentItem = Instantiate(prefab, canvas.transform);
        originalScale = currentItem.transform.localScale;
        isPlacing = true;
        moving = true;
    }

    void Update()
    {
        if (!isPlacing) return;

        FollowMouse();

        if (moving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moving = false;
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ConfirmPlacement();
        }
    }

    void FollowMouse()
    {
        RectTransform rectTransform = currentItem.GetComponent<RectTransform>();
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint
    );

    rectTransform.localPosition = localPoint;
    }

    void ConfirmPlacement()
    {
        currentItem.transform.localScale = originalScale * 2f;

        isPlacing = false;
        currentItem = null;
    }
}