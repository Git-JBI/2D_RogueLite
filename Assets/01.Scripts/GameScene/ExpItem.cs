using System;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    private int _exp;
    private Sprite[] _expSprites = new Sprite[3];
    private SpriteRenderer _spriteRenderer;
    public void Init(Vector2 spawnPos, int expType)
    {
        transform.position = spawnPos;
        transform.SetParent(null);

        InitSprites();

        if(_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        switch (expType)
        {
            case 0:
                _exp = 10;
                _spriteRenderer.sprite = _expSprites[0];
                break;
            case 1:
                _exp = 20;
                _spriteRenderer.sprite = _expSprites[1];
                break;
            case 2:
                _exp = 50;
                _spriteRenderer.sprite = _expSprites[2];
                break;
        }
    }

    private void InitSprites()
    {
        if (_expSprites[0] == null)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/Props");
            for(int i = 0; i < _expSprites.Length; i++)
            {
                _expSprites[i] = Array.Find(sprites, x => x.name == $"Exp {i}");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnatic"))
        {
            IItem itemable = collision.gameObject.GetComponentInParent<IItem>();

            itemable.GetExpItem(_exp);

            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
