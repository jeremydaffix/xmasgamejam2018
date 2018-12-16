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

    public Texture2D PlayerOneCursor;
    public Texture2D PlayerTwoCursor;
    public Texture2D PlayerOneCursorClicked;
    public Texture2D PlayerTwoCursorClicked;


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

    public SoundSystem SoundSystem;

    public GameObject Facile, Moyen, Difficile;

    bool flagShake = false;



    // Use this for initialization
    void Start () {
        IsInGame = true;
        PlayerOneTurn = true;
        Cursor.SetCursor(PlayerOneCursor, Vector2.zero, CursorMode.ForceSoftware);
        GameEnded = false;
        StartCoroutine(OnBeginGame());
        ScoreMax = 300; //TODO update
        ScoreOne = 0;
        ScoreTwo = 0;
        DisplayedScoreOne.text = ScoreOne.ToString() + " / 300";
        DisplayedScoreTwo.text = ScoreTwo.ToString() + " / 300";
    }
	
	// Update is called once per frame
	void Update () {

        if (!Kdo.KdoInitialized) return;

        UpdateCursor();
        if (!GameEnded)
        {
            if (!IsInGame)
            {
                int diff = Random.Range(0, 3);
                currentKdo = Kdo.DrawKdo(diff);
                StartCoroutine(DisplayCountry(currentKdo.City.name));
                IsInGame = true;

                if(diff == 0)
                {
                    Facile.SetActive(true);
                    Moyen.SetActive(false);
                    Difficile.SetActive(false);
                }

                else if (diff == 1)
                {
                    Facile.SetActive(false);
                    Moyen.SetActive(true);
                    Difficile.SetActive(false);
                }

                else
                {
                    Facile.SetActive(false);
                    Moyen.SetActive(false);
                    Difficile.SetActive(true);
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    int WonScore = CalcScore(Input.mousePosition, currentKdo);
                    if (PlayerOneTurn)
                    {
                        ScoreOne += WonScore;
                        DisplayedScoreOne.text = ScoreOne.ToString() + " / 300";
                        //Debug.Log("One: " + ScoreOne);
                        if(ScoreOne >= 300) SoundSystem.PlayVictory();
                    }
                    else
                    {
                        ScoreTwo += WonScore;
                        DisplayedScoreTwo.text = ScoreTwo.ToString() + " / 300";
                        //Debug.Log("Two: " + ScoreTwo);
                        if (ScoreTwo >= 300) SoundSystem.PlayVictory();
                    }
                    PlayerOneTurn = !PlayerOneTurn; //switch player
                    SwitchCursor();
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
            //SoundSystem.PlayVictory();
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
        Kdo.KdoInitialized = false;
        Kdo.Kdos1.Clear();
        Kdo.Kdos2.Clear();
        Kdo.Kdos3.Clear();

        iTween.ShakePosition(Map, iTween.Hash("x", 10.0f, "time", 1.0f, "delay", 0.0f));
        //iTween.ShakePosition(Map, iTween.Hash("y", 50.0f, "time", 1.5f, "delay", 0.0f));

        //UpdateCursor();

        if (PlayerOneTurn)
        {
            Cursor.SetCursor(PlayerOneCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(PlayerTwoCursor, Vector2.zero, CursorMode.ForceSoftware);
        }

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
        Debug.Log("resetgame");
        //SoundSystem.PlayVictory();
        ScoreOne = 0;
        ScoreTwo = 0;
        DisplayedScoreOne.text = ScoreOne.ToString() + " / 300";
        DisplayedScoreTwo.text = ScoreTwo.ToString() + " / 300";
        StartCoroutine(OnBeginGame());
    }



    public int CalcScore(Vector3 posMouse, Kdo kdo)
    {
        if (kdo == null) return 0;

        int score = 0;

        Vector3 posKdo = /*Camera.main.WorldToScreenPoint*/(kdo.City.transform.position);

        float dist = Vector3.Distance(posMouse, posKdo);
        int diff = kdo.Difficulty;

        // score entre 0 et 120

        int scoreDist = 0;

        if (dist < 10) scoreDist = 4;
        else if (dist < 40) scoreDist = 3;
        //else if (dist < 80) scoreDist = 2;
        //else if (dist < 150) scoreDist = 1;
        //else scoreDist = 0;

        if(dist  < 40) SoundSystem.PlayGood();
        else SoundSystem.PlayBad();

        score = (diff + 1) * scoreDist * 10;
  
        return score;

    }
    



    public void Shake()
    {   
        if (!flagShake)
        {
            //Debug.Log("SHAKE");
            //Camera.main.GetComponent<CameraShake>().Shake();

            SoundSystem.PlayShake();

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
    
    private void UpdateCursor()
    {
        if (PlayerOneTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(PlayerOneCursorClicked, Vector2.zero, CursorMode.ForceSoftware);
                if (Input.GetMouseButtonUp(0))
                {
                    Cursor.SetCursor(PlayerOneCursor, Vector2.zero, CursorMode.ForceSoftware);
                }
            }
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(PlayerTwoCursorClicked, Vector2.zero, CursorMode.ForceSoftware);
                if (Input.GetMouseButtonUp(0))
                {
                    Cursor.SetCursor(PlayerTwoCursor, Vector2.zero, CursorMode.ForceSoftware);
                }
            }
        }
    }

    private void SwitchCursor()
    {
        if (PlayerOneTurn)
        {
           Cursor.SetCursor(PlayerOneCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(PlayerTwoCursor, Vector2.zero, CursorMode.ForceSoftware);
        }

    }
}
