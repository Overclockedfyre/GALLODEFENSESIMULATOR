using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public Canvas canvas;
    public TowerPlacementFree worldPlacer;   // drag TowerPlacementSystem here in Inspector

    private GameObject selectedPrefab;       // the real tower prefab to place
    private GameObject currentItem;          // UI ghost instance
    private bool isPlacing = false;
    private int selectedCost;
    private Vector3 originalScale;

    public void TakeItem(GameObject prefab, int cost)
    {
        if (isPlacing) return;
        if (prefab == null) return;

        selectedPrefab = prefab;
        selectedCost = Mathf.Max(0, cost);

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
        Debug.Log($"DROP: isPlacing={isPlacing}, selectedPrefab={(selectedPrefab ? selectedPrefab.name : "null")}, " +
                  $"cost={selectedCost}, worldPlacer={(worldPlacer ? worldPlacer.name : "null")}, " +
                  $"money={(MoneyManager.Instance ? MoneyManager.Instance.Money.ToString() : "MoneyManager NULL")}");

        if (worldPlacer == null) { Debug.Log("BLOCKED: worldPlacer is null"); return; }
        if (selectedPrefab == null) { Debug.Log("BLOCKED: selectedPrefab is null"); return; }

        bool canPlace = worldPlacer.CanPlaceAtScreen(Input.mousePosition);
        Debug.Log("canPlace=" + canPlace);

        if (!canPlace) { Debug.Log("BLOCKED: CanPlaceAtScreen false"); return; }

        if (MoneyManager.Instance == null) { Debug.Log("BLOCKED: MoneyManager.Instance is null"); return; }

        bool spent = MoneyManager.Instance.TrySpend(selectedCost);
        Debug.Log("spent=" + spent);

        if (!spent) { Debug.Log("BLOCKED: Not enough money"); return; }

        worldPlacer.PlaceAtScreen(Input.mousePosition, selectedPrefab);
        Debug.Log("PLACED!");

        Destroy(currentItem);
        currentItem = null;
        isPlacing = false;
        selectedPrefab = null;
        selectedCost = 0;
    }

    void Cancel()
    {
        if (currentItem != null) Destroy(currentItem);
        currentItem = null;
        isPlacing = false;
        selectedPrefab = null;
    }
}