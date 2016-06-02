using UnityEngine;
using System.Collections;

public class BoundaryBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        onOutBoundary();
	}

    void onOutBoundary()
    {
        //var GameManager = SingletonMonoBehaviour<GameManager>;

        var xBoundary = SingletonMonoBehaviour<GameManager>.Instance.XBoundary;
        var yBoundary = SingletonMonoBehaviour<GameManager>.Instance.YBoundary;
        if (transform.position.x > xBoundary)
        {
            transform.position = resetXPosition(transform.position, -xBoundary * 2);
        }

        if (transform.position.x < -SingletonMonoBehaviour<GameManager>.Instance.XBoundary)
        {
            transform.position = resetXPosition(transform.position, xBoundary * 2);
        }

        if (transform.position.y > SingletonMonoBehaviour<GameManager>.Instance.YBoundary)
        {
            transform.position = resetYPosition(transform.position, -yBoundary * 2);
        }

        if (transform.position.y < -SingletonMonoBehaviour<GameManager>.Instance.YBoundary)
        {
            transform.position = resetYPosition(transform.position, yBoundary * 2);
        }
    }

    Vector3 resetXPosition(Vector3 curPosition, float offset)
    {
        var position = curPosition;
        position.x += offset;
        Debug.LogFormat("change {0} to {1}", curPosition, position);
        return position;
    }

    Vector3 resetYPosition(Vector3 curPosition, float offset)
    {
        var position = curPosition;
        position.y += offset;
        return position;
    }

}
