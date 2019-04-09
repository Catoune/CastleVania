﻿using UnityEngine;
using System.Collections;

public class HurtManager : MonoBehaviour {

	public float InvisibleInterval = 1.0f;
	public float initHurtVerticalSpeed = 1.0f;

	private bool hurting; // only provide accessor
	public bool Hurting {
		get {
			return hurting || disableControl;
		}
	}

	private bool disableControl;

	private Animator animator;
	private PlayerController pc;
	private SpriteRenderer sprite;
	private StatusManager status;

	void Start () {
		animator = GetComponent<Animator> ();
		pc = GetComponent<PlayerController> ();
		sprite = GetComponent<SpriteRenderer> ();
		status = GetComponent<StatusManager> ();
		hurting = false;

		disableControl = false;
	}
	// HACK
	public bool onFlyHurting () {
		return disableControl;
	}
    // ============================================================================ //
    public IEnumerator Hurt()
    {
        status.playerHealth -= 2;

        Debug.Log("HURTING: Hurt fucntion called");
        hurting = true;

        // TODO do what ever is needed to turn off some collision
        // CODE HERE
        disableControl = true;

        animator.SetBool("Hurt", true);
        pc.VerticalSpeed = 0; //reset to avoid fly high
        pc.VerticalSpeed += initHurtVerticalSpeed;
        // add horizontal speed according to facing
        pc.HorizontalVelocity = pc.isFacingRight ? -1 : 1;

        yield return new WaitForSeconds(0.33f);
        animator.SetBool("Hurt", false);
        Debug.Log("HURTING: fly state cleaned");
        StartCoroutine(turnInvisible());
        // turn 
        animator.SetInteger("Speed", 0);

        yield return new WaitForSeconds(0f);
        pc.HorizontalVelocity = 0;
        disableControl = false;
    }
	
	public IEnumerator turnInvisible () {
		hurting = true;
		Color curColor = sprite.color;
		curColor.a = curColor.a/2; // turn to half alpha
		sprite.color = curColor;

		yield return new WaitForSeconds (InvisibleInterval);
		curColor.a = 1.0f; // return to full alpha
		sprite.color = curColor;
		hurting = false;

		// TODO enable physics again 
	}
}
