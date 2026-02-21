using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public TowerPlacementFree placementSystem;
    public GameObject itemPrefab;

    public void SelectTower()
    {
        placementSystem.SetCurrentTower(itemPrefab);
    }
}