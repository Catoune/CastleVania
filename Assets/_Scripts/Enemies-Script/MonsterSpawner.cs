using UnityEngine;
using System.Collections;


//Attaché à un trigger
public class MonsterSpawner : MonoBehaviour
{
	public GameObject monsterPrefab     ; // Monstre à faire spawn
	public Vector2    createAt          ; // Position de création

    public  int numberMonsters          ; // Nombre de monstre à faire Spawn
    private int currentMonsterNumber = 0; // Nombre actuel de monstre

	public float creationDelay = 0.5f   ; // Temps avant de spawn un autre monstre
	public float refreshTime   = 5.0f   ; // Temps avant de repartir à 0
	
	void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player") {StartCoroutine(spawnMonster());}
	}

	IEnumerator spawnMonster()
	{
		while (GetComponent<Collider2D>().enabled == true)
        {
			for (; currentMonsterNumber < numberMonsters; ++currentMonsterNumber)
            {
				GameObject gameObj = Instantiate (monsterPrefab) as GameObject;
				gameObj.transform.position = new Vector3 (transform.position.x + createAt.x, transform.position.y + createAt.y, 0);
				yield return new WaitForSeconds (creationDelay);
                if (GetComponent<Collider2D>().enabled == false) { yield return true; }
			}
			yield return new WaitForSeconds (refreshTime/2f);
            if (GetComponent<Collider2D>().enabled == false) { yield return true; }
			yield return new WaitForSeconds (refreshTime/2f);
			currentMonsterNumber = 0;
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
