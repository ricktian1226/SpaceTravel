using UnityEngine;
using System.Collections;

public class ObstacleGroupBehaviour : MonoBehaviour {
    [Tooltip("障碍组成员列表")]
    public GameObject[] Obstacles;

    [Tooltip("障碍组成员数目")]
    public int ItemCount;

    [Tooltip("移动速度")]
    public float Speed;

    [Tooltip("障碍组半径")]
    public float Radius;

	// Use this for initialization
	void Start () {

        if (Obstacles.Length > 0)
        {
            var length = Obstacles.Length;
            for (int i = 0; i < ItemCount; i++)
            {
                var b = Instantiate(Obstacles[Random.Range(0, length)]);
                b.transform.parent = transform;
                b.transform.localPosition = new Vector3(-Radius, 0f, 0f);
                b.transform.RotateAround(transform.position,Vector3.forward, 360 * i / ItemCount);
                b.transform.localScale = Vector3.one;
            }    
        }

        //把障碍组整体上移一个直径的距离，避免障碍组在可是范围内突然出现
        transform.position += new Vector3(0, 2*Radius, 0f);
	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * Speed);
        transform.Translate(Speed * SingletonMonoBehaviour<GameManager>.Instance.GlobalSpeed * Time.deltaTime * Vector3.down, Space.World);
	}
}
