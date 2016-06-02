using UnityEngine;
using System.Collections;

public class TouchBehaviour : MonoBehaviour {

    [Tooltip("保持接触时间")]
    public float KeepTouchingTime;

    [Tooltip("收集价值")]
    public int Value;

    public TouchingUnit Unit{ get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
