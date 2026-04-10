using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private HeroController _heroController;

    private void Awake()
    {
        //TODO 이렇게 사용해도 될지
        _heroController = FindAnyObjectByType<HeroController>();
    }

    private void Start()
    {
        // _heroController.Init();
    }
}
