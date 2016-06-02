using UnityEngine;
using System.Collections;

public class EnergyBehaviour : MonoBehaviour {
    [Tooltip("能量源速度")]
    public float Speed;

    [Tooltip("能量源价值")]
    public int Value;

	// Use this for initialization
	void Start () {
        Speed *= Random.Range(0.5f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}

    void move()
    {
        if (transform.position.y < -SingletonMonoBehaviour<GameManager>.Instance.YBoundary)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.down * Time.deltaTime * Speed * SingletonMonoBehaviour<GameManager>.Instance.GlobalSpeed, Space.World);
    }
}
