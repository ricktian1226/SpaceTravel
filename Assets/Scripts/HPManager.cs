using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    private int HP;

    //上次减少时刻
    private float lastDecreaseTime = 0f;

    public static HPManager Instance { get; set; }

    // Use this for initialization
    void Start()
    {
        HP = SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.MaxLimit;
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        DecreaseHP();
    }

    public void AddHP(int value)
    {
        var MaxHP = SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.MaxLimit;
        HP += value;
        HP = HP > MaxHP ? MaxHP : HP;
        updateUI();
    }

    public void DecreaseHP()
    {
        var now = Time.time;
        if (lastDecreaseTime + SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.DecreaseInterval < now)
        {
            HP -= SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.DecreasePerTime;
            HP = HP < 0 ? 0 : HP;
            updateUI();
            lastDecreaseTime = now;
        }
    }

    void updateUI()
    {
        var label = SingletonMonoBehaviour<GameManager>.Instance.uiConfiguration.EnergyLabel;
        var bar = SingletonMonoBehaviour<GameManager>.Instance.uiConfiguration.EnergyForeBar;
        if (null != label)
        {
            var maxLimit = SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.MaxLimit;
            label.text = string.Format("{0}/{1}", HP, maxLimit);
            bar.fillAmount = (float)(HP) / (float)(maxLimit);
        }

        if (NoHP())
        {
            SingletonMonoBehaviour<GameManager>.Instance.GameReload();
        }
    }

    public bool HPMax()
    {
        return HP >= SingletonMonoBehaviour<GameManager>.Instance.energyConfiguration.MaxLimit;
    }

    public bool NoHP()
    {
        return HP <= 0;
    }

}
