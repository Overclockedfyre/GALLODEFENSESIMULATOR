using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ItemPlacement placementController;
    public MoneyManager moneyManager;
    public GameObject itemPrefab;
    public int cost = 25;

    public void Select()
    {
        if (moneyManager.Money >= 200)
        {
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.BuyGallo);
        Debug.Log($"SHOP SELECT: {itemPrefab.name} cost={cost}");
        placementController.TakeItem(itemPrefab, cost);
        }
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.DontBuyGallo);
        
    }
}