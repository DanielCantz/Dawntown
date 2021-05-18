using UnityEngine;

///
/// Author: Samuel Müller: sm184
/// Description: Defines Interactable Elements and provides according functionalities
/// ==============================================
/// Changelog: 
/// ==============================================
///
public abstract class Interactible : MonoBehaviour
{
    [SerializeField]
    private float _radius = 3f;

    [SerializeField]
    private InteractionHandler _interactionHandler;

    [SerializeField]
    private float _targetHeight;

    protected virtual void Awake()
    {
        _interactionHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_interactionHandler.Focus)
        {
            _interactionHandler.SetFocus(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interactionHandler.RemoveFocus(this);
        }
    }


    /// <summary>
    /// Draws Gizmos around the Object
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    /// <summary>
    /// Abstract Method, that is overwritten by the class it gets inherrited from
    /// </summary>
    public abstract void Interact();
    public void UpdateSprite(Sprite sprite)
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.sprite = sprite;
        Bounds bounds = sprite.bounds;
        float factor = _targetHeight / bounds.size.y;
        renderer.transform.localScale = Vector3.one * factor;
    }
}
