using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawer
{
    private LineRenderer lineRenderer;
    private Transform begin;
    private Transform end;
    public LineDrawer(Transform _begin, Transform _end, LineRenderer l)
    {
        lineRenderer = l;
        begin = _begin;
        end = _end;
    }

    public void Draw()
    {
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPositions(new Vector3[] { begin.position, end.position});
    }

    public void Clear()
    {
        //lineRenderer.SetPositions(new Vector3[]{});
        lineRenderer.SetVertexCount(0);
    }
}

/// <summary>
/// 基础的单位相关信息定义
/// </summary>
public class TouchingUnit
{
    private GameObject obj;
    private GameObject _connector = null;
    public GameObject Connector 
    {
        set 
        {
            _connector = value;
        }
        get 
        {
            if (null == _connector)
            {
                _connector = GameObject.Instantiate(Resources.Load("Connector")) as GameObject;
                _connector.transform.parent = SingletonMonoBehaviour<GameManager>.Instance.obstacleConfiguration.ConnectorLayer.transform;
                _connector.transform.position = Vector3.forward;
            }
            return _connector;
        }
    }
    private Color objColor;
    private float timeLimit;//到达奖励时间
    private int value;//到达奖励时间
    //private LineDrawer lineDraw;//画线工具
    private float beginTime;//当前累计时间
    public bool InTouching { set; get; }

    public TouchingUnit() 
    { 
    }

    public TouchingUnit(float _stayTimeLimit, int _value, LineRenderer lineRenderer, Transform begin, Transform end, GameObject o)
    {
        timeLimit = _stayTimeLimit;
        value = _value;
        //lineDraw = new LineDrawer(begin, end, lineRenderer);
        obj = o;
        objColor = obj.GetComponent<SpriteRenderer>().color;
        
    }

    /// <summary>
    /// 校验，返回是否完成接触
    /// </summary>
    /// <returns>true 完成接触，false 未完成接触</returns>
    public bool Check()
    {
        var now = Time.time;
        if (InTouching)
        {
            if (beginTime + timeLimit <= now)//达到指定时间
            {
                showFont();
                SingletonMonoBehaviour<ScoreManager>.Instance.Add(value);
                DisTouching();
                Debug.LogFormat("{0} Touching end", obj);
                //销毁对象
                GameObject.Destroy(obj);

                return true;
            }
            return false;
        }
        else//开始接触
        {
            Debug.LogFormat("{0} Begin to touch", obj);
            InTouching = true;//设置状态
            beginTime = now;//设置开始时间
            obj.GetComponent<SpriteRenderer>().color = Color.green;
            return false;
        }
    }

    public void showFont()
    {
        //var f = GameObject.Instantiate(Resources.Load("FontNice"), obj.transform.position, Quaternion.identity);
        var f = GameObject.Instantiate(Resources.Load("FontNice"));
        GameObject.Destroy(f, 3f);
    }

    public void DisTouching()
    {
        Debug.LogFormat("{0} Touching end", obj);
        InTouching = false;//设置状态为接触结束
        //Connector.SetActive(false);
        GameObject.Destroy(Connector);
        if (null != obj)
        {
            switch (obj.tag)
            {
                case TAG.RESOURCE:
//                     GameObject.Destroy(obj);
//                     break;
                case TAG.OBSTACLE:
                    obj.GetComponent<SpriteRenderer>().color = objColor;
                    break;
            }
        }
    }


    /// <summary>
    /// 根据位置刷新连接器的
    /// </summary>
    /// <param name="position"></param>
    public void RefreshConnector(Vector3 target)
    {
        //设置位置，为障碍物和person之间的中点
        Connector.transform.position = (target + obj.transform.position) * 0.5f;

        //设置长度
        var factor = Vector2.Distance(target, obj.transform.position)  / 4f;//4f是Connector原始长度
        var scale = Connector.transform.localScale;
        scale.y = (4f / Vector2.Distance(target, obj.transform.position)) * 0.1f;
        scale.x = Vector2.Distance(target, obj.transform.position) / 4f; 
        Connector.transform.localScale = scale;
        //Debug.LogFormat("Distance {0}, originalSize {1}, factor {2} localScale {3}", Vector3.Distance(target, obj.transform.position), 4f, factor, Connector.transform.localScale);

        //设置角度
        var relativePosition = (target - obj.transform.position).normalized;
        Connector.transform.rotation = Quaternion.LookRotation(Vector3.forward, relativePosition);
        Connector.transform.RotateAround(Connector.transform.position, Vector3.forward, 90f);
    }
}

public class EffectiveBehaviour : MonoBehaviour {

    private Dictionary<GameObject, TouchingUnit> dictionaryObject2TouchingUnit = new Dictionary<GameObject,TouchingUnit>();

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        check();
	}

    /// <summary>
    /// 检测资源信息
    /// </summary>
    void check()
    {
        var collisions = Physics2D.OverlapCircleAll((Vector2)(gameObject.transform.position), SingletonMonoBehaviour<GameManager>.Instance.personConfiguration.Radius, LayerMask.GetMask(LAYERNAME.RESOURCE));
        Dictionary<GameObject, TouchingUnit> tmpDictionary = new Dictionary<GameObject, TouchingUnit>();
        var touchingUnit = new TouchingUnit();
        if (collisions.Length > 0)
        {
            foreach (var c in collisions)
            {
                switch(c.gameObject.tag)
                {
                    //case TAG.OBSTACLE:
                    case TAG.RESOURCE:
                        //如果不存在这个对象就加一个
                        if (!dictionaryObject2TouchingUnit.TryGetValue(c.gameObject, out touchingUnit))
                        {
                            var touchBehaviour = c.gameObject.GetComponent<TouchBehaviour>();
                            if (null != touchBehaviour)
                            {
                                var touchingTime = touchBehaviour.KeepTouchingTime;
                                var touchingValue = touchBehaviour.Value;
                                var lineRenderer = c.gameObject.GetComponent<LineRenderer>();
                                touchingUnit = new TouchingUnit(touchingTime, touchingValue, lineRenderer, c.gameObject.transform, transform, c.gameObject);
                                touchBehaviour.Unit = touchingUnit;//建立引用
                            }
                        }

                        //将未完成的插入临时列表中
                        if (!touchingUnit.Check())
                        {
                            tmpDictionary.Add(c.gameObject, touchingUnit);
                            touchingUnit.RefreshConnector(transform.position);
                        }

                        break;
                }
            }

        }

        //校验一遍，如果没有在现存的intouching列表中。需要恢复原状
        foreach(var t in dictionaryObject2TouchingUnit)
        {
            if (!tmpDictionary.TryGetValue(t.Key, out touchingUnit))
            {
                t.Value.DisTouching();
                if (null != t.Key)
                {
                    t.Key.GetComponent<TouchBehaviour>().Unit = null;
                }
            }
        }

        //重新赋值
        dictionaryObject2TouchingUnit = tmpDictionary;
    }


}
