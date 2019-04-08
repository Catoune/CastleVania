using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour
{
	public const string playerTag = "Player";

	public const string SEdir = "Prefab/AudioObject/";

	public const int maxPlayerHealth = 16;
	public const int maxBossHealth = 16;


	public enum Direction {Right, Left, Top, Bottom};

	public enum ItemName
	{
		Money_S, Money_M, Money_L,
		LargeHeart, SmallHeart,
		WhipUp, Rosary,
		Dagger, Axe, HolyWater, StopWatch,
		BossHeart,
		ChickenLeg
	}

	public enum SubWeapon {Axe,Dagger,HolyWater,StopWatch}

	void Awake(){Physics2D.queriesHitTriggers= true;}
}
