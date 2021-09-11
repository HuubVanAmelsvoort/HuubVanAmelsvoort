using Scripts;
using Scripts.Characters;
using Scripts.QuizSystem;
using System;
using Unity.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    QuizWindow quizWindow;
    public Player playerInstance;

    private static UIManager _instance;
    public GameObject interactionPopUp;
    public GameObject pauseMenuObj;
    
    public GameObject journalObject;
    public GameObject quizObject;
    public GameObject gameOverScreen;
    public GameObject bleedingScreen;
    public GameObject hitScreen;
    public GameObject fadeInOut;
    public GameObject dialoguePanel;
    public GameObject levelStats;
    public GameObject leaderboard;
    
    public bool enableDialoguePanel = false;
    [ReadOnly]
    public bool InitializedDialoguePanel;

    private bool _quizWasActive;
    private bool _dialogueWasActive;
    public float waitingTimer; 

    private void Awake()
    {
        if (_instance != null)
            Debug.LogWarning("More then one instance of UIManager found");
        _instance = this;
    }

    public static UIManager Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType(typeof(UIManager)) as UIManager;

            if (!_instance)
                Debug.Log("There need to be at least one active GenericDialog on the scene");
        }

        return _instance;
    }

    void Start()
    {
        interactionPopUp.SetActive(false);
        quizWindow = QuizWindow.Instance();
        gameOverScreen.SetActive(false);
        pauseMenuObj.SetActive(false);
        journalObject.SetActive(false);
        quizObject.SetActive(false);
        hitScreen.SetActive(false);
        levelStats.SetActive(false);
        playerInstance = Player.Instance();
        _quizWasActive = false;
        _dialogueWasActive = false;
        InitializedDialoguePanel = false;
    }

    //Update is called once per frame
    void Update()
    {
        if(waitingTimer >= 2.5f)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !journalObject.activeSelf)
            {
                Resume();
            }

            if (Input.GetKeyDown(KeyCode.J) && !PauseMenu.isPaused && !journalObject.activeSelf)
            {
                print("First");
                Journal.journalInstance.ClearJournalHints();
                Journal.journalInstance.FirstJournalSprite(true);
                journalObject.SetActive(true);
                if (quizObject.activeSelf)
                {
                    _quizWasActive = true;
                    quizObject.SetActive(false);
                }

            }
            else if ((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.isPaused && journalObject.activeSelf))
            {
                closeJournal();
            }
        }
        else
        {
            waitingTimer += Time.deltaTime;
        }

        CheckPlayerHealth();
    }

    public void Resume() {
        if (PauseMenu.isPaused) {
            if (_quizWasActive) {
                quizObject.SetActive(true);
                _quizWasActive = false;
            }

            //if (DialogueControl.Instance() != null && !DialogueControl.Instance().IsDisabled)
                if (_dialogueWasActive)
                {
                    dialoguePanel.SetActive(true);

                    _dialogueWasActive = false;
                }
            

            pauseMenuObj.GetComponent<PauseMenu>().Resume();
            pauseMenuObj.SetActive(false);
        }
        else {
            if (quizObject.activeSelf) {
                quizObject.SetActive(false);
                _quizWasActive = true;
            }
            if (dialoguePanel.activeSelf) {

                dialoguePanel.SetActive(false);

                _dialogueWasActive = true;
            }
            
            pauseMenuObj.SetActive(true);
            dialoguePanel.SetActive(false);

            pauseMenuObj.GetComponent<PauseMenu>().Pause();
        }
    }

    private void CheckPlayerHealth()
    {
        float current = playerInstance.attributes.CurrentHealth;

        if (bleedingScreen.activeSelf) {
            float max = playerInstance.attributes.MaxHealth;

            bleedingScreen.GetComponent<Animator>().SetInteger("health", Mathf.FloorToInt(current / max * 100));
        }

        if (current <= 0)
        {
            if (gameOverScreen.activeSelf == true)
                return; //prevents from executing more than once on death

            quizWindow.AbortQuiz();
            gameOverScreen.SetActive(true);
            Player.Instance().Die();
        }
    }

    public void AwakeQuiz()
    {
        quizObject.SetActive(true);
    }

    public void CloseQuiz()
    {
        quizObject.SetActive(false);
    }

    public void HitPlayer()
    {
        hitScreen.SetActive(true);
    }

    public void SetActiveDialoguePanel(bool active)
    {
        if (!enableDialoguePanel || InitializedDialoguePanel && dialoguePanel.GetComponent<DialogueControl>().Sentences.Count < 1)
        {
            dialoguePanel.SetActive(false);
            return;
        }

        if (InitializedDialoguePanel && active && dialoguePanel.GetComponent<DialogueControl>().Sentences.Count > 0)
        {
            dialoguePanel.SetActive(true);
            return;
        }

        dialoguePanel.SetActive(active);
    }

    public void FadeOut()
    {
        fadeInOut.GetComponent<Animator>().SetTrigger("fade_out");
        waitingTimer = 0f;
    }

    public void ShowStatistics() {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + Player.Instance().stats.GetScore());
        levelStats?.SetActive(true);
        fadeInOut?.SetActive(false);
        AudioManager.Instance().StopMusic("statistics");
        AudioManager.Instance().Play("statistics");
    }

    public void closeJournal() { //aangemaakt door Roland 22/06/21
        print("WTF????");
        journalObject.SetActive(false);

        if (_quizWasActive == true) {
            _quizWasActive = false;
            quizObject.SetActive(true);
        }
    }
}