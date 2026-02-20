using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ItemPlacement placementController;
    public GameObject itemPrefab;
    public void GalloTower()
    {
        Debug.Log("clicked");
        placementController.TakeItem(itemPrefab);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
