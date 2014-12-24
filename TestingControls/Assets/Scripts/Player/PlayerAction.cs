using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour{
	

	private bool sprinting;
	private bool jumping;
	private bool turn90Right, turn90Left;

	private float speed;	
	private float direction;



	/*
	 * 
	 */ 
	void Awake(){
		this.sprinting = false;
		this.jumping = false;
		this.turn90Right = false;
		this.turn90Left = false;
		this.speed = 0f;
	} // end of Awake function



	/*
	 * Getters and Setters
	 */ 

	public float getSpeed(){return speed;}

	public void setSpeed(float speed){this.speed = speed;}

	public bool isSprinting(){return sprinting;}

	public void setSprinting(bool sprinting){this.sprinting = sprinting;}

	public bool isJumping(){return jumping;}

	public void setJumping(bool jumping){this.jumping = jumping;}

	public void setDirection(float direction){this.direction = direction;}

	public float getDirection(){return this.direction;}

	public bool getTurn90Right(){return this.turn90Right;}

	public void setTurn90Right(bool turn90Right){this.turn90Right = turn90Right;}

	public bool getTurn90Left(){return this.turn90Left;}
	
	public void setTurn90Left(bool turn90Left){this.turn90Left = turn90Left;}

} // end of PlayerAction class
