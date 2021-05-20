using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
	private bool fadeOut = false;	// Is the object fading out right now?
	private bool fadeIn = false;	// Is the object fading in right now?
	public float fadeSpeed;			// Increase for faster fade
	public bool startInvisible = false;
	public bool fadeOutOnAwake = false;
	
	void Awake() {
		if (fadeOutOnAwake) {
			FadeOut();
		}
	}
	
	void Start() {
		// Make the object invisible by setting its alpha to 0
		if (startInvisible) {
			Color objectColor = GetComponent<Image>().color;
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
			GetComponent<Image>().color = objectColor;
		}
	}
	
	void Update() {
		// If the object is fading out...
		if (fadeOut)
		{
			// Get its current color
			Color objectColor = GetComponent<Image>().color;
			// Adjust alpha based on fadeSpeed
			float newAlpha = objectColor.a - (fadeSpeed * Time.deltaTime);
			
			// Apply the new color
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, newAlpha);
			GetComponent<Image>().color = objectColor;
			
			// If alpha is zero or negative, the object has completely faded out
			// Stop trying to make it fade
			if (objectColor.a <= 0)
				fadeOut = false;
		}
		
		// If the object is fading in...
		if (fadeIn)
		{
			// Get its current color
			Color objectColor = GetComponent<Image>().color;
			// Adjust alpha based on fadeSpeed
			float newAlpha = objectColor.a + (fadeSpeed * Time.deltaTime);
			
			// Apply the new color
			objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, newAlpha);
			GetComponent<Image>().color = objectColor;
			
			// If alpha is 1 or more, the object has completely faded in
			// Stop trying to make it fade
			if (objectColor.a >= 1)
				fadeIn = false;
		}
	}
	
	public void FadeOut() {
		fadeOut = true;
	}
	
	public void FadeIn() {
		fadeIn = true;
	}
}