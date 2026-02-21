using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ItemPlacement placementController;
    public GameObject itemPrefab;
    public int cost = 25;

    public void Select()
    {
        Debug.Log($"SHOP SELECT: {itemPrefab.name} cost={cost}");
        placementController.TakeItem(itemPrefab, cost);
    }
}