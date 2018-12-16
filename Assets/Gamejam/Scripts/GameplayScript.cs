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

    [Header("Score Animation")]
    public AnimationCurve BumpAnimationCurve;
    public float BumpDuration;
    private float BumpElapsed;
    public float BumpMagnitude;
    public Color BumpColor;

    [Header("Gameplay")]
    private int ScoreOne;
    private int ScoreTwo;
    private bool IsInGame;
    private Kdo currentKdo;

    // Use this for initialization
    void Start () {
        IsInGame = true;
        StartCoroutine(OnBeginGame());
        
        ScoreOne = 30;
        ScoreTwo = 34;
        StartCoroutine(UpdateScoreOne());
        StartCoroutine(UpdateScoreTwo());

        //IsInGame = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!IsInGame)
        {
            IsInGame = true;
            currentKdo = Kdo.DrawKdo(0);
            StartCoroutine(DisplayCountry(currentKdo.City.name));
            Debug.Log(currentKdo.City.transform);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pressed primary button." + Input.mousePosition);
                IsInGame = false;
            }
        }
    }

    private IEnumerator DisplayCountry(string letter)
    {
        DisplayedCountry.text = letter;
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
}
