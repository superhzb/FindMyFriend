using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T:Component
{
    [SerializeField]
    protected T prefab;

    public static GenericObjectPool<T> Instance { get; private set; }
    protected Queue<T> objects = new Queue<T>();

    private void Awake()
    {
        Instance = this;
    }

    public virtual T Get()
    {
        if (objects.Count == 0)
        {
            AddObjects(1);
        }
        T item = objects.Dequeue();
        Init(item);
        return item;
    }

    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = Instantiate(prefab);
            newObject.gameObject.SetActive(false);
            newObject.transform.SetParent(transform);
            objects.Enqueue(newObject);
        }
    }

    protected abstract void Init(T item);
}
