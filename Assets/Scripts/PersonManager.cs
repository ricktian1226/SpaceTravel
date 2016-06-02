using UnityEngine;
using System.Collections;

public class PersonManager : MonoBehaviour {
    [Tooltip("遮罩对象")]//为了修改遮罩半径，需要引用遮罩对象
    public GameObject MeshMask;

    private Animator personAnimator;
    private Rigidbody2D personRigidBody;
    private GameObject person;
    public GameObject Person { 
        set{person = value;}
        get 
        { 
            if(null == person){
                person = Instantiate(SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Person);
            }
            return person;
        }
    }

    public PersonManager()
    {

    }

    // Use this for initialization
    void Start()
    {
        /*lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(2);*/

        //Person = Instantiate(GameManager.Instance.personConfiguration.Person);
        //person.transform.parent = GameManager.Instance.personConfiguration.PersonLayer.transform;

        personAnimator = Person.GetComponent<Animator>();
        personRigidBody = Person.GetComponent<Rigidbody2D>();
        var scale = SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Scale;
        Person.transform.localScale = new Vector3(scale, scale, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //move();
    }

 /*   void move()
    {
        movePerson();
    }

    void movePerson()
    {
        Rigidbody2D rigidBody = personRigidBody;
        var Force = SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Force;

        if (null != rigidBody)
        {

            bool moving = false;
            //键盘控制
            if (Input.GetKeyDown("left"))
            {
                rigidBody.AddForce(new Vector2(-Force, 0f));
                moving = true;
            }

            if (Input.GetKeyDown("right"))
            {
                rigidBody.AddForce(new Vector2(Force, 0f));
                moving = true;
            }

            if (Input.GetKeyDown("up"))
            {
                rigidBody.AddForce(new Vector2(0f, Force.y));
                moving = true;
            }

            if (Input.GetKeyDown("down"))
            {
                rigidBody.AddForce(new Vector2(0f, -Force.y));
                moving = true;
            }

            //手机重力感应
            if (Input.acceleration.x != 0 || Input.acceleration.y != 0)
            {
                rigidBody.AddForce((new Vector2(Input.acceleration.x * Force.x, Input.acceleration.y * Force.y)));
                moving = true;
            }
        }
    }*/

}
