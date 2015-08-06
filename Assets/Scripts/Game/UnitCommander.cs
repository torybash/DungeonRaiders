using UnityEngine;
using System.Collections;

public class UnitCommander : MonoBehaviour {

	[SerializeField] protected Transform weaponContainer;

	//Variables
	protected AI unitAI;

	public int health;
	public float attackCooldownTimer;
	public bool turnedRight;

	public float attackCooldownDuration;
	public float moveSpeed;
	public int maxHealth;

	public Weapon weapon;

	protected Sprite projectileSprite;


	//Refs
	protected GameController gameCtrl;


	//Prefabs
	[SerializeField] protected Transform projectilePrefab;


	protected void Awoken(){
		gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}


	protected void GotHit(Transform damagerT)
	{
		//Direction of hit
		float xDir = Mathf.Sign(damagerT.position.x - transform.position.x);

		//Knockback in opposite direction
		//DEBUG
//		transform.Translate(new Vector2(-xDir, 0) * 0.2f);
		GetComponent<Rigidbody2D>().velocity = new Vector2(-xDir * 1f, 1.5f);

		health -= 5; //DEBUG TODO

		if (health <= 0) Die ();

		Debug.Log("GOT HIT - (vec: " + (new Vector2(-xDir * 1f, 1.5f)) + " ---- time: "+ Time.time);


	}


	protected void Die()
	{
		//TODO Play death animation

		GetComponent<Collider2D>().enabled = false;


		//TODO more elegantly? + handle heroes
		if (this.GetType() == typeof(MonsterCommander)){
			Timer.DelayedExecute(() => {GameObject.Destroy(gameObject); gameCtrl.unitCtrl.MonsterKilled((MonsterCommander)this);}, 1.5f);
		}
		
	}


	protected void UpdateHealthBar()
	{
		gameCtrl.gameUICtrl.UpdateHealthBar(this);
	}
}
