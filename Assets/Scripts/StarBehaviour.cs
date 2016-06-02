using UnityEngine;
using System.Collections;

public class StarBehaviour : MonoBehaviour {
    public float Speed { get; set; }

	// Use this for initialization
	void Start () {
        var random = Random.Range(0.05f, 0.30f);
        Speed = random;
        transform.localScale *= random;
        transform.position = new Vector3(Random.Range(-SingletonMonoBehaviour<GameManager>.Instance.XBoundary, SingletonMonoBehaviour<GameManager>.Instance.XBoundary), Random.Range(-SingletonMonoBehaviour<GameManager>.Instance.YBoundary, SingletonMonoBehaviour<GameManager>.Instance.YBoundary), 5f);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -SingletonMonoBehaviour<GameManager>.Instance.YBoundary)
        {
            Destroy(gameObject);
        }

        transform.Translate(Speed * SingletonMonoBehaviour<GameManager>.Instance.GlobalSpeed * Time.deltaTime * Vector3.down, Space.World);
	}
}
