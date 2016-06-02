using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnergyConfiguration
{
    [Tooltip("能量源层")]
    public GameObject EnergyLayer;
    [Tooltip("能量源对象列表")]
    public GameObject[] Energys;
    [Tooltip("能量源生成时间间隔")]
    public float SpawnInterval;
    [Tooltip("能量源最大值")]
    public int MaxLimit;
    [Tooltip("每次减少能量值")]
    public int DecreasePerTime;
    [Tooltip("减少间隔")]
    public int DecreaseInterval;
}

[System.Serializable]
public class RocketConfiguration
{
    [Tooltip("火箭移动速度")]
    public float Speed;
}

[System.Serializable]
public class UIConfiguration
{
    [Tooltip("restart按钮")]
    public Button RestartButton;

    [Tooltip("能量条")]
    public Image EnergyForeBar;

    [Tooltip("能量标签")]
    public Text EnergyLabel;

    [Tooltip("分数标签")]
    public Text ScoreLabel;

    [Tooltip("移动灵敏度调节滑块")]
    public Scrollbar ForceScrollBar;

    [Tooltip("移动灵敏度数值")]
    public InputField InputForceValue;

    [Tooltip("设置按钮")]
    public Button SettingButton;
}

[System.Serializable]
public class HPConfiguration
{
    [Tooltip("每次降低的HP值")]
    public int DecreaseValuePerTime;

    [Tooltip("HP降低间隔")]
    public float DecreaseInterval;
}

[System.Serializable]
public class PersonConfiguration
{
    [Tooltip("移动速度")]
    public float Force;

    [Tooltip("主角所在层")]
    public GameObject PersonLayer;

    [Tooltip("接触半径")]
    public float Radius;

    [Tooltip("主角缩放比例")]
    public float Scale;

    private GameObject person;
    public GameObject Person
    {
        set
        {
            person = value;
        }

        get
        {
            if (null == person)
            {
                person = Resources.Load("Person") as GameObject;
            }
            return person;
        }
    }
}

[System.Serializable]
public class ObstacleConfiguration
{
    [Tooltip("障碍列表")]
    public GameObject[] Obstacles;

    [Tooltip("障碍对象层")]
    public GameObject ObstacleLayer;

    [Tooltip("连接器对象层")]
    public GameObject ConnectorLayer;

    [Tooltip("生成时间间隔")]
    public Vector2 SpawnInteval;

    [Tooltip("障碍运动速度范围")]
    public Vector2 Speed;

    [Tooltip("障碍旋转角度范围")]
    public Vector2 Angle;
}

[System.Serializable]
public class ResourceConfiguration
{
    public GameObject[] Resources;

    public GameObject ResourceLayer;

    public Vector2 SpawnInteval;
}

[System.Serializable]
public class StarConfiguration
{
    public GameObject[] Stars;

    public GameObject StarLayer;

    public Vector2 SpawnInteval;
}

public class GameManager : MonoBehaviour {

    [Tooltip("UI配置信息")]
    public UIConfiguration uiConfiguration;

    [Tooltip("能量源配置信息")]
    public EnergyConfiguration energyConfiguration;

    [Tooltip("HP配置信息")]
    public HPConfiguration hpConfiguration;

    [Tooltip("火箭配置信息")]
    public RocketConfiguration rocketConfiguration;

    [Tooltip("人物配置信息")]
    public PersonConfiguration personConfiguration;

    [Tooltip("障碍物配置信息")]
    public ObstacleConfiguration obstacleConfiguration;

    [Tooltip("资源配置信息")]
    public ResourceConfiguration resourceConfiguration;

    [Tooltip("资源配置信息")]
    public StarConfiguration starConfiguration;

    [Tooltip("移动速度加速值")]
    public float SpeedUpValue = 3f;

    [Tooltip("默认速度值")]
    public float DefaultSpeedValue = 1f;

    public float GlobalSpeed { get; set; }

    public float XBoundary { get; set; }
    public float YBoundary { get; set; }

    public bool PersonOn = true;

    //public static GameManager Instance { get; set; }

    void Awake()
    {
//         Debug.Log("GameManager.Awake");
// 
//         if (null == Instance)
//         {
//             Instance = this;
//         }
//         else if (Instance != this)
//         {//保证只有一个实例存在
//             Destroy(gameObject);
//         }
// 
//         //在reload scene的时候不destory
//         DontDestroyOnLoad(gameObject);

        //获取屏幕的x轴和y轴的大小
        YBoundary = Camera.main.orthographicSize;
        XBoundary = Screen.width * YBoundary / Screen.height;

        Time.timeScale = 1;

        GlobalSpeed = DefaultSpeedValue;

        //personConfiguration.Person =  Resources.Load("Person") as GameObject;
    }

	// Use this for initialization
	void Start () {
        uiConfiguration.InputForceValue.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        //personConfiguration.Force = new Vector2(uiConfiguration.ForceScrollBar.value, uiConfiguration.ForceScrollBar.value);
	}

    public void GameOver()
    {
        //uiConfiguration.RestartButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameReload()
    {
        //Debug.Log("GameManager.GameReload");
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScenePersonAndRocket");
    }

    public void ForceChanging()
    {
        Time.timeScale = 0;
        float value;
        if(float.TryParse(uiConfiguration.InputForceValue.text, out value))
        {
            personConfiguration.Force = value;//new Vector2(value, value);
        }
    }

    public void ForceChanged()
    {
        Time.timeScale = 1;
        uiConfiguration.SettingButton.gameObject.SetActive(true);
        uiConfiguration.InputForceValue.gameObject.SetActive(false);
    }

    public void Setting()
    {
        Time.timeScale = 0;
        uiConfiguration.SettingButton.gameObject.SetActive(false);
        uiConfiguration.InputForceValue.gameObject.SetActive(true);
    }
}
