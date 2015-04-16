using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 次の目標値を決めるクラス
/// </summary>
public class CreateNextScore : MonoBehaviour {

    public float invokeTime = 1;        //何秒に一回呼ぶか
    public MinMaxSliderValue randRange; //乱数範囲設定

    private ShowCountCycle score;
    private Text nextScore;

    void Start () {
        score = transform.FindChild ("Score").GetComponent<ShowCountCycle>();
        nextScore =  transform.FindChild ("NextScore").GetComponent<Text>();
        Run ();
    }

    void Run ()
    {
        int rand = Random.Range ((int)randRange.minValue, (int)randRange.maxValue);
        nextScore.text = "NextScore " + rand.ToString();
        score.GetNext (rand);
        Invoke ("Run", invokeTime);
    }
}
