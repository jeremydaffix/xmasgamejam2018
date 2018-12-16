using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroAnim : MonoBehaviour {

    public GameObject Photo, Photo2, Black, Santa;
    public Text Dialog;

    int cpt = 0;


	// Use this for initialization
	void Start () {

        StartCoroutine(Anim());
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private IEnumerator Anim()
    {
        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(2f);

        Santa.SetActive(true);

        //iTween.FadeFrom(Santa, iTween.Hash("alpha", 0f, "time", 1.5f));

        yield return new WaitForSeconds(2f);

        Black.SetActive(false);

        StartCoroutine(AnimMouth());

        yield return new WaitForSeconds(2f);

        Photo.SetActive(true);
        Photo2.SetActive(false);

        Dialog.gameObject.SetActive(true);

        Dialog.text = "Vous etes des larves.";
        yield return new WaitForSeconds(2f);

        Dialog.text = "Mais il faut bien trouver un remplacant au pere noel.";
        yield return new WaitForSeconds(2f);

        Dialog.text = "Ne me decevez pas.";
        yield return new WaitForSeconds(2f);

        Dialog.text = "...";
        yield return new WaitForSeconds(2f);

        Dialog.text = "Que le moins mauvais gagne.";
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("theo"); 
    }


    private IEnumerator AnimMouth()
    {
        yield return new WaitForEndOfFrame();

        while(true)
        {
            if(cpt % 2 == 0)
            {
                Photo.SetActive(true);
                Photo2.SetActive(false);
            }

            else
            {
                Photo.SetActive(false);
                Photo2.SetActive(true);
            }

            cpt++;

            yield return new WaitForSeconds(0.7f);
        }
    }
}
