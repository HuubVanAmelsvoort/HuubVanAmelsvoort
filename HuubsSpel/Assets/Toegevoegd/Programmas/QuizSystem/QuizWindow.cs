using Scripts.Characters;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.QuizSystem {
	public class QuizWindow : MonoBehaviour {
		private static QuizWindow instance;

		public GameObject QuestionText;
		public Button[] AnswerButtons;
		private Player player;

		private UnityAction _correctAnswerAction;
		private UnityAction _wrongAnswerAction;

		public QuizItem _currentQuestion;
		private Queue<QuizItem> _queue;

		public void Awake() {
			player = Player.Instance();

			foreach(var b in AnswerButtons) {
				string text = b.GetComponentInChildren<TextMeshProUGUI>().text;
				b.onClick.AddListener(delegate { ReceivedAnswer(text); });
			}
		}

        public static QuizWindow Instance() {
			if(!instance) {
				instance = FindObjectOfType(typeof(QuizWindow)) as QuizWindow;

				if(!instance)
					Debug.Log("There need to be at least one active QuizWindow on the scene");
			}

			return instance;
		}

		public QuizWindow SetQuestions(QuizItem[] questions) {
			_queue = new Queue<QuizItem>(questions);
			return this;
		}

		public QuizWindow SetOnCorrectAnswer(UnityAction action) {
			_correctAnswerAction = action;
			return this;
		}

		public QuizWindow SetOnWrongAnswer(UnityAction action) {
			_wrongAnswerAction = action;
			return this;
		}

		private void initQuestion() {
			//used to enable buttons after prevention of spam click of correct answer(a very bad solution)
			for (int i = 0; i < AnswerButtons.Length; i++) { AnswerButtons[i].interactable = true; }
			QuestionText.GetComponent<TextMeshProUGUI>().text = _currentQuestion.Question;

			for(var i = 0; i < Math.Min(_currentQuestion.Answers.Count, 4); i++) {
				AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _currentQuestion.Answers[i];
			}
		}

		public void StartQuiz() {
			player.isInteracting = true;
            gameObject.SetActive(true);
			nextQuestion();
		}

		private void nextQuestion() {
			if(_queue.Count < 1) {
				AbortQuiz();
				return;
			}

            foreach (var item in AnswerButtons)
            {
				item.interactable = true;
            }
			_currentQuestion = _queue.Dequeue();
			Debug.Log("Correct answer: " + _currentQuestion.CorrectAnswer.ToUpper());
			initQuestion();
		}

		private bool isCorrect(string answer) {
			return char.ToUpper(answer[0]) == char.ToUpper(_currentQuestion.CorrectAnswer[0]);
		}

		//This method is called when answer input is received
		public void ReceivedAnswer(string input) {
			Player.Instance().stats.answersGiven++;

			if(isCorrect(input)) {
				Player.Instance().stats.UpdateCorrectAnswerStreak();
				Player.Instance().stats.correctAnswers++;
				//used to prevent spam click of correct answer(a very bad solution)
				Journal.journalInstance.AddQuestions(_currentQuestion.Question, _currentQuestion.getAnswerTextjabadabadooooooo());

				for (int i=0; i< AnswerButtons.Length; i++) { AnswerButtons[i].interactable = false; }
				
				_correctAnswerAction.Invoke();
				nextQuestion();
				return;
			}
			Player.Instance().stats.EndCorrectAnswerStreak();
			_wrongAnswerAction.Invoke();
		}

		public void AbortQuiz() {
			player.isInteracting = false;
			_queue.Clear();
			UIManager.Instance().dialoguePanel.SetActive(false);

			//UIManager.Instance().SetActiveDialoguePanel(false);
		}
	}
}