using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public IntVariable score;
    public IntVariable speed;
    public IntVariable level;

    [HideInInspector]
    public float timeLeft;
    public float levelTime;

    public GameObject WorkingGirl;
    public GameObject ScoreBackground;
    public GameObject ScoreGirl;

    public Text timeTxt;
    public Text ScoreTxt;

    public List<GameObject> glow = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        SettingInitialValues();
    }

    private void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        timeTxt.text = timeLeft.ToString("F");
        ScoreTxt.text = score.value.ToString();

        if (timeLeft <= 0.0f)
        {
            timeLeft = 0.0f;
            LevelOver();
        }
    }

    public void LevelOver()
    {
        //show score screen
        ShowScoreProcedure();

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

    public void ShowScoreProcedure()
    {
        WorkingGirl.SetActive(false);
        ScoreBackground.SetActive(true);
        ScoreGirl.SetActive(true);
    }

    public void HideScoreProcedure()
    {
        WorkingGirl.SetActive(true);
        ScoreBackground.SetActive(false);
        ScoreGirl.SetActive(false);
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
