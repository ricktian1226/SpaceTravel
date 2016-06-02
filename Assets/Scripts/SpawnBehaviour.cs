using UnityEngine;
using System.Collections;

public class SpawnBehaviour : MonoBehaviour {
    [Tooltip("成员列表")]
    public GameObject[] Items;

    [Tooltip("所在层")]
    public GameObject Layer;

    [Tooltip("时间间隔")]
    public Vector2 Interval;

    [Tooltip("缩放值")]
    public float Scale;

    private float lastSpawnTime = 0;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        spawn();
    }

    void spawn()
    {
        spawnSomething(Items,
            Layer,
            ref lastSpawnTime,
            Interval);
    }
    void spawnSomething(GameObject[] things, GameObject layer, ref float lastSpawnTime, Vector2 SpawnInteval)
    {
        if (things.Length <= 0)
        {
            return;
        }

        var now = Time.time;

        if (lastSpawnTime + Random.Range(SpawnInteval.x, SpawnInteval.y) < now)
        {
            var o = Instantiate(things[Random.Range(0, things.Length)], new Vector3(Random.Range(-SingletonMonoBehaviour<GameManager>.Instance.XBoundary, SingletonMonoBehaviour<GameManager>.Instance.XBoundary), SingletonMonoBehaviour<GameManager>.Instance.YBoundary, 0f), Quaternion.identity) as GameObject;
            o.transform.parent = layer.transform;
            o.transform.localScale *= Scale;
            lastSpawnTime = now;
        }
    }
}
