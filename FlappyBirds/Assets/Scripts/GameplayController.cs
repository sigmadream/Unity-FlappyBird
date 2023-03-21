using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	[SerializeField] Text scoreText, endScore, bestScore, gameOverText;

	[SerializeField] Button restartGameButton, instructionsButton;

	[SerializeField] GameObject pausePanel;

	[SerializeField] GameObject[] birds;
	[SerializeField] Sprite[] medals;

	[SerializeField] Image medalImage;

	// Use this for initialization
	void Start () {
		MakeInstance();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MakeInstance ()
	{
		if (instance == null) {
			instance = this;
		}
	}



	public void PauseGame ()
	{
		if (BirdScript.instance != null && BirdScript.instance.isAlive) {

			// activate pause panel
			pausePanel.SetActive(true);

			// deactivate the game over text
			gameOverText.gameObject.SetActive(false);

			// populate scores
			endScore.text = BirdScript.instance.score.ToString();
			bestScore.text = GameController.instance.HighScore.ToString();

			// pause the game
			Time.timeScale = 0f;

			// clear all button functions
			restartGameButton.onClick.RemoveAllListeners();

			// Add the one function it needs
			restartGameButton.onClick.AddListener( () => ResumeGame() );

		}


	}


	public void GoToMenuButton ()
	{
		SceneManager.LoadScene("MainMenu");
	}


	public void ResumeGame() 
	{
		// turn off pause panel
		pausePanel.SetActive(false);

		// resume game
		Time.timeScale = 1f;

	}


	public void RestartGame ()
	{
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}


	public void PlayGame ()
	{

		scoreText.gameObject.SetActive(true);

		birds[GameController.instance.SelectedBird].SetActive(true);

		instructionsButton.gameObject.SetActive(false);

		Time.timeScale = 1f;

	}


	public void SetScore (int score)
	{
		scoreText.text = score.ToString();
		

	}


	public void PlayerDiedShowScore (int score)
	{

		pausePanel.SetActive (true);

		gameOverText.gameObject.SetActive (true);

		scoreText.gameObject.SetActive (false);

		endScore.text = score.ToString ();



		if (score > GameController.instance.HighScore) {

			GameController.instance.HighScore = score;

		}

		bestScore.text = GameController.instance.HighScore.ToString ();


		if (score <= 20) {
			medalImage.sprite = medals [0];
		} else if (score > 20 && score <= 40) {
			medalImage.sprite = medals [1];

			// unlock green bird
			GameController.instance.IsGreenBirdUnlocked = 1;

		} else {
			medalImage.sprite = medals [2];

			// unlock red bird
			GameController.instance.IsRedBirdUnlocked = 1;

		}


		// clear all button functions
		restartGameButton.onClick.RemoveAllListeners();

		// Add the one function it needs
		restartGameButton.onClick.AddListener( () => RestartGame() );


	}


}
