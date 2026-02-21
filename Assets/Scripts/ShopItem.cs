using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ItemPlacement placementController;
    public GameObject itemPrefab;
    public void GalloTower()
    {
        Debug.Log("clicked");
        placementController.TakeItem(itemPrefab);

        //GalloTower() is a button
        //currency happening here 

    }

}
