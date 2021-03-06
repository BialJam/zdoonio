﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerStats : MonoBehaviour {

	private float maxHealth = 100;
	private float currentHealth = 100;
	private float maxStamina = 100;
	private float currentStamina = 100;
	public Text healthText;
	public Text staminaText;
	public Text macedoniaText;
	public GameObject ally;
	public Transform spawnAlly;
	public float timer;

	private float canHeal = 0.0f;
	private float canRegenerate = 0.0f;

	private CharacterController chCont;
	private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsC;
	private Vector3 lastPosition;

	void Awake()
	{
		chCont = GetComponent<CharacterController>();
		fpsC = gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ();
		lastPosition = transform.position;
	}


	void Start()
	{
		timer = 0.0f;
	}

	void Update()
	{
		timer += Time.deltaTime;

		healthText.text =  "Health: " +  currentHealth;
		staminaText.text = "Stamina: " +  currentStamina;
		macedoniaText.text = "Ładowanie przyzwania kapitana Macedonii " + timer;

		if ((Input.GetButtonDown ("Fire1"))){
			currentStamina -= 30;

			}

		if ((Input.GetButtonDown ("Fire2"))){
			if (timer > 30.0f) {
				currentStamina -= 100;
				Instantiate (ally, spawnAlly.position, spawnAlly.rotation);
				timer = 0.0f;
			}
		}

		if (currentHealth == 0) {
			Application.LoadLevel (1);
		}
			

		if(canHeal > 0.0f) {
			canHeal -= Time.deltaTime;
		}
		if(canRegenerate > 0.0f) {
			canRegenerate -= Time.deltaTime;
		}

		if(canHeal <= 0.0f && currentHealth < maxHealth) {
			regenerate(ref currentHealth, maxHealth);
		}
		if(canRegenerate <= 0.0f && currentStamina < maxStamina) {
			regenerate(ref currentStamina, maxStamina);
		}

		if (currentStamina <= 0) {
			currentStamina = 0;
		}

	}

	void FixedUpdate () 
	{
		if(chCont.isGrounded && Input.GetKey(KeyCode.LeftShift)) {
			lastPosition = transform.position;
			currentStamina -= 1;
			currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
		}	

		if (currentStamina > 0) {
			fpsC.m_RunSpeed = 7;
		} else {
			fpsC.m_RunSpeed = 3;
		}	
	}

	void takeHit(float damage) 
	{
		currentHealth -= damage;

		if(currentHealth < maxHealth) {
			canHeal = 5.0f;
		}

		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}

	void regenerate(ref float currentStat, float maxStat)
	{
		currentStat += maxStat * 0.005f;
		Mathf.Clamp(currentStat, 0, maxStat);
	}

}