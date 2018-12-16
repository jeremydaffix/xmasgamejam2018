using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Kdo {

    public static List<Kdo> Kdos1 = new List<Kdo>();
    public static List<Kdo> Kdos2 = new List<Kdo>();
    public static List<Kdo> Kdos3 = new List<Kdo>();


    string name;
    Image photo;
    City city;
    int difficulty; // 0 - 2


    public static void GenerateKdos()
    {
        new Kdo("Paris", 0);
        new Kdo("Berlin", 0);
        new Kdo("Dublin", 0);
        new Kdo("Bruxelles", 0);
        new Kdo("Madrid", 0);
        new Kdo("London", 0);
        new Kdo("Luxembourg", 0);
        new Kdo("Montreal", 0);
        new Kdo("New York", 0);

        new Kdo("Pretoria", 1);
        new Kdo("Montpellier", 1);

        new Kdo("Oulan Bator", 2);
        new Kdo("Sucre", 2);
    }



    public static Kdo DrawKdo(int diff)
    {
        List<Kdo> l;

        if (diff == 0) l = Kdos1;
        else if (diff == 1) l = Kdos2;
        else l = Kdos3;

        return l.ElementAt(Random.Range(0, l.Count));
    }




    public Kdo(string cityN, int difficulty, string name = "", Image photo = null)
    {
        this.name = name;
        this.photo = photo;
        this.city = City.Cities[cityN];
        this.difficulty = difficulty;


        if (difficulty == 0) Kdos1.Add(this);
        else if (difficulty == 1) Kdos2.Add(this);
        else if (difficulty == 2) Kdos3.Add(this);
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
