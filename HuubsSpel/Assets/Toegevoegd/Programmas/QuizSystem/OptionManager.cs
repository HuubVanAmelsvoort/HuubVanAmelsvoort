using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


//TODO: move to UI because this class only manages the buttonpanel and registers the reaction (it does not handle it). 
public class OptionManager : MonoBehaviour
{
    private Dialogue _dialogue; //Optional: Submitting the options for a Question might be possible with
    //an override method or a parent class (children: Dialogue, Question) 

    public Button[] Buttons;


    //private bool _isClicked = false;

    public string ChosenOption { get; private set; } = "";

    public void DisableAnswers()
    {
        gameObject.SetActive(false);
    }

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplayOptions(Dialogue dialogue)
    {
        gameObject.SetActive(true);

        //TODO block all input

        for (var i = 0; i < Buttons.Length; i++)
        {
            if (i >= dialogue.Options.Length)
            {
                Buttons[i].gameObject.SetActive(false);
                continue;
            }

            var answer = dialogue.Options[i];

            Buttons[i].gameObject.SetActive(true);

            var buttonText = Buttons[i].GetComponentInChildren<Text>();
            Buttons[i].onClick.AddListener(delegate { StoreAnswer(buttonText); });
            buttonText.text = answer;
        }
    }


    void StoreAnswer(Text text)
    {
        //_isClicked = true;
        print("stored: " + text.text);
        ChosenOption = text.text;
        //_dialogue.Reply = text.text;
    }

    // used to check if the monster gets damaged
    public string givenAnswer()
    {
        return ChosenOption;
    }
}