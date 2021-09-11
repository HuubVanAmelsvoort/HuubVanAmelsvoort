using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public Dialogue Conversation;
    protected DialogueManager _dialogueManager;
    public static Journal journalInstance;
    List<string> _journalHints;
    List<string> _journalQuestions;
    List<string> _journalLore;
    private List<string> _journalPrevious;
    public Text textbox;
    public SpriteRenderer spriteRenderer;
    public Image image;
    public Sprite journal_1;
    public Sprite journal_2;
    public Sprite journal_3;
    private Sprite journal_Previous;

    //Singleton
    private void Awake()
    {
        if (journalInstance != null)
            Debug.LogWarning("More then one instance of journal found");
        journalInstance = this;
    }

    void Start()
    {
        textbox.supportRichText = true;
        journalInstance._journalHints = new List<string>();
        journalInstance._journalQuestions = new List<string>();
        journalInstance._journalLore = new List<string>();
        journalInstance.AddLore("22-06-2021\nLief dagboek,\nWat had ik vandaag een heerlijk dag.Ik werd vroeg in de ochtend wakker door de schijnende zon. \n  Ik besloot de TV aan te zetten voor tijdens het ontbijt en ik zag een reclame over een grote Jackpot van €1.000.000, -euro.\nAh wat zou ik nou kunnen doen met een miljoen euro ? Misschien de wijk verbeteren ? Misschien wat aan mijn kinderen geven en op vakantie te gaan naar Curaçao.Vandaag was werk ook niet heel zwaar wat mij blij maakte.\nDe laatste weken waren heel druk dus wat rust was nodig.\n");
        journalInstance.AddLore("23-06-2021\nLief dagboek,\nIk zat zo na te denken over de Jackpot dat ik een lot had gekocht.Dat ding kostte maar een paar eurotjes, maar het kan uiteindelijk zo veel goeds doen.\nHet is ook woensdag vandaag, wat betekend dat we over de helft zijn! We gaan dit weekend kamperen.Ik moet niet weer vergeten om mijn sandalen mee te nemen!\n");
        journalInstance.AddLore("24-06-2021\nLIEF DAGBOEK!!!!\nDe loterij nummers kwamen vanochtend binnen! Alle nummers waren correct! Ik moest het geld zo snel mogelijk ophalen dus heb ik vrij genomen van werk! \nIk kan het nog steeds niet geloven.Ik denk dat ik eindelijk het huisje ga kopen die ik en mijn wijfie altijd wilde hebben.Het heeft natuurlijk wat werk dat er aan gedaan moet worden, maar het staat al zo lang te koop, ik denk niet dat het een probleem gaat worden om het te kopen.\n");
        journalInstance.AddLore("25-06-2021\nLief dagboek,\nIk schrijf wat eerder vandaag want het wordt een lange dag bij de gemeente.Ik moet eerst wat papieren halen voordat ik het huis een beetje op kan knappen.Ik had wat gehoord over “structurele veranderingen” of zo iets…\nBegin ik nou knettergek te worden of ik heb net een spook gezien die zei dat ik door een lift moest gaan.. \nIk denk dat het een goed idee zou zijn om wat ik tegen kom even op te schrijven.\n");
        /*journalInstance.AddHint("Lief dagboek,\nvandaag ben ik naar de gemeente Den Haag gegaan. Hier heb ik met de afdeling Verlening & Toetsing te maken gehad vanwege de sloop van mijn ouderlijk huis.");
        journalInstance.AddHint("Lief dagboek,\nblijkbaar had ik te maken met een sloop aanvraag omdat ik een burger ben.");
        journalInstance.AddHint("Stom dagboek,\nVcgvimv yilmmvm pzm qv mllrg evigilfdvm.");
        journalInstance.AddLore("test2");*/
        //journalInstance.ClearJournal();
    }

    public void AddHint(string _hint)
    {
        string hint = _hint + "\n";
        journalInstance._journalHints.Add(hint);        
    }

    public void AddQuestions(string question, string answer)
    {
        string questionAndAnswer = question + $"\n {answer} \n";
        journalInstance._journalQuestions.Add(questionAndAnswer);        
    }

    public void AddLore(string _lore)
    {
        string lore = _lore + "\n";
        journalInstance._journalLore.Add(lore);
    }

    //TO CHANGE: Possibly clears journal after a level, might not be needed because we maybe want to ask the same questions in later levels again without giving them the same hints again
    public void ClearJournalHints()
    {
        textbox.text = "";
    }

    public void FirstJournalSprite(bool openPreviousSprite = false)
    {
        SwitchJournalSprite(journal_1, _journalHints, openPreviousSprite);
    }

    public void SecondJournalSprite(bool openPreviousSprite = false)
    {
        SwitchJournalSprite(journal_2, _journalQuestions, openPreviousSprite);
    }

    public void ThirdJournalSprite(bool openPreviousSprite = false)
    {
        SwitchJournalSprite(journal_3, _journalLore, openPreviousSprite);
    }

    public void SwitchJournalSprite(Sprite sprite, List<string> text, bool openPreviousSprite = false) {
        print($"good point {sprite.name}");

        textbox.text = "";

        if (!openPreviousSprite || journal_Previous == null || _journalPrevious == null) {
            image.sprite = sprite;
            _journalPrevious = text;
        }

        journal_Previous = image.sprite;
        foreach (string line in _journalPrevious) {
            textbox.text += line + "\n";
        }
    }

    public void closeJournal() {
        UIManager.Instance().closeJournal();
    }
}
