using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kdo {

    string name;
    Image photo;
    City city;
    int difficulty; // 0 - 2

    public Kdo(string name, City city, int difficulty, Image photo = null)
    {
        this.name = name;
        this.photo = photo;
        this.city = city;
        this.difficulty = difficulty;
    }

    public Kdo(string name, string cityN, int difficulty, Image photo = null)
    {
        this.name = name;
        this.photo = photo;
        this.city = City.Cities[cityN];
        this.difficulty = difficulty;
    }


    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Image Photo
    {
        get
        {
            return photo;
        }

        set
        {
            photo = value;
        }
    }

    public City City
    {
        get
        {
            return city;
        }

        set
        {
            city = value;
        }
    }

    public int Difficulty
    {
        get
        {
            return difficulty;
        }

        set
        {
            difficulty = value;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
