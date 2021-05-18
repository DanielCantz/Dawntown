using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller, sm184
/// Edit: Tina Truong, tt035
/// Description: Changes Spirtes of the Main Character based on player inputs.
/// ==============================================
/// Changelog: 
/// ==============================================
[RequireComponent(typeof(Animator))]
public class AnimationUpdater : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationHolder _animationHolder;
    private AnimatorOverrideController[,] _overrides;
    private bool _isDirectionLocked = false;
    enum Direction { Front, Back, Side };

    void Start()
    {
        _animator = GetComponent<Animator>();
        _overrides = _animationHolder.GetOverrideMatrix();
    }

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        bool isMoving = direction != Vector3.zero;
        _animator.SetBool("isMoving", isMoving);
        if (!_isDirectionLocked)
        {
            if (isMoving)
            {
                UpdateAnimator(direction);
            }
            else
            {
                UpdateAnimatorByMousePosition();
            }
        }
    }

    private void UpdateAnimatorByMousePosition()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = Input.mousePosition;
        Vector3 direction = (mousePosition - position).normalized;

        UpdateAnimator(direction);
    }

    private void UpdateAnimator(Vector3 direction)
    {
        /* Side direction has priority over Front and Back*/
        if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            int directionY = direction.y > 0 ? (int)Direction.Back : (int)Direction.Front;
            _animator.SetInteger("direction", directionY);
        }
        else
        {
            _animator.SetInteger("direction", (int)Direction.Side);
            int signed = direction.x > 0 && direction.x != 0 ? 1 : -1;

            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * signed, transform.localScale.y, transform.localScale.z);
        }
    }

    internal void RecalculateController(Ability attack, Ability defense)
    {
        _animator.runtimeAnimatorController = _overrides[(int)attack.Spell.WeaponMod, (int)defense.Spell.WeaponMod];
    }

    public void TriggerAttack()
    {
        UpdateAnimatorByMousePosition();
        BlockReorientation();
        _animator.SetTrigger("triggerAttack");
    }

    public void TriggerDefense(bool isDashing)
    {
        if (!isDashing)
        {
            UpdateAnimatorByMousePosition();
        }
        _animator.SetTrigger("triggerDefence");
        BlockReorientation();
    }

    private void BlockReorientation()
    {
        _isDirectionLocked = true;
        Invoke("UnblockReorientation", 1f);
    }

    private void UnblockReorientation()
    {
        _isDirectionLocked = false;
    }
}

