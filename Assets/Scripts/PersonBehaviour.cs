using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum PersonState
{
    UNKOWN,
    IDLE,
    POWERING,//蓄能
    MOVING,
}

public class PersonBehaviour : MonoBehaviour
{
    private GameObject progressBar;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 lastTouchPosition;
    //正在接触的物体
    private List<GameObject> touchings = new List<GameObject>();

    private float lastStationaryTime;

    private PersonState State { set; get; }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Idle();

        //生成血条
//         progressBar = Instantiate(Resources.Load("Image")) as GameObject;
//         if(null == progressBar)
//         {
//             Debug.LogError("progressBar is null");
//         }
        //progressBar.transform.parent = transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        moveByTouch();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Person.OnCollisionEnter2D");
        switch (collision.gameObject.tag)
        {
            case TAG.ENERGY://如果是能量块，则
                SingletonMonoBehaviour<HPManager>.Instance.AddHP(collision.gameObject.GetComponent<EnergyBehaviour>().Value);
                Destroy(collision.gameObject);
                break;
            case TAG.OBSTACLE:
                //GameManager.Instance.GameOver();
                SingletonMonoBehaviour<GameManager>.Instance.GameReload();
                break;
        }
    }

//     void OnTriggerEnter2D(Collision2D collision)
//     {
//         switch (collision.gameObject.tag)
//         {
//             case TAG.ENERGY://如果是能量块，则
//                 SingletonMonoBehaviour<HPManager>.Instance.AddHP(collision.gameObject.GetComponent<EnergyBehaviour>().Value);
//                 Destroy(collision.gameObject);
//                 break;
//             case TAG.OBSTACLE:
//                 //GameManager.Instance.GameOver();
//                 SingletonMonoBehaviour<GameManager>.Instance.GameReload();
//                 break;
//         }
//     }

    public void Idle()
    {
        animator.Play("person_idle");
    }

    void move()
    {
        moveByMouse();
        moveByTouch();
    }

    void moveByGravity()
    {
        var Force = SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Force;
        //手机重力感应
        if (Input.acceleration.x != 0 || Input.acceleration.y != 0)
        {
            var xForce = Input.acceleration.x * Time.deltaTime * Force * 3;
            var yForce = Input.acceleration.y * Time.deltaTime * Force * 3;
            transform.position += new Vector3(xForce, yForce, 0f);
            transform.position.Normalize();
            //moving = true;
        }

    }

    void moveByTouch()
    {
        if (Input.touchCount > 0)// && touch.phase == TouchPhase.Moved)
        {
            var touch = Input.GetTouch(0);
            switch(Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    //lastTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    //resetMask();
                    lastTouchPosition = TouchPosition(touch);
                    animator.Play(PERSONANIMATION.MOVE);
                    break;
                case TouchPhase.Moved:
                    //resetMask();
                    //setState(PersonState.MOVING);
                    animator.Play(PERSONANIMATION.MOVE);
                    var Force = SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Force;
                    var curTouchPosition = TouchPosition(touch);
                    //var direction = ((Vector2)curTouchPosition - lastTouchPosition).normalized;
                    //transform.Translate(direction * Time.deltaTime * Force, Space.World);
                    transform.position += curTouchPosition - lastTouchPosition;
                    lastTouchPosition = curTouchPosition;
                    break;
                case TouchPhase.Stationary:
                    //setState(PersonState.POWERING);
                    //蓄能，速度降低，光圈加大
                    //extendMask();
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    //setState(PersonState.IDLE);
                    //resetMask();
                    animator.Play(PERSONANIMATION.IDLE);
                    break;
            }

            return;

            //ToDelete 以下使用Touch的代码，会出现移动时抖动不平滑的问题
            /*Vector2 touchDeltaPosition = touch.deltaPosition.normalized;
            //var dt = Time.deltaTime / touch.deltaTime;
            //touchDeltaPosition *= dt;
            //Vector2 touchDeltaPosition = Camera.main.ScreenToWorldPoint(touch.position);
            transform.Translate(touchDeltaPosition.x * Force.x * Time.smoothDeltaTime, touchDeltaPosition.y* Force.y * Time.smoothDeltaTime, 0f, Space.World);*/
        }
    }


    Vector3 TouchPosition(Touch touch)
    {
        var touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        return new Vector3(touchPosition.x, touchPosition.y, 0f);
    }

    void checkMask()
    {
        switch (State)
        {
            case PersonState.IDLE:
            case PersonState.MOVING:
                resetMask();
                break;
            case PersonState.POWERING:
                var now = Time.time;
                var extendTime = SingletonMonoBehaviour<PersonManager>.Instance.MeshMask.GetComponent<MeshMaskBehaviour>().ExtendTime;
                if (lastStationaryTime + extendTime <= now)
                {
                    extendMask();
                    lastStationaryTime = now;
                }
                break;
        }
    }


    void extendMask()
    {
        SingletonMonoBehaviour<PersonManager>.Instance.MeshMask.GetComponent<MeshMaskBehaviour>().ExtendRadius();
    }

    void resetMask()
    {
        SingletonMonoBehaviour<PersonManager>.Instance.MeshMask.GetComponent<MeshMaskBehaviour>().ResetRadius();
    }

    void setState(PersonState state)
    {
        State = state;
    }

    void moveByMouse()
    {
        var Force = SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Force;
        //bool moving = false;
        //键盘控制
        float xForce = Input.GetAxis("Horizontal") /** Time.deltaTime */* Force;
        float yForce = Input.GetAxis("Vertical") /** Time.deltaTime*/ * Force;
        if (xForce != 0 || yForce != 0)
        {
            //transform.position += new Vector3(xForce, yForce, 0f);
            transform.Translate(xForce, yForce, 0f, Space.World);
            Debug.LogFormat("moveByMouse : {0}", transform.position);
            //moving = true;
        }
    }

}
