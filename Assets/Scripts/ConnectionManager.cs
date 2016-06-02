using UnityEngine;
using System.Collections;


public class ConnectionManager : MonoBehaviour {
    public GameObject Person;
    public GameObject Rocket;
    public Vector2 Force;
    public GameObject CameraManager;

    private LineRenderer lineRenderer;

    private Animator personAnimator;
    private Animator rocketAnimator;

    private Rigidbody2D personRigidBody;
    private Rigidbody2D rocketRigidBody;

    //bool personOn = true;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(2);

        Person = SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Person;
        personAnimator = Person.GetComponent<Animator>();
        rocketAnimator = Person.GetComponent<Animator>();

        personRigidBody = Person.GetComponent<Rigidbody2D>();
        rocketRigidBody = Rocket.GetComponent<Rigidbody2D>();

        Person.transform.localScale = new Vector3(0.3f, 0.3f, 1f);

        Rocket.transform.localScale = new Vector3(1f, 1f, 1f);
        Rocket.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        Rocket.transform.localPosition = Vector3.zero;

        //GameManager.Instance.PersonOn = true;
        Person.SetActive(true);
        Rocket.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
        //判断是否需要切换角色
        switchCharacter();

        move();

        //画线
        //drawLine();
	}

    void drawLine()
    {
        lineRenderer.SetPosition(0, Person.transform.position);
        lineRenderer.SetPosition(1, Rocket.transform.position);
    }

    void move()
    {
        if (SingletonMonoBehaviour<GameManager>.Instance.PersonOn)
        {
            movePerson();
        }
        else
        {
            moveRocket();
        }
    }

    void movePerson()
    {
        //Rigidbody2D rigidBody = personOn? personRigidBody:rocketRigidBody;
        Rigidbody2D rigidBody = personRigidBody;

        if (null != rigidBody)
        {

            bool moving = false;
            //键盘控制
            if (Input.GetKeyDown("left"))
            {
                rigidBody.AddForce(new Vector2(-Force.x, 0f));
                moving = true;
            }

            if (Input.GetKeyDown("right"))
            {
                rigidBody.AddForce(new Vector2(Force.x, 0f));
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
                rigidBody.AddForce(new Vector2(Input.acceleration.x * Force.x, Input.acceleration.y * Force.y));
                moving = true;
            }

            if (moving)
            {
                //rigidBody.mass = 1.5f;
                //rigidBody.gravityScale = 1;

                //animator.Play("person_move");
                //animator.Play("person_idle");
            }
        }
    }

    void moveRocket()
    {
        bool moving = false;
        var h = Input.GetAxis("Horizontal");
        if (h != 0)
        {
            var moveDirection = new Vector3(h * SingletonMonoBehaviour<GameManager>.Instance.rocketConfiguration.Speed, 0f, 0f);
            moveDirection = transform.TransformDirection(moveDirection);
            Rocket.transform.Translate(moveDirection, Space.World);
        }

        //手机重力感应
        if (Input.acceleration.x != 0)
        {
            Rocket.transform.Translate(Input.acceleration.x * SingletonMonoBehaviour<GameManager>.Instance.rocketConfiguration.Speed, 0f, 0f, Space.World);
            moving = true;
        }

    }

    void switchCharacter()
    {
        bool personOn = SingletonMonoBehaviour<GameManager>.Instance.PersonOn;
        if (personOn && HPManager.Instance.HPMax())
        {
            SingletonMonoBehaviour<GameManager>.Instance.PersonOn = false;
            Person.SetActive(false);
            Rocket.SetActive(true);
            SingletonMonoBehaviour<GameManager>.Instance.GlobalSpeed *= SingletonMonoBehaviour<GameManager>.Instance.SpeedUpValue;
        }
        else if (!personOn && HPManager.Instance.NoHP())
        {
            SingletonMonoBehaviour<GameManager>.Instance.PersonOn = true;
            Person.SetActive(true); 
            Rocket.SetActive(false);
            SingletonMonoBehaviour<GameManager>.Instance.GlobalSpeed = SingletonMonoBehaviour<GameManager>.Instance.DefaultSpeedValue;
        }
    }
}


