using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int[] lvlValues = new[] { 1, 2, 3 };

    public IntVariable score;
    public IntVariable speed;
    public IntVariable level;

    public float timeLeft;

    public Text timeTxt;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        score.value = 0;
        speed.value = 3;
        level.value = 1;
    }

    private void FixedUpdate()
    {
        //Debug.Log(score.value);

        timeLeft -= Time.deltaTime;
        timeTxt.text = timeLeft.ToString();

        if (timeLeft <= 0)
        {
            LevelOver();
        }

    }

    public void LevelOver()
    {
        if (level.value == 3)
        {
            //GameOver();
        }

        else
        {
            speed.value+=3;
            level.value++;
        }
    }

    public void IncreaseScore()
    {
        score.value += 1;

    }

    public void DecreaseSCore()
    {
        if (score.value == 0)
        {

            return;
        }
        else
            score.value -= 1;

    }

}
