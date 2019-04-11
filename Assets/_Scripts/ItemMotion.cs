using UnityEngine;
using System.Collections;

public class ItemMotion : MonoBehaviour
{
	public float speedY = -1.0f;
	public float perishInSec = 10.0f;

	public float Amplitude = 0.5f;
	public float omega = 2f;
	public Globals.ItemName itemName;

	private float SpeedX;
	private float time;
	private StatusManager status;
	private CollisionManager cmScript;

	StatusManager smScript;
	WhipAttackManager attManager;

	void Start ()
    {
		time = Time.time;
		cmScript = GetComponent<CollisionManager> ();
	}
	
	void FixedUpdate ()
    {
		move ();
	}

	void move()
	{
		if (speedY < 0)
        {
			if (cmScript.isWallOn (Globals.Direction.Bottom))
            {
				hitGround();
			}
			SpeedX = Amplitude * Mathf.Cos(omega * (Time.time - time));
			Vector3 pos = this.transform.position;
			pos.y += speedY * Time.fixedDeltaTime;
			pos.x += SpeedX * Time.fixedDeltaTime;
			this.transform.position = pos;
		}
	}

	public void hitGround()
	{
		speedY = 0;
		Destroy (this.gameObject, perishInSec);
	}

	void OnTriggerEnter2D( Collider2D coll )
    {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			itemPickedUp(collidedObj);		              
		}
	}

	void itemPickedUp(GameObject plObj)
	{
		smScript = plObj.GetComponent<StatusManager> ();
		attManager = plObj.GetComponent<WhipAttackManager> ();

		switch (itemName)
		{
		    case Globals.ItemName.Money_S:			
		    case Globals.ItemName.Money_M:					
		    case Globals.ItemName.Money_L:		
			    GameObject moneySE = Resources.Load (Globals.SEdir + "moneySE") as GameObject;
			    Instantiate (moneySE, transform.position, Quaternion.identity);
			    break;
			
		    case Globals.ItemName.SmallHeart:		
		    case Globals.ItemName.LargeHeart:	
			    GameObject heartSE = Resources.Load (Globals.SEdir + "heartSE") as GameObject;
			    Instantiate (heartSE, transform.position, Quaternion.identity);
			    break;
			
		    case Globals.ItemName.WhipUp:
	
		    default:
			    GameObject defaultSE = Resources.Load (Globals.SEdir + "heartSE") as GameObject;
			    Instantiate (defaultSE, transform.position, Quaternion.identity);
			    break;
		}

		switch (itemName)
		{
		    case Globals.ItemName.Money_S:
			    smScript.score += 100;
			    Debug.Log ("fetched small money");
			    break;

		    case Globals.ItemName.Money_M:		
			    smScript.score += 400;
			    Debug.Log ("fetched medium money");
			    break;

		    case Globals.ItemName.Money_L:		
			    smScript.score += 700;
			    Debug.Log ("fetched large money");
			    break;

		    case Globals.ItemName.SmallHeart:		
			    smScript.heart += 1;
			    Debug.Log ("fetched heart");
			    break;

		    case Globals.ItemName.LargeHeart:	
			    smScript.heart += 5;
			    Debug.Log ("fetched heart");
			    break;

		    case Globals.ItemName.ChickenLeg:
			    smScript.playerHealth += 6;
			    Mathf.Clamp(smScript.playerHealth, 0, Globals.maxBossHealth);
			    break;

		    default:
			    break;
		}

		if(GetComponent<AudioSource>())
			GetComponent<AudioSource>().Play ();
		Destroy (this.gameObject);
	}
}