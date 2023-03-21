using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

	public static BirdScript instance;

	[SerializeField] private Rigidbody2D myRigidBody2D;

	[SerializeField] private Animator animator;

	// Audio files
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip flapClick, pointsClip, diedClip;

	// set bird movement speeds
	private float forwardSpeed = 3.0f;
	private float bounceSpeed = 4.0f;

	// declare state monitors
	private bool didFlap;
	public bool isAlive;

	public int score = 0;


	private Button flapButton;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		}
		isAlive = true;
		flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
		flapButton.onClick.AddListener( () => FlapTheBird() );
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (isAlive) {
			Vector3 temp = transform.position;
			temp.x += forwardSpeed * Time.deltaTime;
			transform.position = temp;

			if (didFlap) {
				didFlap = false;
				myRigidBody2D.velocity = new Vector2 (0, bounceSpeed);
				animator.SetTrigger ("Flap");
				audioSource.PlayOneShot(flapClick);
			}

			if (myRigidBody2D.velocity.y >= 0) {
				transform.rotation = Quaternion.Euler (0, 0, 0);
			} else {
				float angle = 0;
				angle = Mathf.Lerp (0, -80, -myRigidBody2D.velocity.y / 7);
				transform.rotation = Quaternion.Euler(0,0,angle);
			}
		}		
	}

	public void FlapTheBird ()
	{
		didFlap = true;
	}

	// get the X position
	public float GetPositionX ()
	{
		return transform.position.x;

	}

	private void SetCamerasX ()
	{

		// update the Camera static offset value
		//CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;

	}


	private void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pipe") {
			if (isAlive) {
				isAlive = false;
				animator.SetTrigger("BirdDied");
				audioSource.PlayOneShot(diedClip);
				GameplayController.instance.PlayerDiedShowScore(score);
			}
		}
	}


	private void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.tag == "Pipeholder") {
			score++;
			GameplayController.instance.SetScore(score);
			audioSource.PlayOneShot(pointsClip);
		}
	}

}
