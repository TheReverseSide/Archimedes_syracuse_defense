using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	public static int Money;
	public int startMoney = 400;

	public static float health;
	public float startHealth = 20f;
	public float workaround;

	public static int Rounds;
    public Image healthBar;

	public bool isDead;


	void Start ()
	{
		Money = startMoney;
		health = startHealth;

		Rounds = 0;
	}

	void Update() {
        healthBar.fillAmount = (float)health / startHealth;
		// Debug.Log(healthBar.fillAmount);
        workaround = health;
    }

}
