using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class VisualCueManager : MonoBehaviour
{
    [SerializeField] GameObject GreenSphere;
    [SerializeField] GameObject BlackSphere;
    [SerializeField] TextMeshProUGUI ResultUI;
    [SerializeField] float min_start = 0.6f;
    [SerializeField] float max_start = 1.5f;
    [SerializeField] float init_ds = 0.1f;
    [SerializeField] float intervals = 4f;
    private float starttime = 0;
    private float nowtime = 0;
    private float intervaltime = 0;
    private float Entertime = 0;
    private float Atime = 0;
    private bool isPressedEnterbutton = false;
    private bool isPressedAbutton = false;
    private float ds = 0;
    private bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        GreenSphere.SetActive(false);
        BlackSphere.SetActive(false);
        ResultUI.text = " ";
        starttime = Random.Range(min_start, max_start) + 2;
        ds = init_ds;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) isStart = true;

        if (isStart)
        {
            nowtime += Time.deltaTime;


            if (nowtime < 2)
            {

            }
            else if (nowtime >= 2 && nowtime < starttime)
            {
                BlackSphere.SetActive(true);
            }
            else if (nowtime >= starttime && !(isPressedEnterbutton && isPressedAbutton))
            {
                GreenSphere.SetActive(true);
                UpdateTaskParameter();
            }
            else if (intervaltime < intervals)
            {
                ResultUI.text = checkResult() ? "Succeed!!" : "Failed!!";
                intervaltime += Time.deltaTime;
                GreenSphere.SetActive(false);
                BlackSphere.SetActive(false);
            }
            else
            {
                resetSetting();
            }

        }

    }

    private void UpdateTaskParameter()
    {
        if (!isPressedEnterbutton) Entertime += Time.deltaTime;
        if (!isPressedAbutton) Atime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Return)) isPressedEnterbutton = true;
        if (Input.GetKeyDown(KeyCode.A)) isPressedAbutton = true;
    }

    private bool checkResult()
    {
        return (Mathf.Abs(Entertime - Atime) <= ds);
    }
    private void resetSetting()
    {
        isPressedEnterbutton = false;
        isPressedAbutton = false;
        GreenSphere.SetActive(false);
        BlackSphere.SetActive(false);
        ResultUI.text = " ";
        starttime = Random.Range(min_start, max_start) + 2;
        nowtime = 0;
        intervaltime = 0;
        ds = (Entertime + Atime) / 8;
        Entertime = 0;
        Atime = 0;
    }
}
