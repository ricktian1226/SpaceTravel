using UnityEngine;
using System.Collections;

public class MeshMaskBehaviour : MonoBehaviour {

    [Tooltip("Player对象")]
    public GameObject Player;

    [Tooltip("光照半径")]
    public float MinRadius;

    [Tooltip("光照半径上限")]
    public float MaxRadius;

    [Tooltip("光照半径扩展蓄能时间")]
    public float ExtendTime;

    [Tooltip("光照质量，数值越低，向四周辐射的射线越多，效果越好，但是性能越低")]
    public int LevelOfDetail;

    private Vector3 direction;
    private int index;
    private int triIndex = 0;
    private int lod = 1;
    private float width;
    private GameObject go;
    private GameObject pGo;
    private Mesh mesh;
    private Vector3 worldPos;

    private Vector3[] verts;
    private int[] tris;
    private Vector2[] uvs;
    private GameObject didHit;

    private int mask;
    private float Radius { get; set; }//当前的遮罩半径值

    // Use this for initialization
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        lod = LevelOfDetail;
        Radius = MinRadius;

        //如果lod=1，则共发射360条射线，共有360/lod个定点加一个圆心
        verts = new Vector3[(360 / lod) + 1];

        //每两个顶点和圆心组成一个三角形，所以三角形数为定点数乘以3
        tris = new int[(360 / lod) * 3];

        //定点数
        uvs = new Vector2[verts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        index = 0;
        triIndex = 0;
        worldPos = SingletonMonoBehaviour<PersonManager>.Instance.Person.transform.position;
        //worldPos = new Vector3(0f,0f,0f);
        verts[index] = worldPos;
        index++;

        for (var a = 0; a < 360; a += lod)
        {
            var direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * a), Mathf.Cos(Mathf.Deg2Rad * a), 0f);
            direction *= Radius;

            RaycastHit2D hit = Physics2D.Raycast(worldPos, direction, Radius, 1 << LayerMask.NameToLayer("Obstacle"));
            if (null != hit.collider)
            {
                //如果探测到，将探测到的点作为网格的点
                verts[index] = new Vector3(hit.point.x, hit.point.y, 0f);
            }
            else
            {
                //否则，用射线的末端作为网格的点
                verts[index] = new Vector3(direction.x + worldPos.x, direction.y + worldPos.y, 0f);
            }
            index++;
        }

       /* for (var i = 1; i < (360 / lod); i++)
        {
            Debug.DrawLine(verts[0], verts[i], Color.green);
        }*/


        //根据网格定点组合三角形
        for (var i = 1; i < (360 / lod); i++)
        {
            tris[triIndex] = 0;
            tris[triIndex + 1] = i;
            tris[triIndex + 2] = i + 1;
            triIndex += 3;
        }

        tris[((360 / lod) * 3) - 3] = 0;
        tris[((360 / lod) * 3) - 2] = 360 / lod;
        tris[((360 / lod) * 3) - 1] = 1;

        //网格贴图
        //         int j = 0;
        //         while(j < uvs.Length){
        //             uvs[j] = new Vector2(verts[j].x, verts[j].z);
        //             j++;
        //         }

        //重新组合网格
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        //mesh.uv = uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

    }

    /// <summary>
    /// 扩大光照半径
    /// </summary>
    public void ExtendRadius()
    {
        Radius = MaxRadius;
    }

    /// <summary>
    /// 复原光照半径
    /// </summary>
    public void ResetRadius()
    {
        Radius = MinRadius;
    }
}
