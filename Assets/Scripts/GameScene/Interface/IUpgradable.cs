using UnityEngine;

public interface IUpgradable
{
    Sprite IconSprite { get; }

    int Level { get; }

    int MaxLevel { get; }

    string Desc { get; }

    bool IsPermanentAbility { get; }

    void Upgrade();
}
