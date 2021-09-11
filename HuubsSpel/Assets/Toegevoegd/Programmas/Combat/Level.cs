using System.Collections.Generic;
using UnityEngine;
using Scripts.QuizSystem;
using System;
//TODO: change to be moved between scenes
public class Level:MonoBehaviour {
    
    private int _bossQuestionAmount;
	private int _numberOffQuestions;
	private Quiz _bossQuestions = new Quiz();
	private Quiz _questions = new Quiz();
	private bool dataCalled = false;
	//needed in order to remove questions already used without the risk of creating an infinante loop(small chance but possible)
	List<QuizItem> allQuestions = new List<QuizItem>();
	public static Level _instance;

	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
			return;
		}

		_instance = this;
		
		DontDestroyOnLoad(gameObject);
	}
	//calculations of _bossQuestionAmount: is always 'min', every 'n' questions there will be 1 additional question added to the number of boss questions
	//3 questions  / 5 questions  =  .6 + 3 min = 3;
	//8 questions  / 5 questions  = 1.6 + 3 min = 4;
	//12 questions / 5 questions  = 2.4 + 3 min = 5;

	public void SetupLevel(int questions) {
		//yes I know it is basically the same as below. this is just used once on the first call so we can easily remove the get infinte questions option if we wish to end the game when all questions have been answered
		if (!dataCalled)
		{
			allQuestions.AddRange(DataProvider._instance.GetQuestions());
			dataCalled = true;
		}
		
		int min = Math.Min(3, questions);
		int n = 5;
		_questions.Questions = new List<QuizItem>();
		_bossQuestionAmount = questions / n + min;
		_numberOffQuestions = questions;
		// makes sure ther are enough questions could be used to end the game in the future
		if (allQuestions.Count < _numberOffQuestions)
		{
			allQuestions.AddRange(DataProvider._instance.GetQuestions());
		}

		setLevelQuestions(questions);
	}

	//selects the number of questions needed for the level
	private void setLevelQuestions(int amount) {
		for (int i = 0; i < amount; i++) {
			int index = UnityEngine.Random.Range(0, allQuestions.Count - 1); //this line can be used to randomize questions
			//int index = 0;                                         //this line can be deleted to avoid static questions

			QuizItem pulledQuestion = allQuestions[index];

			_questions.Questions.Add(pulledQuestion);
			//removes question to prevent duplicates
			allQuestions.Remove(pulledQuestion);
		}

		setBossQuestions();
	}

	// will select questions for the bos from the questions for non-boss monsters to ensure that the boss does not get new questions
	private void setBossQuestions() {
		//needed in order to remove questions already used
		Quiz levelQuestions = new Quiz();

		levelQuestions.Questions = new List<QuizItem>();

		foreach(QuizItem qustion in _questions.Questions)
			levelQuestions.Questions.Add(qustion);

		_bossQuestions.Questions = new List<QuizItem>();

		for(int i = 0; i < _bossQuestionAmount; i++) {
			int index = i;//Random.Range(0, levelQuestions.Questions.Count - 1);

			QuizItem pulledQuestion = levelQuestions.Questions[index];

			_bossQuestions.Questions.Add(pulledQuestion);
			//removes question to prevent duplicates
			//levelQuestions.Questions.Remove(pulledQuestion);
		}
	}

	public Quiz getBossQuestions() {
		return _bossQuestions;
	}

	public Quiz getLevelQuestions() {
		return _questions;
	}
}