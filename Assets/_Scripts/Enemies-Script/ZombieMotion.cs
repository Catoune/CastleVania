using UnityEngine;
using System.Collections;

public class ZombieMotion : MonoBehaviour
{
	private Vector2 monsterSpeed = new Vector2 (-0.007f, -0.015f);
    private Vector2 speed;
    private float timeToDie = 1.0f;

	public bool isMoveLeft = true;

	void Start ()
    {
		GameObject player = GameObject.FindGameObjectWithTag (Globals.playerTag);

        if (player.transform.position.x > transform.position.x)
        {
            isMoveLeft = false;
            Flip();
        }
        if (!isMoveLeft) { monsterSpeed.x *= -1; }

		speed = new Vector2(0.0f, monsterSpeed.y);
	}

	public void Flip()
    {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;	
	}
	
	void FixedUpdate()
    {
		move ();
	}

	void move()
	{
		Vector3 pos = this.transform.position;
		pos.x += speed.x;
		pos.y += speed.y;
		this.transform.position = pos;
		
		CollisionManager cmScript = GetComponent<CollisionManager>();
		if (cmScript != null) 
		{
			if(cmScript.isWallOn(Globals.Direction.Bottom))
			{
				speed.y = 0;
				speed.x = monsterSpeed.x;
				/*if(cmScript.isWallOn(Globals.Direction.Left))
				{
					speed.x = 0;
					StartCoroutine (autoDie());
				}*/
			}
			else
			{
				speed.x = 0;
				speed.y = monsterSpeed.y;
			}
		}
	}

	void OnTriggerEnter2D( Collider2D coll )
    {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			onPlayerEnter(coll.gameObject);		              
		}

        if (coll.gameObject.tag == "Stair")
        {
            if (!isMoveLeft)
            {
                this.transform.position = new Vector3(transform.position.x + 0.099f, transform.position.y + 0.1f, transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x + -0.099f, transform.position.y + 0.1f, transform.position.z);
            }
        }
    }

    IEnumerator autoDie()
	{
		yield return new WaitForSeconds(timeToDie);
		Destroy (this.gameObject);
	}
	
	void onPlayerEnter(GameObject gb)
	{
        PlayerController pcScript = gb.GetComponent<PlayerController>();
        pcScript.Hurt ();
	}
}
