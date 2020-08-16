using UnityEngine;
using UnityEngine.UI;
public class TimerFiller : MonoBehaviour
{
    public Image fill;

    private float startTime;
    private float fullTime;

    public void StartTimer(float time)
    {
        Debug.Log("START");
        fullTime = time;
        startTime = Time.time;
        Debug.Log(startTime + "/" + fullTime);
        fill.fillAmount = 0f;
    }

    void Start()
    {
        startTime = -2f;    
    }

    void Update()
    {
        if (startTime > -1f)
        {
            if (Time.time - startTime >= fullTime)
            {
                fill.fillAmount = 0f;
                startTime = -2f;
            }
            else
            {
                fill.fillAmount = (Time.time - startTime) / fullTime;
            }
        }
    }
}
