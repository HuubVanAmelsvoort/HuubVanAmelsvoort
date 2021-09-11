using System.Collections.Generic;
using Scripts.Characters;
using Scripts.QuizSystem;
using UnityEngine;

public class NPC_Monster : NPC, IDamagable {
    public float _attackDistance = 7;
    public MonsterType type;

    public bool evade = true;

    private GameObject player;
    public bool facingRight;
    public bool isIdle;

    //objects related to boss_monster
    public List<Gate> gates;
    public GameObject portal;

    private List<QuizItem> _questions = new List<QuizItem>();

    new void Start() {
        animator.SetBool("idle", isIdle);
        GetComponent<SpriteRenderer>().flipX = !facingRight;

        player = Player.Instance().gameObject;

        // sets attributes and actions based on type
        switch (type) {
            case MonsterType.Boss:
                attributes.Damage = (int)(attributes.Damage * 1.5);
                break;
            case MonsterType.Agro:
                attributes.Damage = (int)(attributes.Damage * 1.25);
                break;
            default:
                break;
        }
    }

    new void Update() {
        if (!Player.Instance().isInteracting)
        {
            // calculates distance between monster and player
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (type == MonsterType.Agro && distance < _attackDistance && !IsInteracting)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    facingRight = true;
                }
                else
                {
                    facingRight = false;
                }

                GetComponent<SpriteRenderer>().flipX = !facingRight;
                //for walking animation
                animator.SetBool("idle", false);
                //animator.SetBool("walk", true);
                //moves monster to player
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, attributes.MovementSpeed * Time.deltaTime);
                if (distance <= 2 && !IsInteracting)
                {
                    InteractWithMe();
                }
            }
        }else
            animator.SetBool("idle", true);
    }

    //this method is called as an event of the last frame of the hit animation (DONT MAKE PRIVATE) 
    public void CheckHealth() {
        if (attributes.CurrentHealth <= 0) {
            Die();
            return;
        }
    }

    public override void Die() {
        if (IsBoss()) {
            AudioManager.Instance().StopMusic();
            AudioManager.Instance().Play("dungeon 1", .5f);
            portal.SetActive(true);

            foreach (Gate gate in gates) {
                gate.Unlock(true, true);
            }
        }
        Player.Instance().isInteracting = true;
        gameObject.tag = "Untagged";
        QuizWindow.Instance().AbortQuiz();
        UIManager.Instance().CloseQuiz();
        Player.Instance().stats.npcsDefeated++;
        animator.SetTrigger("die");
    }

    //used to check if a monster is a boss // replaces isBoss bool
    public bool IsBoss() {
        return type == MonsterType.Boss;
    }

    //this method is called as an event of the last frame of the animator of the NPC (DONT MAKE PRIVATE)
    public void Destroy() {
        Destroy(gameObject);
    }

    public void ApplyDamage() {
        animator.SetTrigger("attack");
        Player.Instance().ApplyDamage(attributes.Damage);

        if (IsBoss())
            AudioManager.Instance().Play("boss punch");
        else
            AudioManager.Instance().Play("quack");
    }

    public void setQuestions(List<QuizItem> questions) {
        if (questions != null)
            _questions.AddRange(questions);

        setCurrentHealth();
    }

    public void setQuestions(QuizItem question) {
        if (question != null)
            _questions.Add(question);

        setCurrentHealth();
    }

    //attributes is a property inherited from NPC_Monster : NPC : Character
    private void setCurrentHealth() {
        attributes.CurrentHealth = _questions.Count * Player.Instance().attributes.Damage;
    }

    private void ReceiveDamage() {
        attributes.CurrentHealth -= Player.Instance().attributes.Damage;

        if (attributes.CurrentHealth <= 0) {
            AudioManager.Instance().Play("monster death");
            AudioManager.Instance().Play("ugh kid", .2f);
        }
        else {
            AudioManager.Instance().Play("punch");
        }

        animator.SetTrigger("hit");
    }

    public override void InteractWithMe() {
        if (!IsInteracting) {
            if (type == MonsterType.Agro)
            {
                //for walking animation
                animator.SetBool("idle", true);
                //animator.SetBool("walk", false);
            }
            IsInteracting = true;
            Player.Instance().isInteracting = true;
            QuizWindow //Builder pattern; methods can be chained because they return QuizWindow (except for StartQuiz() method)
                .Instance()
                .SetQuestions(_questions.ToArray())
                .SetOnCorrectAnswer(ReceiveDamage)
                .SetOnWrongAnswer(ApplyDamage)
                .StartQuiz();
        }
    }
}

public enum MonsterType { Passive, Agro, Boss }