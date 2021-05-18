using UnityEngine;
///
/// Author: Samuel Müller, sm184
/// Description: does nothing. Defines behaviour of player, when no weapon is equipped.
/// ==============================================
/// Changelog:
/// ==============================================
///

[CreateAssetMenu(menuName = "Abilities/Null/Spell")]
public class NullSpell : AbstractSpell
{
    public override void Initialize(GameObject obj)
    {
        //do nothing
    }

    public override void TriggerSpell()
    {
        //do nothing
    }
}
