using UnityEngine;
using System.Collections;

public class WhipAttackManager : MonoBehaviour
{

	public int WeaponLevel = 1;
	public LayerMask collideLayer;
	[HideInInspector] 
	public bool attacking;
	[HideInInspector] 
	private Animator animator;
	private PlayerController CharacterController;

	private float attackWait = 0.66f;

	void Start ()
    {
		animator = GetComponent<Animator> ();
		CharacterController = GetComponent<PlayerController> ();
		attacking = false;
	}

    void FixedUpdate()
    {
        attacking = (animator.GetInteger("Attack") > 0);

        if (attacking)
        {
            genWhipHit(0.16f);
            genWhipHit(0.10f);
        }
    }

    public IEnumerator WhipAttack()
    {
		if (CharacterController.grounded && animator.GetInteger("Speed") != 0) {animator.SetInteger("Speed", 0);} // Speed à 0 car on veut pas qu'il bouge quand il attaque
		attacking = true;
		animator.SetInteger ("Attack", WeaponLevel);

		yield return new WaitForSeconds(0.15f);
        // C'est pour jouer le son
        //GameObject whipHitSE = Resources.Load (Globals.SEdir + "whipHitSE") as GameObject;
        //Instantiate (whipHitSE, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(attackWait - 0.15f);
		animator.SetInteger ("Attack", 0);
		attacking = false;
	}

	void genWhipHit(float yCorrection)
    {
		Vector3 From = WhipStart(yCorrection);
		Vector3 To = WhipEnd(From);
		// pixel correction +- heightOffset
		genRayHit(From, To);
	}

	void genRayHit (Vector3 From, Vector3 To)
    {
		RaycastHit2D[] hits = Physics2D.RaycastAll(From, (To-From).normalized, (To-From).magnitude, collideLayer);
		Debug.Log("number of hits: " + hits.Length);
		// Boardcast to all objects that has a WhipEventhandler
		foreach (RaycastHit2D hit in hits)
        {
			GameObject gb = hit.transform.gameObject;
			OnWhipEvent CC = gb.GetComponent<OnWhipEvent>();
            Debug.Log(gb.name);
			if (CC)
            {
				CC.onWhipEnter();
			}
		}
		Debug.DrawLine(From, To, Color.blue, 1.0f);
	}


	Vector3 WhipStart(float yCorrection)
    {
		return new Vector3(
			CharacterController.transform.position.x + Globals.PivotToWhipStart * (CharacterController.facingRight ? 1.0f : -1.0f), 
			CharacterController.transform.position.y + yCorrection + Globals.SquatOffset * (animator.GetBool("Squat") ? 1.0f : 0.0f), 0);
	}

	Vector3 WhipEnd(Vector3 From)
    {
		if (WeaponLevel <= 2)
			return new Vector3(From.x + Globals.WhipLengthShort * (CharacterController.facingRight ? 1.0f : -1.0f), From.y, 0);
		else 
			return new Vector3(From.x + Globals.WhipLengthLong * (CharacterController.facingRight ? 1.0f : -1.0f), From.y, 0);

	}

}
