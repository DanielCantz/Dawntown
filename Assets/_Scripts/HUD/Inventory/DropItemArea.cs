using UnityEngine;
using UnityEngine.EventSystems;

public class DropItemArea : MonoBehaviour, IDropHandler
{
    private GameObject _player, _collectableItem;

    private ItemSlot previousItemSlot;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnDrop(PointerEventData eventData)
    {
        previousItemSlot = ItemDragHandler.PreviousItemSlot;
        if (previousItemSlot == null || !previousItemSlot.IsItemDroppable)
        {
            return;
        }
        Item draggedItem = previousItemSlot.GetItem();
        DropUtil.DropItem(_player.transform.Find("BulletSpawn").transform, draggedItem);
        Debug.Log("Dropped Item " + draggedItem.ToString());
        previousItemSlot.SetItemAndReload(new NullItem());
    }
}
