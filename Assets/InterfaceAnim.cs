using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceAnim : MonoBehaviour {

    public Image interf;

    public Sprite Interface1;
    public Sprite Interface2;
    private bool switcher;
    private float resetTime;
    private float elapsedTime;

	// Use this for initialization
	void Start () {
		interf = GetComponent<Image>();
        resetTime = 1.0f;
        elapsedTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(Anim());
	}

    private IEnumerator Anim()
    {
        if (elapsedTime < resetTime)
        {
            elapsedTime = elapsedTime + Time.deltaTime;
        }else
        {
            if (switcher)
            {
                interf.sprite = Interface1;
            }
            else
            {
                interf.sprite = Interface2;
            }
            switcher = !switcher;
            elapsedTime = 0.0f;
        }
        yield return new WaitForEndOfFrame();
    }
}
