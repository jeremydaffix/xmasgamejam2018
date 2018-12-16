using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ClicEasy : MonoBehaviour {
    public Image gift;
    public Sprite opened;
    public Sprite closed;
	// Use this for initialization
	void Start () {
        gift = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        gift.sprite = opened;
    }
}
