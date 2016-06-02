using UnityEngine;
using System.Collections;

/// <summary>
/// 生产行为定义
/// </summary>
/*public class Spawner
{
    public static void DoSpawn(GameObject[] things, GameObject layer, ref float lastSpawnTime, Vector2 SpawnInteval)
    {
        if (things.Length <= 0)
        {
            return;
        }

        var now = Time.time;

        if (lastSpawnTime + Random.Range(SpawnInteval.x, SpawnInteval.y) < now)
        {
            var o = GameObject.Instantiate(things[Random.Range(0, things.Length)], new Vector3(Random.Range(-SingletonMonoBehaviour<GameManager>.Instance.XBoundary, SingletonMonoBehaviour<GameManager>.Instance.XBoundary), SingletonMonoBehaviour<GameManager>.Instance.YBoundary, 0f), Quaternion.identity) as GameObject;
            o.transform.parent = layer.transform;
            o.transform.localScale *= 0.3f;
            lastSpawnTime = now;
        }
    }
}*/

/*public class ObstacleManager : MonoBehaviour
{
    private float lastSpawnTime = 0;
    //private float lastSpawnResourceTime = 0;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        spawn();

        //spawnResource();
    }

    void spawn()
    {
        /*spawnSomething(SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.Obstacles, 
            SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.ObstacleLayer, 
            ref lastSpawnTime,
            SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.SpawnInteval);
        Spawner.DoSpawn(SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.Obstacles,
            SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.ObstacleLayer,
            ref lastSpawnTime,
            SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.SpawnInteval);
    }*/

    /*void spawnSomething(GameObject[] things, GameObject layer, ref float lastSpawnTime, Vector2 SpawnInteval)
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
            //o.transform.localScale *= Random.Range(0.1f, 1f);//设置随意大小
            o.transform.localScale *= 0.3f;
            lastSpawnTime = now;
        }
    }*/
//}
