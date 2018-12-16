using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour {

    public static Dictionary<string, City> Cities = new Dictionary<string, City>();

    public string Name;
    public string Country;
    //public Image Photo;


	// Use this for initialization
	void Start () {

        Cities[Name] = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
