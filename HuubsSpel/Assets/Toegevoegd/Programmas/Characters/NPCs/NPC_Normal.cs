using System;
using Scripts;
using Scripts.Characters;

public class NPC_Normal : NPC {
	private bool _isTalking = false;
	private bool _hintGiven = false;
	public bool isHintGiver;
	public bool _speaksHint = true;
	private string[] hint;
	public Item item;

	new void Update() {
		if(PlayerInRange && !_isTalking) {
			InteractWithMe();
			/*DialogueControl.Instance().StartDialogue(Conversation);
			IsInteracting = true;*/
		}
	}

	public override void Die()
	{
		throw new NotImplementedException();
	}

	public void setDialogue(string[] hint) 
	{
		this.hint = hint;
		if ( _speaksHint) {
			string[] hintDialogue = new string[Conversation.ToSay.Length + hint.Length];
			Conversation.ToSay.CopyTo(hintDialogue, 0);
			hint.CopyTo(hintDialogue, Conversation.ToSay.Length);
			Conversation.ToSay = hintDialogue;
		}
	}

	public override void InteractWithMe() {
        if (!UIManager.Instance().dialoguePanel.gameObject.activeSelf)
		{
			DialogueControl.Instance().StartDialogue(Conversation, true, this.Image, this.Name);
			if(item != Item.NOTHING)
				Player.Instance().AddItem(item);
			if(hint !=null && !_hintGiven)
			Journal.journalInstance.AddHint(hint[1]);
			_hintGiven = true;
		}
		else
		{
			DialogueControl.Instance().DisplayNext();
		}
	}

	public enum Item
	{
		NOTHING,
		BOOK,
		SWORD
	}
}