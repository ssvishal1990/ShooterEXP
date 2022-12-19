using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    [SerializeField] TextMeshProUGUI scoreField;
    [SerializeField] TextMeshProUGUI specialField;
    [SerializeField] TextMeshProUGUI currentWaveNumberRepresentation;
    [SerializeField] int scoreValue = 0;
    [SerializeField] int hitScore = 10;
    [SerializeField] int killValue = 50;
    [SerializeField] float waitTimeBeforeFading = 0.5f;



    bool specialReady;

    protected int cutOffForSpecialLaunch = 500;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1 unit action system  " + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }
        instance = this;
        scoreValue = 0;
        specialReady = false;
        scoreField.text = scoreValue.ToString();
        specialField.text = specialReady.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        specialAvailableStatus();
    }

    public void addHitScore()
    {
        scoreValue += hitScore;
        scoreField.text = scoreValue.ToString();
    }

    public void addKillScore()
    {
        scoreValue += killValue;
        scoreField.text = scoreValue.ToString();
    }

    public void displayWaveStartNum(int waveNumber)
    {
        currentWaveNumberRepresentation.enabled = true;
        currentWaveNumberRepresentation.text = waveNumber.ToString();
        StartCoroutine(disableWaveNumberUI());
    }

    IEnumerator disableWaveNumberUI()
    {
        yield return new WaitForSecondsRealtime(waitTimeBeforeFading);
        currentWaveNumberRepresentation.enabled = false;
    }

    public void specialAvailableStatus()
    {
        String specialAail = canLaunchSpecial().ToString() + "Required :: " + cutOffForSpecialLaunch.ToString();
        specialField.text = specialAail;
    }

    public bool canLaunchSpecial()
    {
        if (scoreValue > cutOffForSpecialLaunch)
        {

            return true;
        }else
        {
            return false;
        }
    }

    public void specialLaunched()
    {
        if (cutOffForSpecialLaunch < 30000)
        {
            cutOffForSpecialLaunch *= 2;
        }
    }

    internal void specialLaunchScoreReduction()
    {
        scoreValue -= cutOffForSpecialLaunch;
        scoreField.text = scoreValue.ToString();
    }
}
