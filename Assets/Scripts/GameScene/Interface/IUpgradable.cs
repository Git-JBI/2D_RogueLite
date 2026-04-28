using UnityEngine;

public interface IUpgradable
{
    Sprite IconSprite { get; }

    int Level { get; }

    string Desc { get; }

    void Upgrade();
}
