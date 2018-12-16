using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayScript : MonoBehaviour {

    [Header("Rise In Animation")]
    public AnimationCurve RiseInAnimationCurve;
    public float RiseInDuration;
    private float RiseInElapsed;


    [Header("UI")]
    public Text DisplayedCountry;
    public Text DisplayedScoreOne;
    public Text DisplayedScoreTwo;
    public Text DisplayedMiddleText;

    [Header("Score Animation")]
    public AnimationCurve BumpAnimationCurve;
    public float BumpDuration;
    private float BumpElapsed;
    public float BumpMagnitude;
    public Color BumpColor;

    [Header("Gameplay")]
    private int ScoreOne;
    private int ScoreTwo;
    private int ScoreMax;
    private bool IsInGame;
    private Kdo currentKdo;
    private bool PlayerOneTurn;

    // Use this for initialization
    void Start () {
        IsInGame = true;
        PlayerOneTurn = true;
        StartCoroutine(OnBeginGame());
        ScoreMax = 30; //TODO update
        ScoreOne = 0;
        ScoreTwo = 0;
        StartCoroutine(UpdateScoreOne());
        StartCoroutine(UpdateScoreTwo());
    }
	
	// Update is called once per frame
	void Update () {
        if (!IsInGame)
        {
            currentKdo = Kdo.DrawKdo(0);
            StartCoroutine(DisplayCountry(currentKdo.City.name));
            IsInGame = true;
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pressed primary button." + Input.mousePosition);
                if (PlayerOneTurn)
                {
                    //ScoreOne += 10; //TODO add score here
                    ScoreOne += CalcScore(Input.mousePosition, currentKdo);
                    UpdateScoreOne();
                } else
                {
                    //ScoreTwo += 10; //TODO add score here
                    ScoreOne += CalcScore(Input.mousePosition, currentKdo);
                    UpdateScoreTwo();
                }
                PlayerOneTurn = !PlayerOneTurn; //switch player
                IsInGame = false;
                if(ScoreOne >= ScoreMax || ScoreTwo >= ScoreMax)
                {
                    string player = "1";
                    if (ScoreTwo >= ScoreMax) { player = "2"; }
                    StartCoroutine(DisplayMiddleText("Joueur " + player + " gagnant!"));
                }
            }
        }
    }

    private IEnumerator DisplayCountry(string country)
    {
        DisplayedCountry.text = country;
        RiseInElapsed = 0.0f;
        float min = 0.3f;
        Vector3 minScale = new Vector3(min, min, 1.0f);
        DisplayedCountry.transform.localScale = minScale;
        while (RiseInElapsed != RiseInDuration)
        {
            yield return new WaitForEndOfFrame();
            RiseInElapsed = Mathf.Clamp(RiseInElapsed + Time.deltaTime, 0.0f, RiseInDuration);
            float scale = RiseInAnimationCurve.Evaluate(RiseInElapsed / RiseInDuration);
            DisplayedCountry.transform.localScale = minScale + new Vector3(1.0f - min, 1.0f - min, 0.0f) * scale;

        }
    }

    private IEnumerator UpdateScoreOne()
    {
        BumpElapsed = 0.0f;
        Vector3 bumpedScale = new Vector3(BumpMagnitude, BumpMagnitude, 0.0f);
        Vector3 minScale = new Vector3(1.0f, 1.0f, 1.0f);
        //DisplayedScoreOne.transform.localScale = minScale;
        while (BumpElapsed != BumpDuration)
        {
            yield return new WaitForEndOfFrame();
            BumpElapsed = Mathf.Clamp(BumpElapsed + Time.deltaTime, 0.0f, BumpDuration);
            float value = BumpAnimationCurve.Evaluate(BumpElapsed / BumpDuration);
            if (value >= 0.5f)
                DisplayedScoreOne.text = ScoreOne.ToString();
            //DisplayedScoreOne.transform.localScale = minScale + bumpedScale * value;
            DisplayedScoreOne.color = Color.Lerp(Color.white, BumpColor, value);

        }
        yield return null;
    }

    private IEnumerator UpdateScoreTwo()
    {
        BumpElapsed = 0.0f;
        Vector3 bumpedScale = new Vector3(BumpMagnitude, BumpMagnitude, 0.0f);
        Vector3 minScale = new Vector3(1.0f, 1.0f, 1.0f);
        //DisplayedScoreTwo.transform.localScale = minScale;
        while (BumpElapsed != BumpDuration)
        {
            yield return new WaitForEndOfFrame();
            BumpElapsed = Mathf.Clamp(BumpElapsed + Time.deltaTime, 0.0f, BumpDuration);
            float value = BumpAnimationCurve.Evaluate(BumpElapsed / BumpDuration);
            if (value >= 0.5f)
                DisplayedScoreTwo.text = ScoreTwo.ToString();
            //DisplayedScoreTwo.transform.localScale = minScale + bumpedScale * value;
            DisplayedScoreTwo.color = Color.Lerp(Color.white, BumpColor, value);

        }
        yield return null;
    }

    private IEnumerator DisplayMiddleText(string message)
    {
        DisplayedMiddleText.text = message;
        RiseInElapsed = 0.0f;
        float min = 0.3f;
        Vector3 minScale = new Vector3(min, min, 1.0f);
        DisplayedMiddleText.transform.localScale = minScale;
        while (RiseInElapsed != RiseInDuration)
        {
            yield return new WaitForEndOfFrame();
            RiseInElapsed = Mathf.Clamp(RiseInElapsed + Time.deltaTime, 0.0f, RiseInDuration);
            float scale = RiseInAnimationCurve.Evaluate(RiseInElapsed / RiseInDuration);
            DisplayedMiddleText.transform.localScale = minScale + new Vector3(1.0f - min, 1.0f - min, 0.0f) * scale;

        }
    }

    private IEnumerator OnBeginGame()
    {
        //TODO add sound for 3 2 1 Go
        IsInGame = true; //block input
        StartCoroutine(DisplayCountry("3"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DisplayCountry("2"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DisplayCountry("1"));
        yield return new WaitForSeconds(1.0f);
        Kdo.GenerateKdos();
        IsInGame = false;
    }










    public int CalcScore(Vector3 posMouse, Kdo kdo)
    {
        int score = 0;

        Vector3 posKdo = /*Camera.main.WorldToScreenPoint*/(kdo.City.transform.position);

        float dist = Vector3.Distance(posMouse, posKdo);
        int diff = kdo.Difficulty;


        // score entre 0 et 120

        int scoreDist = 0;
        if (dist < 10) scoreDist = 4;
        else if (dist < 50) scoreDist = 3;
        else if (dist < 100) scoreDist = 2;
        else if (dist < 200) scoreDist = 1;
        else scoreDist = 0;

        score = (diff + 1) * scoreDist * 10;


        Debug.Log("***CALCSCORE***");
        Debug.Log(dist);
        Debug.Log(score);


        return score;
    }                                                                                                                 
}
