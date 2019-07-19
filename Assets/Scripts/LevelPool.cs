using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPool : GenericObjectPool<Level>
{
    public Level[] levelPrefabs;

    public override Level Get()
    {
        if (objects.Count == 0)
        {
            for (int i = 0; i < levelPrefabs.Length; i++)
            {
                var newObject = Instantiate(levelPrefabs[i]);
                newObject.gameObject.SetActive(false);
                newObject.transform.SetParent(transform);
                objects.Enqueue(newObject);
            }
        }

        Level level = objects.Dequeue();
        Init(level);
        return level;
    }

    protected override void Init(Level level)
    {
        //Do nothing.
    }
}
