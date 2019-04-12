using UnityEngine;
using System.Collections;

public class OnWhipHitDestroy : OnWhipEvent
{
	public GameObject itemPrefab;

	public  bool fixedItem = true;
	private bool hitted    = false;

	public override void onWhipEnter ()
    {
        if (!hitted) { StartCoroutine(revealItemAndDestroy()); }
	}

	IEnumerator revealItemAndDestroy()
	{
		hitted = true;
		GameObject deathEffect = Resources.Load ("Prefab/death") as GameObject;
		Instantiate (deathEffect, GetComponent<Collider2D>().bounds.center, Quaternion.identity);

		GameObject hitSE = Resources.Load (Globals.SEdir + "hitSE") as GameObject;
		Instantiate (hitSE, transform.position, Quaternion.identity);

		GameObject pObj = GameObject.FindGameObjectWithTag (Globals.playerTag);
		if(pObj)
		{
			StatusManager smScript = pObj.GetComponent<StatusManager> ();
            if (smScript){ smScript.score += 100;}
		}

		Instantiate (itemPrefab, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
		yield return null;
	}
}


