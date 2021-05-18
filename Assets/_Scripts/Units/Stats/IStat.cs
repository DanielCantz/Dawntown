using System;
///
/// Author: Samuel Müller
/// Description: Interface for Stats
/// ==============================================
/// Changelog:
/// ==============================================
///
public interface IStat
{
    float GetValue();

    void AddMod(IStatModificator mod);

    void RemoveMod(IStatModificator mod);

    bool HasMod(IStatModificator mod);
}
