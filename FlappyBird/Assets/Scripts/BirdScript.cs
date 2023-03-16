using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{

    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float fowardSpeed = 3f;
    
    [SerializeField]
    private float bounceSpeed = 4f;

   	[SerializeField] 
    private AudioSource audioSource;
	
    [SerializeField] 
    private AudioClip flapClick, pointsClip, diedClip;

    private bool didFlap;

    public bool isAlive;

    private Button flapButton;

    public int score = 0;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        isAlive = true;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        // flapButton.onClick.AddListener(() => FlapTheBird());
        setCameraX();

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += fowardSpeed * Time.deltaTime;
            transform.position = temp;

            if (didFlap)
            {
                didFlap = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClick);
                anim.SetTrigger("Flap");
            }

            if (myRigidBody.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

        }
    }

    void setCameraX() 
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public void FlapTheBird()
    {
        didFlap = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D collision)
	{

		// did it hit the ground or a pipe?
		if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Pipe") {

			// sanity that it's still alive
			if (isAlive) {
				isAlive = false;
				anim.SetTrigger("Bird Died");
				audioSource.PlayOneShot(diedClip);
			}
		}
	}


	private void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.tag == "PipeHolder") {
            score++;
			audioSource.PlayOneShot(pointsClip);
		}
	}
}