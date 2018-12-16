using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroAnim : MonoBehaviour {

    public GameObject Photo, Photo2, Black, Santa, About;
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
        //iTween.FadeFrom(Santa, iTween.Hash("alpha", 0f, "amount", 1f, "time", 1.5f));

        //iTween.ShakePosition(Santa, iTween.Hash("x", 5.0f, "time", 0.5f, "delay", 0.0f));
        //iTween.ShakePosition(Santa, iTween.Hash("y", 5.0f, "time", 0.5f, "delay", 0.0f));

        yield return new WaitForSeconds(1.5f);
        About.SetActive(true);
        yield return new WaitForSeconds(2.0f);

        Santa.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        Black.SetActive(false);
        Photo2.gameObject.SetActive(true);


        yield return new WaitForSeconds(1.0f);

        StartCoroutine(AnimMouth());

        Dialog.gameObject.SetActive(true);

        Dialog.text = "Messieurs, vous etes des larves.";
        yield return new WaitForSeconds(3f);

        Dialog.text = "Votre simple vue me donne des nausees.";
        yield return new WaitForSeconds(3f);

        Dialog.text = "Blabla, j'aime donner du bonheur, blabla.";
        yield return new WaitForSeconds(3f);

        Dialog.text = "Mais il faut bien trouver un remplacant au pere noel.";
        yield return new WaitForSeconds(3f);

        Dialog.text = "La survie de notre entreprise en depend.";
        yield return new WaitForSeconds(3f);

        Dialog.text = "Ne me decevez pas.";
        yield return new WaitForSeconds(3f);

        Dialog.text = "...";
        yield return new WaitForSeconds(1f);

        Dialog.text = "Livrez-moi ces produits en vitesse si vous voulez le job.";
        yield return new WaitForSeconds(3f);

        Dialog.text = "Que le moins mauvais gagne.";
        yield return new WaitForSeconds(4f);


        SceneManager.LoadScene("Game"); 
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

            yield return new WaitForSeconds(0.5f);
        }
    }
}
