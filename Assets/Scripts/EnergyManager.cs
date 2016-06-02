using UnityEngine;
using System.Collections;

    public class EnergyManager : MonoBehaviour
    {

        /*[Tooltip("能量源对象列表")]
        public GameObject[] Energys;

        [Tooltip("生成能量源时间间隔")]
        public float SpawnInterval;

        [Tooltip("能量源层")]
        public GameObject EnergyLayer;*/

        //上次生成能量源时间
        private float lastSpawnTime = 0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var now = Time.time;
            if (now > SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.SpawnInterval + lastSpawnTime)
            {
                spawn();
                lastSpawnTime = now;
            }
        }

        void spawn()
        {
            var Energys = SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.Energys;
            var length = Energys.Length;
            if (length > 0)
            {
                var energy = Instantiate(Energys[Random.Range(0, length)]);
                energy.transform.parent = SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.EnergyLayer.transform;
                var position = energy.transform.position;
                position.y = 6f;
                position.x = Random.Range(-SingletonMonoBehaviour<GameManager>.Instance.XBoundary, SingletonMonoBehaviour<GameManager>.Instance.XBoundary);
                energy.transform.position = position;
            }
        }
    }
