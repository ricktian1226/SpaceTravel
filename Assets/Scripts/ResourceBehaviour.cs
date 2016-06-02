using UnityEngine;
using System.Collections;

public class ResourceBehaviour : MonoBehaviour {

    public float Speed { get; set; }

    // Use this for initialization
    void Start()
    {
        var random = Random.Range(0.2f, 0.8f);
        Speed = random;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -SingletonMonoBehaviour<GameManager>.Instance.YBoundary)
        {
            Destroy(gameObject);
        }

        transform.Translate(Speed * SingletonMonoBehaviour<GameManager>.Instance.GlobalSpeed * Time.deltaTime * Vector3.down, Space.World);
    }
}
