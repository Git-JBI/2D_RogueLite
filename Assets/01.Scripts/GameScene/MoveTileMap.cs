using UnityEngine;

public class MoveTileMap : MonoBehaviour
{
    private HeroController _target;

    private void Awake()
    {
        _target = FindAnyObjectByType<HeroController>();
    }

    private void Update()
    {
        MoveMap();
    }

    private void MoveMap()
    {
        float distanceX = (transform.position.x - _target.transform.position.x);
        float distanceY = (transform.position.y - _target.transform.position.y);

        // X축 이동
        if(Mathf.Abs(distanceX) > 18.0f)
        {
            if (transform.position.x - _target.transform.position.x > 0)
                transform.position = new Vector3(transform.position.x - 36.0f, transform.position.y, 0);
            else
                transform.position = new Vector3(transform.position.x + 36.0f, transform.position.y, 0);
        }

        // Y축 이동
        if(Mathf.Abs(distanceY) > 30.0f)
        {
            if(transform.position.y - _target.transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 60.0f, 0);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 60.0f, 0);
            }
        }
    }
}
