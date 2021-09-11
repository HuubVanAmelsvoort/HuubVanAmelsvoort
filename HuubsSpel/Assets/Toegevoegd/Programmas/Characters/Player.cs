using System;
using System.Collections.Generic;
using UnityEngine;
using static NPC_Normal;

namespace Scripts.Characters {
    public class Player : Character {
        private static Player instance;

        public Statistics stats;
        public Statistics totalStats;
        public HealthBar healthBar;
        public GameObject currentInteractable;
        public GameObject previousInteractable;
        public GameObject interactionPopup;
        public Sprite playerPortait;
        public Rigidbody2D rigidBody;
        public bool isInteracting = false;

        public static Player Instance() {
            if (!instance) {
                instance = FindObjectOfType(typeof(Player)) as Player;

                if (!instance)
                    Debug.Log("There need to be at least one active Player on the scene");
            }
            return instance;
        }

        private void Awake() {
            healthBar = GameObject.Find("Slider").GetComponent<HealthBar>();
        }

        new void Start() {
            base.Start();
            healthBar?.UpdateHealth();
        }

        void Update() {
            CheckMovement();
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                UIManager.Instance().leaderboard.SetActive(true);
            }
        }

        private void CheckMovement() {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (x != 0 || y != 0) {
                if (!isInteracting) {
                    Move(x, y);
                }
            }
            else {
                animator?.SetBool("moving", false);
            }
        }

        public override void Move(float x, float y) {
            Vector2 move = Vector2.zero;
            move.x = x;
            move.y = y;
            move = Vector3.ClampMagnitude(move, 1f);
            MoveAnim(x, y);
            rigidBody.velocity = new Vector2(move.x, move.y) * attributes.MovementSpeed;
        }

        public void MoveAnim(float moveX, float moveY) {
            animator?.SetBool("moving", true);
            animator?.SetFloat("x", moveX);
            animator?.SetFloat("y", moveY);
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
                animator?.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator?.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }
        private void OnTriggerStay2D(Collider2D collision) {
            UIManager.Instance().interactionPopUp.transform.position = transform.position + new Vector3(0, 1.15f, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Interactable interactable = collision.GetComponent<Interactable>();

            if (interactable != null) {
                currentInteractable = collision.gameObject;
                currentInteractable.GetComponent<Interactable>().OnSelected();
            }

            UIManager.Instance().interactionPopUp.transform.position = transform.position + new Vector3(0, 1.15f, 0);
            if (collision.CompareTag("interactable")) {
                UIManager.Instance().interactionPopUp.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            Interactable interactable = collision.GetComponent<Interactable>();

            if (interactable != null)
                interactable.OnDeselect();

            UIManager.Instance().interactionPopUp.SetActive(false);
        }

        public GameObject FindClosestInteractable() {
            GameObject[] allObjects;
            allObjects = GameObject.FindGameObjectsWithTag("interactable");
            GameObject closest = null;
            float InteractDistance = 1.5f;

            foreach (GameObject obj in allObjects) {
                Vector2 distance = obj.transform.position - transform.position;
                float thisDistance = distance.sqrMagnitude;

                if (thisDistance < InteractDistance) {
                    closest = obj;
                    InteractDistance = thisDistance;
                }
            }

            print(closest.name);
            return closest;
        }

        public void ApplyDamage(int damage) {
            UIManager.Instance().HitPlayer();
            attributes.CurrentHealth -= damage;
            healthBar?.UpdateHealth();
        }

        public override void Die() {
            AudioManager.Instance().Play("player death");
        }

        public void AddItem(Item item)
        {
            switch(item)
            {
                case Item.BOOK:
                    Book.Instance().AddItem();
                    break;
                case Item.SWORD:
                    Sword.Instance().AddItem();
                    break;
                default:
                    break;
            }
        }
    }
}