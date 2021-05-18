using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: ElementApplier for world transform prefabs.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class ElementApplierByLifetime : ElementApplier
{
    private Timer _timer;

    override protected void Start()
    {
        base.Start();
        _timer = gameObject.AddComponent<Timer>();
        _timer.Lifetime = CurrentElement.Delay;
    }

    private void OnTriggerStay(Collider collision)
    {
        ApplyDebuff(collision);
    }
    private void OnTriggerExit(Collider collision)
    {
        ApplyDebuff(collision);
    }

    protected override bool GetApplyCondition(Collider collider)
    {
        return base.GetApplyCondition(collider) && _timer.HasExceded;
    }

    protected override void OnSuccess(Collider collider)
    {
        _timer.Restart();
    }
}
