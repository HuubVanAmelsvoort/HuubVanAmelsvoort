using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using PlayFab;
using PlayFab.ClientModels;

namespace Scripts.QuizSystem {
	public class DataProvider : MonoBehaviour {
		private string _url = "https://script.google.com/macros/s/AKfycbzr4Kg_QnMX2aDf9B26tiR_yG0GNQoPXd6wdOM71vrsFMkglPvAURLFgqZwfpi4s6o32w/exec";

		private List<QuizItem> _quizItems;
		public static DataProvider _instance;

		/*public static DataProvider Instance() {
			if(!_instance) {
				_instance = FindObjectOfType(typeof(DataProvider)) as DataProvider;
				if(!_instance)
					Debug.Log("There need to be at least one active DataProvider on the scene");
			}

			return _instance;
		}*/

		public void Awake() {

			if (_instance != null)
			{
				Destroy(gameObject);
				return;
			}
			_instance = this;
			LoadData();
			LoginPlayfab();
			DontDestroyOnLoad(gameObject);
		}

		public List<QuizItem> GetQuestions() {
			if(_quizItems != null) {
				if(_quizItems.Count != 0) {
					return _quizItems;
				}
			}

			//ReadData(File.ReadAllText("assets/questions.json"), true);
			return _quizItems;
		}

		public void LoadData() {
			StartCoroutine(FetchData(ReadData));
		}

		IEnumerator FetchData(Action<string, bool> callback) {
			UnityWebRequest getData = UnityWebRequest.Get(_url);
			yield return getData.SendWebRequest();
			if(getData.isDone == false || getData.error != null)
				callback(getData.error, false);
			else
				callback(getData.downloadHandler.text, true);
		}

		void ReadData(string response, bool isSuccess) {
			var ms = new MemoryStream(Encoding.UTF8.GetBytes(response));
			var ser = new DataContractJsonSerializer(typeof(QuestionDAO[]));
			var quiz = ser.ReadObject(ms) as QuestionDAO[];

			_quizItems = GETQuestions(quiz);
		}

		private List<string> GETAnswers(QuestionDAO dao) {
			List<string> answers = new List<string>();

			if(dao.Opties != null) {
				string[] answerLines = Regex.Split(dao.Opties, "\n");

				foreach(var keuze in answerLines)
					answers.Add(keuze);
			}

			return answers;
		}

		private List<QuizItem> GETQuestions(QuestionDAO[] daos) {
			var allQuizItems = new List<QuizItem>();

			foreach(var item in daos) {
				var toAdd = new QuizItem {
					Question = item.Vraag,
					Reference = item.Toelichting,
					Category = item.Categorie,
					Info = item.Toelichting,
					CorrectAnswer = item.CorrectAntwoord,
					Answers = GETAnswers(item),
					Hint = item.Hint
				};
				allQuizItems.Add(toAdd);
			}

			return allQuizItems;
		}

		private void LoginPlayfab()
		{
			var request = new LoginWithCustomIDRequest
			{
				CustomId = SystemInfo.deviceUniqueIdentifier,
				CreateAccount = true,
			};
			PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
		}

		void OnSuccess(LoginResult res)
		{
			//Debug.Log("Successfully logged in/ account created[PlayFab]");
		}
		void OnError(PlayFabError err)
		{
			Debug.Log("Error logging into account![PlayFab]");
			Debug.Log(err.GenerateErrorReport());
		}
	}
}