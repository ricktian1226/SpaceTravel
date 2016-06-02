using UnityEngine;
using System.Collections;

public class ObstacleBehaviour : MonoBehaviour {

    public float Speed { get; set; }
    public float Angle { get; set; }

    // Use this for initialization
    void Start()
    {
        //获取随机速度和随机角度
        Speed =  Random.Range(SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.Speed.x, SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.Speed.y);
        Angle = Random.Range(SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.Angle.x, SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.Angle.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -SingletonMonoBehaviour<GameManager>.Instance.YBoundary)
        {
            Destroy(gameObject);
        }


        //判断是否intouching，如果是，则下落速度减半，并且，修改旋转角度
//         var touchingBehaviour = GetComponent<TouchBehaviour>();
//         bool inTouching = false;
//         if (null != touchingBehaviour && null != touchingBehaviour.Unit)
//         {
//             inTouching = touchingBehaviour.Unit.InTouching;
//         }

         float speed = Speed;
//          if (inTouching)
//          {
//             var relativePosition = (SingletonMonoBehaviour<PersonManager>.Instance.Person.transform.position - transform.position).normalized;
//             Quaternion rotation = Quaternion.LookRotation(Vector3.forward,relativePosition);
//             transform.rotation = rotation;
//             speed *= 0.2f;
//          }
//          else
//          {
             transform.Rotate(Vector3.forward * Angle);
//         }

        transform.Translate(speed * SingletonMonoBehaviour<GameManager>.Instance.GlobalSpeed * Time.deltaTime * Vector3.down,Space.World);
    }
}
