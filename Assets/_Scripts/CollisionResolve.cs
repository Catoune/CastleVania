using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CollisionResolve : MonoBehaviour
{
	private enum RDirection{Left, Right, Bottom, Top, None};

	public int collIndex;
	public Hashtable collIdTable = new Hashtable();

	void OnTriggerEnter2D( Collider2D coll )
    {
		GameObject collidedObj = coll.gameObject;

		// get the four corner points of both object
		Vector2 objLL = collidedObj.GetComponent<Collider2D>().bounds.min;
		Vector2 objUR = collidedObj.GetComponent<Collider2D>().bounds.max;
		Vector2 myLL = GetComponent<Collider2D>().bounds.min;
		Vector2 myUR = GetComponent<Collider2D>().bounds.max;
			

		List<float> collDepth = new List<float> (new float[4] {float.MaxValue,float.MaxValue,float.MaxValue,float.MaxValue});

//		if(objUR.x >= myLL.x && objLL.x <= myLL.x)             // Player on left
			collDepth[0] = objUR.x - myLL.x;
//		if(objLL.x <= myUR.x && objUR.x >= myUR.x)             // Player on Right
			collDepth[1] = myUR.x - objLL.x;
//		if(objUR.y>= myLL.y && objLL.y <= myLL.y)             // Player on Bottom
			collDepth[2] = objUR.y- myLL.y;
//		if(objLL.y <= myUR.y && objUR.y>= myUR.y)             // Player on Top
		collDepth[3] = myUR.y - objLL.y - 0.02f;


		
		// return the closest intersection
		collIndex = collDepth.IndexOf(Mathf.Min(collDepth.ToArray()));

		CollisionManager cmScript = collidedObj.GetComponent<CollisionManager>();
		if (cmScript != null)
        {
			cmScript.playerCollisionEnter (collIndex, this.gameObject.GetComponent<Collider2D>().bounds.max.y);
			collIdTable.Add(collidedObj.GetInstanceID(), collIndex);
		}
	}
	
	void OnTriggerExit2D( Collider2D coll )
    {
		GameObject collidedObj = coll.gameObject; 
		releaseItem (collidedObj);
	}

	void OnDestroy()
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag(Globals.playerTag);
		if(playerObj && collIdTable.Contains(playerObj.GetInstanceID()))
		{
			releaseItem (GameObject.FindGameObjectWithTag(Globals.playerTag));
		}
	}

	void releaseItem(GameObject collidedObj)
	{	
		CollisionManager cmScript = collidedObj.GetComponent<CollisionManager>();
		if (cmScript != null)
        {
			cmScript.playerCollisionExit ((int)collIdTable[collidedObj.GetInstanceID()]);
			collIdTable.Remove(collidedObj.GetInstanceID());
		}
	}
}
