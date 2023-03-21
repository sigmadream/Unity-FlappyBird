using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public static MenuController instance;
	[SerializeField] GameObject[] birds;

	void Awake ()
	{
		MakeSingleton ();
	}

	// Use this for initialization
	void Start () {
		birds[GameController.instance.SelectedBird].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MakeSingleton ()
	{
		if (instance == null) {
			instance = this;
		}
	}

	public void ChangeBird ()
	{	
		if (GameController.instance.SelectedBird == 0 && GameController.instance.IsGreenBirdUnlocked == 1) {	
			birds [0].SetActive (false);
			GameController.instance.SelectedBird = 1;
			birds [GameController.instance.SelectedBird].SetActive (true);
		} else if (GameController.instance.SelectedBird == 1 && GameController.instance.IsRedBirdUnlocked == 1) {
			birds [1].SetActive (false);
			GameController.instance.SelectedBird = 2;
			birds [GameController.instance.SelectedBird].SetActive (true);
		} else {
			birds [1].SetActive (false);
			birds [2].SetActive (false);
			GameController.instance.SelectedBird = 0;
			birds [GameController.instance.SelectedBird].SetActive (true);		
		}
	}

	// Create function to allow Start Game button to work from main menu.
	public void StartGame ()
	{
		SceneManager.LoadScene("Gameplay");
	}

}
