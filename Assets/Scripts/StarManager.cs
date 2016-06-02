using UnityEngine;
using System.Collections;

public class StarManager : MonoBehaviour {

    private float lastSpawnTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        spawn();
	}

    void spawn()
    {
        var starConfiguration = SingletonMonoBehaviour<GameManager>.Instance.starConfiguration;
        if (starConfiguration.Stars.Length > 0)
        {
            var now = Time.time;
            var SpawnInteval = starConfiguration.SpawnInteval;
            if (lastSpawnTime + Random.Range(SpawnInteval.x, SpawnInteval.y) < now)
            {
                var Stars = starConfiguration.Stars;
                var star = Instantiate(Stars[Random.Range(0, Stars.Length)]);
                star.transform.parent = starConfiguration.StarLayer.transform;
                lastSpawnTime = now;
            }
        }
    }
}
