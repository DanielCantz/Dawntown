using UnityEngine;
using UnityEngine.UI;
///
/// Author: Marvin Kalchschmidt: mk306
/// Description: Gets Item information and displays it on screen as tooltip
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text _itemNameText;
    [SerializeField] Text _itemTypeText;
    [SerializeField] Text _itemDescriptionOffensiveText;
    [SerializeField] Text _itemDescriptionDefensiveText;

    [SerializeField] private GameObject _tooltipObject;
    [SerializeField] private Vector3 offset;

    //Function that adjusts tooltip position on game tick
    private void Update()
    {
        FollowCursor();
    }

    //Sets tooltip position to cursor position + offset
    private void FollowCursor()
    {
        if (!_tooltipObject.activeSelf) { return; }
        Vector3 newPosition = Input.mousePosition + offset;
        newPosition.z = 0f;
        _tooltipObject.transform.position = newPosition;
    }

    //Show tooltip on Screen with data from item
    public void ShowTooltip(Item item)
    {
        if(item != null)
        {
            AbilityHUDElement info = item.HudInfo;
            _itemNameText.text = info.Description.Title + " Tier " + item.Tier.ToString();
            _itemNameText.color = info.Color;
            _itemTypeText.text = info.Description.Type;
            _itemDescriptionOffensiveText.text = "<i>Offensive:</i> " + info.Description.DescriptionOffensive; 
            _itemDescriptionDefensiveText.text = "<i>Defensive:</i> " + info.Description.DescriptionDefensive;
        
            gameObject.SetActive(true);
        }
        
    }
    
    //Hide tooltip
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
