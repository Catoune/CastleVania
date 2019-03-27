using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public static float WhipLengthLong = 0.54f; // 48 pixel
	public static float WhipLengthShort = 0.32f;  // fixed
	public static float PivotToWhipStart = 0.00f; // fixed
	public static float SquatOffset = -0.05f; // 5 pixel 
	public static float WhipHeightOffset = 0.05f;// 5 pixel
	public static float playerWidth = 0.16f; // 16 pixelß



	public const string playerTag = "Player";
	public const string groundTag = "Ground";
	public const string MapTag = "Map";
	public const string EnemyTag = "Enemy";

	public const string SEdir = "Prefab/AudioObject/";

	public const int maxPlayerHealth = 16;
	public const int maxBossHealth = 16;


	public enum Direction {
		Right, 
		Left, 
		Top, 
		Bottom
	};

	public enum ItemName
	{
		Money_S, Money_M, Money_L,
		LargeHeart, SmallHeart,
		WhipUp, Rosary,
		Dagger, Axe, HolyWater, StopWatch,
		BossHeart,
		ChickenLeg
	}

	public enum SubWeapon
	{
		Axe,
		Dagger,
		HolyWater,
		StopWatch
	}

	void Awake()
    {
		Physics2D.queriesHitTriggers= true;
	}

}
