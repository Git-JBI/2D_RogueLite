using UnityEngine;
using UnityEngine.U2D.Animation;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private HeroController _heroController;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private UIManager _ui;
    [SerializeField] private EnemySpawner _enemySpawner;

    private SpriteLibraryAsset _libraryAsset;

    private void Awake()
    {

    }

    private void Start()
    {
        //TODO 메인씬에서 선택한 캐릭터로 불러오기
        _libraryAsset = GameManager.Instance.ResourceManager.GetPrefabOnce<SpriteLibraryAsset>("SpriteLibrary/Hero/Hero00");

        Init();
    }

    private void Init()
    {
        _heroController.Init(_libraryAsset, _ui);
        _enemySpawner.Init(_ui);

        _joystick.OnTouchDown += _heroController.MoveStart;
        _joystick.OnTouchUp += _heroController.MoveFinish;
        _joystick.OnDragging += _heroController.Move;
    }
}
