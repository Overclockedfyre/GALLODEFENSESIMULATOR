using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ItemPlacement placementController;
    public GameObject itemPrefab;
    public int cost = 25;

    public void Select()
    {
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.BuyGallo);
        Debug.Log($"SHOP SELECT: {itemPrefab.name} cost={cost}");
        placementController.TakeItem(itemPrefab, cost);
    }
}