using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDeletion : MonoBehaviour, IDropHandler
{
    private ItemSlot previousItemSlot;

    public void OnDrop(PointerEventData eventData)
    {
        previousItemSlot = ItemDragHandler.PreviousItemSlot;
        Item draggedItem = previousItemSlot.GetItem();
        EventLog.Instance.ItemDeleted(draggedItem);
        //Debug.Log("Deleted Item " + draggedItem.ToString());
        previousItemSlot.SetItemAndReload(new NullItem());
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Inventory/Drop");
    }
}