using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //private int[] lvlValues = new[] { 1, 2, 3 };

    public IntVariable score;
    public IntVariable speed;
    public IntVariable level;

    [HideInInspector]
    public float timeLeft;
    public float levelTime;

    public Text timeTxt;
    public Text ScoreTxt;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SettingInitialValues();
    }

    private void FixedUpdate()
    {
        Debug.Log(score.value);
        Debug.Log(level.value);

        timeLeft -= Time.deltaTime;
        timeTxt.text = timeLeft.ToString();

        if (timeLeft <= 0)
        {
            LevelOver();
        }
    }

    public void LevelOver()
    {
        //show score screen

        if (level.value == 3)
        {
            GameOver();
        }

        else
        {
            timeLeft = levelTime;
            speed.value += 3;
            level.value++;
        }
    }

    public void GameOver()
    {
        //show gameover screen
        //there will be a restart button -- m3 mona isa.
        SettingInitialValues();
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

    public void SettingInitialValues()
    {
        score.value = 0;
        speed.value = 3;
        level.value = 1;
        timeLeft = levelTime;
    }
}
