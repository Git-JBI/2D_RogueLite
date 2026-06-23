using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public void Init(Vector2 spawnPos)
    {
        transform.position = spawnPos;
        transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magnatic"))
        {
            Iitemable itemable = collision.gameObject.GetComponentInParent<Iitemable>();

            itemable.GetExpItem(10);

            PoolingObject poolOb = GetComponent<PoolingObject>();
            poolOb.ReturnToPool();
        }
    }
}
