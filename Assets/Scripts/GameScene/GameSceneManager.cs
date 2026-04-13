using UnityEngine;
using UnityEngine.U2D.Animation;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    private HeroController _heroController;
    [SerializeField]
    private Joystick _joystick;

    private SpriteLibraryAsset _libraryAsset;

    private void Awake()
    {
        //TODO 추후 다른곳에서 불러옴
        _libraryAsset = Resources.Load<SpriteLibraryAsset>("SpriteLibrary/Hero/Hero00");
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
