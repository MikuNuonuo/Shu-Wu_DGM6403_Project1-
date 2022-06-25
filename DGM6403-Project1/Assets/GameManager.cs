using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static int health = 3;
	public static int flower = 0;
	public Text flowerDisplay;
	public GUISkin layout;

	GameObject thePlayer;
	// Start is called before the first frame update
	void Start()
	{
		thePlayer = GameObject.FindGameObjectWithTag("Player");

	}
	void OnGUI()
	{
		
		health = thePlayer.GetComponent<Healthbar1>().health;
		GUI.skin = layout;
		//GUI.Label(new Rect(Screen.width / 2 - 250 - 12, 20, 100, 100), "HP: " + health);
		//GUI.Label(new Rect(Screen.width / 2 + 100, 20, 160, 100), "Score: " + flower);

		if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
		{
			health = 3;
			flower = 0;
			PlayAgain();
		}

		if (health == 0)
		{
			GUI.Label(new Rect(Screen.width / 2 - 200, 200, 2000, 1000), "YOU LOSE!");
			Time.timeScale = 0;

		}
		else if (flower == 25)
		{
			GUI.Label(new Rect(Screen.width / 2 - 200, 200, 2000, 1000), "YOU WIN!");
			Time.timeScale = 0;
		}
	}
	private void Update()
	{

		flowerDisplay.text = flower.ToString();
	}
		public void IncrementScore()
	{
		flower++;
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene("Chip 'n' Dale");
	}

}