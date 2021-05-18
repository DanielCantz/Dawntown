using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

/// Audio-Part by Michael Dmoch, md118

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static ItemSlot previousItemSlot;
    public static ItemSlot PreviousItemSlot
    {
        get => previousItemSlot;
    }
    Vector3 startPosition;
    Transform startParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        previousItemSlot = startParent.parent.GetComponent<ItemSlot>();
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //sound begin drag
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Inventory/Select");

        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
            previousItemSlot = null;
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}