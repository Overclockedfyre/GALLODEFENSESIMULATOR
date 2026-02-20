using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    public bool isPlacing = false;
    public GameObject currentItem;
    public void TakeItem()
    {

        isPlacing = true;
        // called by shop item
        // is placing turns true
        // copy an item here
        
    }

    public void Update()
    {
        //check every frame if taking item is doing the same thing
        //detect click here, if clicked call confirmpalcement()
         
    }
    public void TakingItem()
    {
       // copied item follow the mouse
    }
    public void ConfirmPlacement()
    {
        //check if it is clicked again, if yes, place item in teh same location
        //placed item resize to a bigger version
    }
}
