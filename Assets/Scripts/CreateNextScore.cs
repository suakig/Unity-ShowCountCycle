using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateNextScore : MonoBehaviour {

	public float invokeTime = 1;
	public MinMaxSliderValue randRange;

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
