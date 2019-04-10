using UnityEngine;
using System.Collections;


//Attaché à un trigger
public class MonsterSpawner : MonoBehaviour
{
	public GameObject  demonPrefab;
	public Vector2 createAt;
    public int numberOfMonsters;
    private int demonNum = 0;
	public float creationDelayInSec = 0.5f;
	public float refreshTime = 5.0f;
	
	void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player") {StartCoroutine(spawnMonster());}
	}

	IEnumerator spawnMonster()
	{
		while (GetComponent<Collider2D>().enabled == true)
        {
			for (; demonNum < numberOfMonsters; ++demonNum)
            {
				GameObject gameObj = Instantiate (demonPrefab) as GameObject;
				gameObj.transform.position = new Vector3 (transform.position.x + createAt.x, transform.position.y + createAt.y, 0);
				yield return new WaitForSeconds (creationDelayInSec);
                if (GetComponent<Collider2D>().enabled == false) { yield return true; }
			}
			yield return new WaitForSeconds (refreshTime/2f);
            if (GetComponent<Collider2D>().enabled == false) { yield return true; }
			yield return new WaitForSeconds (refreshTime/2f);
			demonNum = 0;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
        {
			StartCoroutine(waitToReset());
		}
	}

	IEnumerator waitToReset()
	{
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds (refreshTime);
		GetComponent<Collider2D>().enabled = true;
	}
}
