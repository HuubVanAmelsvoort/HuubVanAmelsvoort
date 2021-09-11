using System.Collections.Generic;
using Scripts.QuizSystem;
using UnityEngine;

public class CombatBehaviour : MonoBehaviour {
	private bool NPCsFound;
	[SerializeField]
	private int questionsPerMonster = 1;

	void Start() {
		NPCsFound = false;
	}

	void Update() {
		if (!NPCsFound)
			FindNpcs();
	}
	
	void FindNpcs()	{ //this code will be run on the 1st frame so that it can detect all the GameObjects
		
		GameObject[] interactables = GameObject.FindGameObjectsWithTag("interactable");
		List<NPC_Monster> monstersNonBoss = new List<NPC_Monster>();
		List<NPC_Monster> monstersBoss = new List<NPC_Monster>();
		List<NPC_Normal> hintNPCs = new List<NPC_Normal>();
		//
		foreach (GameObject interactable in interactables) 
		{
			if (interactable.GetComponent<NPC_Monster>() != null)
			{
				NPC_Monster monster = interactable.GetComponent<NPC_Monster>();
				
				if (monster.IsBoss())
				{
					monstersBoss.Add(monster);
					continue;
				}

				monstersNonBoss.Add(monster);

			}
			else if (interactable.GetComponent<NPC_Normal>() != null)
			{
				NPC_Normal nonMonsterNPC = interactable.GetComponent<NPC_Normal>();
				if (nonMonsterNPC.isHintGiver)
					hintNPCs.Add(nonMonsterNPC);
			}
			else continue;
				
		}
		//Todo: change so level is caried over between scenes 
		Level._instance.SetupLevel(monstersNonBoss.Count * questionsPerMonster); 
		List<QuizItem> levelQuestions = Level._instance.getLevelQuestions().Questions;

		foreach (NPC_Monster boss in monstersBoss)
			boss.setQuestions(Level._instance.getBossQuestions().Questions); //List<QuizItem> is passed as argument
		/*if (levelQuestions.Count > monstersNonBoss.Count)
		{*/
		//adds the number of questions to each monster that has been set in the questions per monster variable
			int j = 0;
			while (j< levelQuestions.Count) 
			{
				foreach (NPC_Monster mNB in monstersNonBoss)
				{
						mNB.setQuestions(levelQuestions[j]);
						Journal.journalInstance.AddHint($"{levelQuestions[j].Question}, hint:{levelQuestions[j].Hint}");
						j++;
				}
			}
		/*}
		else
		for (int i = 0; i < monstersNonBoss.Count; i++)
			monstersNonBoss[i].setQuestions(levelQuestions[i]); //single QuizItem is passed as argument
		*/
		if (hintNPCs.Count > 0)
		{
			// done like this because i do not want to instantiate variables that won't be used or run a check multiple times when once can suffice
			if (hintNPCs.Count != levelQuestions.Count)
			{
				int index= 0;
				List<int> questionGot = new List<int>();
				for (int i = 0; i < hintNPCs.Count; i++)
				{
					if (questionGot.Count == 0)
					{
						index = Random.Range(0, levelQuestions.Count - 1);
						questionGot.Add(index);
					}
					else 
					{
						while (questionGot.Contains(index))
						{
							index = Random.Range(0, levelQuestions.Count - 1);
						}
							questionGot.Add(index);

					}
					//meant to prevent duplicate hints

					string[] hint = { "Ik hoop dat deze Informatie helpt:", levelQuestions[index].Hint };
					hintNPCs[i].setDialogue(hint);
				}

			}
			else {
				for (int i = 0; i < hintNPCs.Count; i++)
				{					
					string[] hint = { "Ik hoop dat deze Informatie helpt:", levelQuestions[i].Hint };
					hintNPCs[i].setDialogue(hint);
				}
			}
            
		}
		
		NPCsFound = true;
	}

	
		
}