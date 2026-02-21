using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public Canvas canvas;
    public TowerPlacementFree worldPlacer;   // drag TowerPlacementSystem here in Inspector

    private GameObject selectedPrefab;       // the real tower prefab to place
    private GameObject currentItem;          // UI ghost instance
    private bool isPlacing = false;

    private Vector3 originalScale;

    public void TakeItem(GameObject prefab)
    {
        if (isPlacing) return;
        if (prefab == null) return;

        selectedPrefab = prefab;

        // Create UI ghost under the canvas
        currentItem = Instantiate(prefab, canvas.transform);

        // Make sure it behaves like UI: remove collider if prefab has it
        foreach (var col in currentItem.GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        // Optional: make ghost semi-transparent if it has SpriteRenderers
        foreach (var sr in currentItem.GetComponentsInChildren<SpriteRenderer>())
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);

        originalScale = currentItem.transform.localScale;

        isPlacing = true;
        FollowMouse();
    }

    void Update()
    {
        if (!isPlacing || currentItem == null) return;

        FollowMouse();

        // Drop on mouse release
        if (Input.GetMouseButtonUp(0))
        {
            Drop();
        }

        // Optional cancel
        if (Input.GetMouseButtonDown(1))
        {
            Cancel();
        }
    }

    void FollowMouse()
    {
        RectTransform rectTransform = currentItem.GetComponent<RectTransform>();

        // If prefab is not a UI object, it may not have RectTransform.
        // In that case, just move the transform in screen space-ish local positions.
        if (rectTransform == null)
        {
            // Fallback: still roughly follow mouse in canvas space
            Vector2 local;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out local
            );
            currentItem.transform.localPosition = local;
            return;
        }

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out localPoint
        );

        rectTransform.localPosition = localPoint;
    }

    void Drop()
    {
        bool placed = false;

        if (worldPlacer != null)
        {
            placed = worldPlacer.TryPlaceAtScreen(Input.mousePosition, selectedPrefab);
        }

        // If placement succeeded, destroy the ghost.
        // If it failed, keep the ghost so the player can try again.
        if (placed)
        {
            Destroy(currentItem);
            currentItem = null;
            isPlacing = false;
            selectedPrefab = null;
        }
        else
        {
            // Optional: feedback, or snap back, or keep dragging
            // For now, keep dragging so they can move and release again
        }
    }

    void Cancel()
    {
        if (currentItem != null) Destroy(currentItem);
        currentItem = null;
        isPlacing = false;
        selectedPrefab = null;
    }
}