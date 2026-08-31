using UnityEngine;

public class AbilityGold : MonoBehaviour, IUpgradable
{
    [SerializeField] private Sprite _goldSprite;
    private GameSceneModel _sceneModel;

    public Sprite IconSprite => _goldSprite;
    public int Level => 0;
    public int MaxLevel => 0;
    public string Desc => "골드 50 획득";
    public bool IsPermanentAbility => false;

    public void Init(GameSceneModel model)
    {
        _sceneModel = model;
    }

    #region Interface
    public void Upgrade()
    {
        // 골드 50 획득
        _sceneModel.IncreaseStageGold(50);
    }
    #endregion
}