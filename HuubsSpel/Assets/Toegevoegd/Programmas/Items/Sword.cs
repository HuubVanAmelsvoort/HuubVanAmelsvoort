using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.QuizSystem;
using UnityEngine.UI;
using TMPro;
using Scripts.Characters;

public class Sword : MonoBehaviour
{
    QuizWindow quizWindow;
    public static Sword instance;
    public TextMeshProUGUI text;
    private int amount = 0;
    string[] answers = { "a", "b", "c", "d" };
    int _num1 = 0;
    int _num2 = 0;

    private void Awake()
    {
        quizWindow = QuizWindow.Instance();
        text.color = new Color(255, 0, 0);
    }

    public static Sword Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(Sword)) as Sword;

            if (!instance)
                Debug.Log("There need to be at least one active SWORD on the scene");
        }

        return instance;
    }

    public void Use()
    {
        if (amount <= 0)
            return;
        
        QuizItem currentQuestion = quizWindow._currentQuestion;
        _num1 = GenerateRandomNumber();
        _num2 = GenerateRandomNumber();
        CheckNumbers();
        

        quizWindow.AnswerButtons[_num1].interactable = false;
        
        quizWindow.AnswerButtons[_num2].interactable = false;
        text.color = new Color(255, 0, 0);
        Player.Instance().stats.itemsUsed++;
        amount = 0;
    }

    private int GenerateRandomNumber()
    {
        return Random.Range(0, 4);
    }


    private void CheckNumbers()
    {
        QuizItem currentQuestion = quizWindow._currentQuestion;
        if (answers[_num1] == currentQuestion.CorrectAnswer )
        {
            _num1 = GenerateRandomNumber();
            CheckNumbers();
        }
        if(_num1 == _num2 || answers[_num2] == currentQuestion.CorrectAnswer)
        {
            _num2 = GenerateRandomNumber();
            CheckNumbers();
        }
    }

    public void AddItem()
    {
        amount = 1;
        text.color = new Color(0, 255, 0);
    }
}
