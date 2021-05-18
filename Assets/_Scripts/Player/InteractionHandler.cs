using UnityEngine;

/// Author: Samuel Müller, sm184
/// Description: Lets the player interact with interactible objects
/// ==============================================
/// Changelog: 
/// ==============================================
public class InteractionHandler : MonoBehaviour
{
    [SerializeField]
    private Interactible _focus;

    public Interactible Focus
    {
        get { return _focus; }
    }

    public bool SetFocus(Interactible interactible)
    {
        if (_focus == null)
        {
            _focus = interactible;
            return true;
        }
        return false;
    }

    public bool RemoveFocus(Interactible interactible){
        if (_focus == interactible)
        {
            _focus = null;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (_focus != null)
        {
            _focus.Interact();
        }
    }
}
