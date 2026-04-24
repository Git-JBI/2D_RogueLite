using UnityEngine;
using UnityEngine.U2D.Animation;

public class GameSceneManager : MonoBehaviour
{
    private ResourceManager _resourceManager;
    [SerializeField]
    private HeroController _heroController;
    [SerializeField]
    private Joystick _joystick;

    private SpriteLibraryAsset _libraryAsset;

    private void Awake()
    {
        _resourceManager = GetComponent<ResourceManager>();

        _libraryAsset = _resourceManager.GetPrefabOnce<SpriteLibraryAsset>("SpriteLibrary/Hero/Hero00");
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _heroController.Init(_libraryAsset);

        _joystick.OnTouchDown += _heroController.MoveStart;
        _joystick.OnTouchUp += _heroController.MoveFinish;
        _joystick.OnDragging += _heroController.Move;
    }
}
