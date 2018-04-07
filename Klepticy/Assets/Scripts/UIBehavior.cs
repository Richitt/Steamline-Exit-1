using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour
{
    static int pos = 0;
    static string displayStr = " ";
    static State state = State.CLOSED;

    public GameObject text;

    private CanvasGroup canvasGroup;
    float fadeTimer = 0f;
    float fadeDuration = 0.3f;

    float holdTimer = 0f;
    float holdDuration = 5f;

    private enum State
    {
        OPENING, OPEN, CLOSING, CLOSED
    }

    public static void DisplayDialogue(string str)
    {
        pos = 0;
        displayStr = str;
        if (state != State.OPEN)
        {
            state = State.OPENING;
        }
    }

    public static bool CheckDialogue()
    {
        return state != State.CLOSED;
    }

	// Use this for initialization
	void Start ()
    {
        state = State.CLOSED;
        canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("space"))
            DisplayDialogue("SAVVVVVVVY DOOOOOESS THIIIISSSSS WOKRKKKKKKKKRKKRKRKRKRK PLEALPAEDASPDLPAL  LASPDLAPJRADPA O OSA DOIAOIDIAPOJ ");
        // handle state
        switch (state)
        {
            case State.CLOSED:
                InputField uiTextShouldBeEmpty = text.GetComponent<InputField>();
                uiTextShouldBeEmpty.text = " ";
                canvasGroup.alpha = 0;
                holdTimer = 0;
                break;
            case State.OPENING:
                // fade in
                canvasGroup.alpha = fadeTimer / fadeDuration;
                fadeTimer += Time.deltaTime;
                if (fadeTimer > fadeDuration)
                {
                    holdTimer = 0;
                    state = State.OPEN;
                }
                break;
            case State.OPEN:
                // scroll text
                pos += 2;
                pos = Math.Min(pos, displayStr.Length);
                string currentStr = displayStr.Substring(0, pos);
                // set the ui text to the current string
                InputField uiText = text.GetComponent<InputField>();
                if (currentStr != "")
                {
                    uiText.text = currentStr;
                }
                // dont set to empty string cuz then it'll show "enter text here..."
                else
                {
                    uiText.text = " ";
                }
                // hold for a while
                canvasGroup.alpha = 1f;
                holdTimer += Time.deltaTime;
                if (holdTimer > holdDuration)
                {
                    state = State.CLOSING;
                }
                break;
            case State.CLOSING:
                // fade in
                canvasGroup.alpha = fadeTimer / fadeDuration;
                fadeTimer -= Time.deltaTime;
                if (fadeTimer <= 0)
                {
                    state = State.CLOSED;
                }
                break;
        }
	}
}
