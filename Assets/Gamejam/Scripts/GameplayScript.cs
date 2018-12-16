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
    public GameObject Map;

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
    private bool GameEnded;


    bool flagShake = false;



    // Use this for initialization
    void Start () {
        IsInGame = true;
        PlayerOneTurn = true;
        GameEnded = false;
        StartCoroutine(OnBeginGame());
        ScoreMax = 300; //TODO update
        ScoreOne = 0;
        ScoreTwo = 0;
        DisplayedScoreOne.text = ScoreOne.ToString();
        DisplayedScoreTwo.text = ScoreTwo.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameEnded)
        {
            if (!IsInGame)
            {
                currentKdo = Kdo.DrawKdo(0);
                StartCoroutine(DisplayCountry(currentKdo.City.name));
                IsInGame = true;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Pressed primary button." + Input.mousePosition);
                    int WonScore = CalcScore(Input.mousePosition, currentKdo);
                    if (PlayerOneTurn)
                    {
                        ScoreOne += WonScore;
                        DisplayedScoreOne.text = ScoreOne.ToString();
                        //Debug.Log("One: " + ScoreOne);
                    }
                    else
                    {
                        ScoreTwo += WonScore;
                        DisplayedScoreTwo.text = ScoreTwo.ToString();
                        //Debug.Log("Two: " + ScoreTwo);
                    }
                    PlayerOneTurn = !PlayerOneTurn; //switch player
                    IsInGame = false;
                    if (ScoreOne >= ScoreMax || ScoreTwo >= ScoreMax) //Fin du jeu
                    {
                        GameEnded = true;
                    }
                }


                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Shake();
                }
            }
        } else
        { //if Game is finished display score and retry
            string player = "1";
            if (ScoreTwo >= ScoreMax) { player = "2"; }
            StartCoroutine(DisplayMiddleText("Joueur " + player + " gagnant!"));
            if (Input.GetMouseButtonDown(0))
            {
                GameEnded = false;
                ResetGame();
            }
        }
    }


    private void FixedUpdate()
    {

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
        StartCoroutine(DisplayMiddleText("3"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DisplayMiddleText("2"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DisplayMiddleText("1"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DisplayMiddleText(""));
        Kdo.GenerateKdos();
        IsInGame = false;
    }

    private void ResetGame()
    {
        ScoreOne = 0;
        ScoreTwo = 0;
        DisplayedScoreOne.text = ScoreOne.ToString();
        DisplayedScoreTwo.text = ScoreTwo.ToString();
        StartCoroutine(OnBeginGame());
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
  
        return score;
    }
    



    public void Shake()
    {   
        if (!flagShake)
        {
            //Debug.Log("SHAKE");
            //Camera.main.GetComponent<CameraShake>().Shake();

            iTween.ShakePosition(Map, iTween.Hash("x", 50.0f, "time", 1.5f, "delay", 0.0f));
            iTween.ShakePosition(Map, iTween.Hash("y", 50.0f, "time", 1.5f, "delay", 0.0f));



            flagShake = true;

            Invoke("EnableShake", 5f);
        }
    }

    void EnableShake()
    {
        flagShake = false;
    }
}
