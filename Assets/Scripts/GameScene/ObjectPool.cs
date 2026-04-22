using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject _pooledObject;
    private Stack<GameObject> _gameObjects = new Stack<GameObject>();
}
