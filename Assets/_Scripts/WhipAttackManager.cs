using UnityEngine;
using System.Collections;

public class WhipAttackManager : MonoBehaviour
{
    [HideInInspector]
    private PlayerController CharacterController;
    [HideInInspector]
    private Animator animator;


    [HideInInspector] 
	public bool attacking;
    public int WeaponLevel = 1;
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
        // C'est pour jouer le son de l'attaque
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
		genRayHit(From, To);
	}

	void genRayHit (Vector3 From, Vector3 To)
    {
		RaycastHit2D[] hits = Physics2D.RaycastAll(From, (To-From).normalized, (To-From).magnitude);

		foreach (RaycastHit2D hit in hits)
        {
			GameObject gb  = hit.transform.gameObject      ;
			OnWhipEvent CC = gb.GetComponent<OnWhipEvent>();

			if (CC)
            {
				CC.onWhipEnter();
			}
		}
		Debug.DrawLine(From, To, Color.blue, 1.0f);
	}


	Vector3 WhipStart(float yCorrection)
    {
		return new Vector3(CharacterController.transform.position.x + 0.00f                * (CharacterController.isFacingRight ? 1.0f : -1.0f   ), 
                           CharacterController.transform.position.y + yCorrection + -0.05f * (animator.GetBool("Squat")         ? 1.0f : 0.0f), 0);
	}

	Vector3 WhipEnd(Vector3 From)
    {
		if (WeaponLevel <= 2)
			return new Vector3(From.x + 0.32f *  (CharacterController.isFacingRight ? 1.0f : -1.0f), From.y, 0); //Shortest Weapon
		else 
			return new Vector3(From.x + 0.54f *  (CharacterController.isFacingRight ? 1.0f : -1.0f), From.y, 0); //Longest Weapon
	}
}
