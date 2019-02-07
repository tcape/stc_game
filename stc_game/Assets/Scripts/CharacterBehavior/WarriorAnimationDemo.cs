using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public enum Warrior{
	Karate,
	Ninja,
	Brute,
	Sorceress,
	Knight,
	Mage,
	Archer,
	TwoHanded, 
	Swordsman,
	Spearman,
	Hammer,
	Crossbow
}

public class WarriorAnimationDemo : MonoBehaviour{
	[HideInInspector]
	public Animator animator;
    public NavMeshAgent agent;
    public Camera camera;
	public Warrior warrior;
	private IKHands ikhands;
	Rigidbody rigidBody;
	public GameObject target;
	public GameObject weaponModel;
	public GameObject secondaryWeaponModel;
	float rotationSpeed = 15f;
	public float gravity = -9.83f;
	public float runSpeed = 8f;
	public float walkSpeed = 3f;
	public float strafeSpeed = 3f;
	bool canMove = true;
	//jumping variables
	public float jumpSpeed = 8f;
	bool jumpHold = false;
	[HideInInspector]
	public bool canJump = true;
	float fallingVelocity = -2;
	bool isFalling = false;
	// Used for continuing momentum while in air
	public float inAirSpeed = 8f;
	float maxVelocity = 2f;
	float minVelocity = -2f;
	[HideInInspector]
	public Vector3 newVelocity;
	Vector3 inputVec;
	Vector3 dashInputVec;
	Vector3 targetDirection;
	bool isDashing = false;
	[HideInInspector]
	public bool isGrounded = true;
	[HideInInspector]
	public bool dead = false;
	bool isStrafing;
	[HideInInspector]
	public bool isAiming;
	bool aimingGui;
	[HideInInspector]
	public bool isBlocking = false;
	[HideInInspector]
	public bool isStunned = false;
	[HideInInspector]
	public bool isSitting = false;
	[HideInInspector]
	public bool inBlock;
	[HideInInspector]
	public bool blockGui;
	[HideInInspector]
	public bool weaponSheathed;
	[HideInInspector]
	public bool weaponSheathed2;
	bool isInAir;
	[HideInInspector]
	public bool isStealth;
	public float stealthSpeed;
	[HideInInspector]
	public bool isWall;
	[HideInInspector]
	public bool ledgeGui;
	[HideInInspector]
	public bool ledge;
	public float ledgeSpeed;
	[HideInInspector]
	public int attack = 0;
	bool canChain;
	[HideInInspector]
	public bool specialAttack2Bool;

	void Start(){
		animator = this.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        //      agent = GetComponent<NavMeshAgent>();
        if (warrior == Warrior.Archer){
			secondaryWeaponModel.gameObject.SetActive(false);
		}
		//sets the weight on any additional layers to 0
		if(warrior == Warrior.Archer || warrior == Warrior.Crossbow){
			if(animator.layerCount >= 1){
				animator.SetLayerWeight(1, 0);
			}
		}
		//sets the weight on any additional layers to 0
		if(warrior == Warrior.TwoHanded){
			secondaryWeaponModel.GetComponent<Renderer>().enabled = false;
			ikhands = this.gameObject.GetComponent<IKHands>();
		}
	}

	#region Updates

	void FixedUpdate(){
		CheckForGrounded();
		//gravity
		rigidBody.AddForce(0, gravity, 0, ForceMode.Acceleration);
		if(!isGrounded){
			AirControl();
		}
		//check if character can move
		if(canMove){
			UpdateMovement();  
		}
		if(rigidBody.velocity.y < fallingVelocity){
			isFalling = true;
		} 
		else{
			isFalling = false;
		}
	}

	void LateUpdate(){
		//Get local velocity of charcter
		float velocityXel = transform.InverseTransformDirection(rigidBody.velocity).x;
		float velocityZel = transform.InverseTransformDirection(rigidBody.velocity).z;
		//Update animator with movement values
		animator.SetFloat("Input X", velocityXel / runSpeed);
		animator.SetFloat("Input Z", velocityZel / runSpeed);
	}

	void Update(){
		CameraRelativeInput();
		InAir();
		JumpingUpdate();
		//Pause
		if(Input.GetKeyDown(KeyCode.P)){
			Debug.Break();
		}
		//if character isn't dead, blocking, or stunned (or in a move)
		if(!dead || !blockGui || !isBlocking){
			if(!weaponSheathed){
				if(!blockGui){
					if(Input.GetAxis("TargetBlock") < -0.1){
						if(!inBlock && !isInAir && attack == 0){
							animator.SetBool("Block", true);
							isBlocking = true;
							animator.SetBool("Running", false);
							animator.SetBool("Moving", false);
							newVelocity = new Vector3(0, 0, 0);
						}
					}
					if(Input.GetAxis("TargetBlock") == 0){
						inBlock = false;
						isBlocking = false;
						animator.SetBool("Block", false);
					}
				}
				//Character is not blocking
				if(!isBlocking){
					if(Input.GetButtonDown("Fire1") && attack <= 3){
						AttackChain();
					}
					if(!isInAir){
						if(Input.GetButtonDown("Jump") && canJump){
							StartCoroutine(_Jump(0.8f));
						}
						if(attack == 0){
							if(Input.GetButtonDown("Fire0")){
								RangedAttack();
							}
							if(Input.GetButtonDown("Fire2")){
								MoveAttack();
							}
							if(Input.GetButtonDown("Fire3")){
								SpecialAttack();
							}
						}
						if(Input.GetButtonDown("LightHit")){
							StartCoroutine(_GetHit());
						}
					}
				}
				//Character is blocking, all buttons perform block hit react
				else{
					if(Input.GetButtonDown("Jump")){
						StartCoroutine(_BlockHitReact());
					}
					if(Input.GetButtonDown("Fire0")){
						StartCoroutine(_BlockHitReact());
					}
					if(Input.GetButtonDown("Fire1")){
						StartCoroutine(_BlockHitReact());
					}
					if(Input.GetButtonDown("Fire2")){
						StartCoroutine(_BlockHitReact());
					}
					if(Input.GetButtonDown("Fire3")){
						StartCoroutine(_BlockHitReact());
					}
					if(Input.GetButtonDown("LightHit")){
						StartCoroutine(_BlockBreak());
					}
					if(Input.GetButtonDown("Death")){
						StartCoroutine(_BlockBreak());
					}
				}
				if(Input.GetAxis("DashVertical") > 0.5 || Input.GetAxis("DashVertical") < -0.5 || Input.GetAxis("DashHorizontal") > 0.5 || Input.GetAxis("DashHorizontal") < -0.5){
					if(!isDashing && !isInAir){
						StartCoroutine(_DirectionalDash());
					}
				}
			}
		}
		//character is dead or blocking, stop character
		else{
			newVelocity = new Vector3(0, 0, 0);
			inputVec = new Vector3(0,0,0);
		}
		if(!dead){
			if(!isBlocking){
				if(Input.GetButtonDown("Death")){
					Dead();
				}
			}
		}
		else{
			if(Input.GetButtonDown("Death")){
				StartCoroutine(_Revive());
			}
		}
		if(Input.GetButtonDown("Special")){
			if(warrior == Warrior.Ninja){
				if(!isStealth){
					isStealth = true;
					animator.SetBool("Stealth", true);
				} 
				else{
					isStealth = false;
					animator.SetBool("Stealth", false);
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.T)){
			if(Time.timeScale == 1){
				Time.timeScale = 0.25f;
			}
			else{
				Time.timeScale = 1;
			}
		}

        
    }

	void CameraRelativeInput(){
		if(!isStunned){
			float inputHorizontal = Input.GetAxisRaw("Horizontal");
			float inputVertical = Input.GetAxisRaw("Vertical");
			float inputDashHorizontal = Input.GetAxisRaw("DashHorizontal");
			float inputDashVertical = Input.GetAxisRaw("DashVertical");
			//Camera relative movement
			Transform cameraTransform = Camera.main.transform;
			//Forward vector relative to the camera along the x-z plane   
			Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
			forward.y = 0;
			forward = forward.normalized;
			//Right vector relative to the camera always orthogonal to the forward vector
			Vector3 right = new Vector3(forward.z, 0, -forward.x);
			inputVec = inputHorizontal * right + inputVertical * forward;
			dashInputVec = inputDashHorizontal * right + inputDashVertical * forward;
			if(!isBlocking){
				//if there is some input (account for controller deadzone)
				if(inputVertical > 0.1 || inputVertical < -0.1 || inputHorizontal > 0.1 || inputHorizontal < -0.1){
					//set that character is moving
					animator.SetBool("Moving", true);
					animator.SetBool("Running", true);
					//if targetting/strafing
					if(Input.GetKey(KeyCode.LeftShift) || Input.GetAxisRaw("TargetBlock") > 0.1){
						if(weaponSheathed != true){
							isStrafing = true;
							animator.SetBool("Running", false);
						}
					}
					else{
						isStrafing = false;
						animator.SetBool("Running", true);
					}
				}
				else{
					//character is not moving
					animator.SetBool("Moving", false);
					animator.SetBool("Running", false);
				}
			}
		}
	}

	float UpdateMovement(){
		Vector3 motion = inputVec;
		if(isGrounded){
			if(!dead && !isBlocking && !blockGui && !isStunned){
				//character is not strafing
				if(!isStrafing){
					newVelocity = motion.normalized * runSpeed;
				}
				//character is strafing
				else{
					newVelocity = motion.normalized * strafeSpeed;
				}
				if(ledge){
					newVelocity = motion.normalized * ledgeSpeed;
				}
				if(isStealth){
					newVelocity = motion.normalized * stealthSpeed;
				}
			}
			//no input, character not moving
			else{
				newVelocity = new Vector3(0,0,0);
				inputVec = new Vector3(0,0,0);
			}
		}
		//if character is falling
		else{
			newVelocity = rigidBody.velocity;
		}
		// limit velocity to x and z, by maintaining current y velocity:
		newVelocity.y = rigidBody.velocity.y;
		rigidBody.velocity = newVelocity;
		if(!isStrafing  && !isWall){
			//if not strafing, face character along input direction
			if(!ledgeGui || !ledge){
                RotateTowardMovementDirection();
			}
		}
		//if targetting button is held look at the target
		if(isStrafing){
			Quaternion targetRotation;
			float rotationSpeed = 40f;
			targetRotation = Quaternion.LookRotation(target.transform.position - new Vector3(transform.position.x, 0, transform.position.z));
			transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, (rotationSpeed * Time.deltaTime) * rotationSpeed);
		}
		//return a movement value for the animator
		return inputVec.magnitude;
	}

	//face character along input direction
	void RotateTowardMovementDirection(){
		//if character is none of these things
		if(!dead && !blockGui && !isBlocking && !isStunned){
			//take the camera orientated input vector and apply it to our characters facing with smoothing
			if(inputVec != Vector3.zero){
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputVec), Time.deltaTime * rotationSpeed);
			}
		}
	}

	#endregion

	#region Jumping

	void CheckForGrounded(){
		float distanceToGround;
		float threshold = 0.45f;
		RaycastHit hit;
		Vector3 offset = new Vector3(0, 0.4f, 0);
		if(Physics.Raycast((transform.position + offset), -Vector3.up, out hit, 100f)){
			distanceToGround = hit.distance;
			if(distanceToGround < threshold){
				isGrounded = true;
				isInAir = false;
			}
			else{
				isGrounded = false;
				isInAir = true;
			}
		}
	}

	void JumpingUpdate(){
		if(!jumpHold){
			//If the character is on the ground
			if(isGrounded){
				//set the animation back to idle
				animator.SetInteger("Jumping", 0);
				canJump = true;
			}
			else{
				//character is falling
				if(!ledge){
					if(isFalling){
						animator.SetInteger("Jumping", 2);
						canJump = false;
					}
				}
			}
		}
	}

	//Take the current character velocity and add jump movement
	public IEnumerator _Jump(float jumpTime){
		animator.SetTrigger("JumpTrigger");
		canJump = false;
        agent.isStopped = true;
		rigidBody.velocity += jumpSpeed * Vector3.up;
		animator.SetInteger("Jumping", 1);
		yield return new WaitForSeconds(jumpTime);
	}

	void AirControl(){
		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");
		Vector3 inputVec = new Vector3(x, 0, z);
		Vector3 motion = inputVec;
		motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? 0.7f : 1;
		//allow some control the air
		rigidBody.AddForce(motion * inAirSpeed, ForceMode.Acceleration);
		//limit the amount of velocity we can achieve
		float velocityX = 0;
		float velocityZ = 0;
		if(rigidBody.velocity.x > maxVelocity){
			velocityX = rigidBody.velocity.x - maxVelocity;
			if(velocityX < 0){
				velocityX = 0;
			}
			rigidBody.AddForce(new Vector3(-velocityX, 0, 0), ForceMode.Acceleration);
		}
		if(rigidBody.velocity.x < minVelocity){
			velocityX = rigidBody.velocity.x - minVelocity;
			if (velocityX > 0){
				velocityX = 0;
			}
			rigidBody.AddForce(new Vector3(-velocityX, 0, 0), ForceMode.Acceleration);
		}
		if(rigidBody.velocity.z > maxVelocity){
			velocityZ = rigidBody.velocity.z - maxVelocity;
			if(velocityZ < 0){
				velocityZ = 0;
			}
			rigidBody.AddForce(new Vector3(0, 0, -velocityZ), ForceMode.Acceleration);
		}
		if(rigidBody.velocity.z < minVelocity){
			velocityZ = rigidBody.velocity.z - minVelocity;
			if (velocityZ > 0){
				velocityZ = 0;
			}
			rigidBody.AddForce(new Vector3(0, 0, -velocityZ), ForceMode.Acceleration);
		}
	}

	void InAir(){
		if(isInAir){
			if(ledgeGui){
				animator.SetTrigger("Ledge-Catch");
				ledge = true;
			}
		}
	}

	#endregion

	#region Buttons

	public void AttackChain(){
		if(isInAir){
			StartCoroutine(_JumpAttack1());
		}
		//if charater is not in air, do regular attack
		else if(attack == 0){
			StartCoroutine(_Attack1());
		}
		//if within chain time
		else if(canChain){
			if(warrior != Warrior.Archer){
				if(attack == 1){
					StartCoroutine(_Attack2());
				}
				else if(attack == 2){
					StartCoroutine(_Attack3());
				}
				else{
					//do nothing
				}
			}
		}
		else{
			//do nothing
		}
	}

	IEnumerator _Attack1(){
		StopAllCoroutines();
		canChain = false;
		animator.SetInteger("Attack", 1);
		attack = 1;
		if(warrior == Warrior.Knight){
			StartCoroutine(_ChainWindow(0.1f, .8f));
			StartCoroutine(_LockMovementAndAttack(0.6f));
		}
		else if(warrior == Warrior.TwoHanded){
			StartCoroutine(_ChainWindow(0.6f, 1f));
			StartCoroutine(_LockMovementAndAttack(0.85f));
		}
		else if(warrior == Warrior.Brute){
			StartCoroutine(_ChainWindow(0.5f, 0.5f));
			StartCoroutine(_LockMovementAndAttack(1.1f));
		}
		else if(warrior == Warrior.Sorceress){
			StartCoroutine(_ChainWindow(0.3f, 1.4f));
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
		else if(warrior == Warrior.Swordsman){
			StartCoroutine(_ChainWindow(0.6f, 1.1f));
			StartCoroutine(_LockMovementAndAttack(0.9f));
		}
		else if(warrior == Warrior.Spearman){
			StartCoroutine(_ChainWindow(0.2f, 0.8f));
			StartCoroutine(_LockMovementAndAttack(1.1f));
		}
		else if(warrior == Warrior.Hammer){
			StartCoroutine(_ChainWindow(0.6f, 1.2f));
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_LockMovementAndAttack(0.7f));
		}
		else if(warrior == Warrior.Mage){
			StartCoroutine(_ChainWindow(0.4f, 1.2f));
			StartCoroutine(_LockMovementAndAttack(1.1f));
		}
		else if(warrior == Warrior.Archer){
			StartCoroutine(_LockMovementAndAttack(0.7f));
		}
		else{
			StartCoroutine(_ChainWindow(0.2f, 0.7f));
			StartCoroutine(_LockMovementAndAttack(0.6f));
		}
		yield return null;
	}

	IEnumerator _Attack2(){
		StopAllCoroutines();
		canChain = false;
		animator.SetInteger("Attack", 2);
		attack = 2;
		if(warrior == Warrior.Knight){
			StartCoroutine(_ChainWindow(0.4f, 0.9f));
			StartCoroutine(_LockMovementAndAttack(0.5f));
		}
		else if(warrior == Warrior.TwoHanded){
			StartCoroutine(_ChainWindow(0.5f, 0.8f));
			StartCoroutine(_LockMovementAndAttack(0.75f));
		}
		else if(warrior == Warrior.Brute){
			StartCoroutine(_ChainWindow(0.3f, 0.7f));
			StartCoroutine(_LockMovementAndAttack(1.4f));
		}
		else if(warrior == Warrior.Sorceress){
			StartCoroutine(_ChainWindow(0.6f, 1.2f));
		}
		else if(warrior == Warrior.Karate){
			StartCoroutine(_ChainWindow(0.3f, 0.6f));
			StartCoroutine(_LockMovementAndAttack(0.9f));
		}
		else if(warrior == Warrior.Swordsman){
			StartCoroutine(_ChainWindow(0.6f, 1.1f));
			StartCoroutine(_LockMovementAndAttack(1.1f));
		}
		else if(warrior == Warrior.Spearman){
			StartCoroutine(_ChainWindow(0.6f, 1.1f));
			StartCoroutine(_LockMovementAndAttack(1.1f));
		}
		else if(warrior == Warrior.Hammer){
			StartCoroutine(_ChainWindow(0.6f, 1.2f));
			StartCoroutine(_LockMovementAndAttack(1.4f));
		}
		else if(warrior == Warrior.Mage){
			StartCoroutine(_ChainWindow(0.4f, 1.2f));
			StartCoroutine(_LockMovementAndAttack(1.3f));
		}
		else if(warrior == Warrior.Ninja){
			StartCoroutine(_ChainWindow(0.2f, 0.8f));
			StartCoroutine(_LockMovementAndAttack(0.8f));
		}
		else{
			StartCoroutine(_ChainWindow(0.1f, 2f));
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
		yield return null;
	}

	IEnumerator _Attack3(){
		StopAllCoroutines();
		animator.SetInteger("Attack", 3);
		attack = 3;
		if(warrior == Warrior.Knight){
			StartCoroutine(_LockMovementAndAttack(0.8f));
		}
		if(warrior == Warrior.Swordsman){
			StartCoroutine(_LockMovementAndAttack(1.2f));
		} 
		else if(warrior == Warrior.Hammer){
			StartCoroutine(_LockMovementAndAttack(1.5f));
		} 
		else if(warrior == Warrior.Karate){
			StartCoroutine(_LockMovementAndAttack(0.8f));
		} 
		else if(warrior == Warrior.Brute){
			StartCoroutine(_LockMovementAndAttack(1.7f));
		} 
		else if(warrior == Warrior.TwoHanded){
			StartCoroutine(_LockMovementAndAttack(1f));
		} 
		else if(warrior == Warrior.Mage){
			StartCoroutine(_LockMovementAndAttack(1f));
		} 
		else{
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
		canChain = false;
		yield return null;
	}

	public void RangedAttack(){
		StopAllCoroutines();
		animator.SetTrigger("RangeAttack1Trigger");
		attack = 4;
		if(warrior == Warrior.Brute){
			StartCoroutine(_LockMovementAndAttack(2.4f));
		} 
		else if(warrior == Warrior.Mage){
			StartCoroutine(_LockMovementAndAttack(1.7f));
		}
		else if(warrior == Warrior.Ninja){
			StartCoroutine(_LockMovementAndAttack(0.9f));
		}
		else if(warrior == Warrior.Archer){
			if(!isAiming){
				StartCoroutine(_SetLayerWeightForTime(0.6f));
				StartCoroutine(_ArcherArrowOn(0.2f));
			}
			else{
				StartCoroutine(_ArcherArrowOff(0.2f));
			}
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_SetLayerWeightForTime(0.6f));
			StartCoroutine(_ArcherArrowOn(0.2f));
		}
		else if(warrior == Warrior.TwoHanded){
			StartCoroutine(_LockMovementAndAttack(2.4f));
			StartCoroutine(_SecondaryWeaponVisibility(0.7f, true));
			StartCoroutine(_WeaponVisibility(0.7f, false));
			StartCoroutine(_SecondaryWeaponVisibility(2f, false));
			StartCoroutine(_WeaponVisibility(2f, true));
		}
		else if(warrior == Warrior.Hammer){
			StartCoroutine(_LockMovementAndAttack(1.7f));
		}
		else if(warrior == Warrior.Knight){
			StartCoroutine(_LockMovementAndAttack(1f));
		}
		else{
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
	}

	public void MoveAttack(){
		StopAllCoroutines();
		attack = 5;
		animator.SetTrigger("MoveAttack1Trigger");
		if(warrior == Warrior.Brute){
			StartCoroutine(_LockMovementAndAttack(1.4f));
		}
		else if(warrior == Warrior.Sorceress){
			StartCoroutine(_LockMovementAndAttack(1.1f));
		}
		else if(warrior == Warrior.Mage){
			StartCoroutine(_LockMovementAndAttack(1.5f));
		}
		else if(warrior == Warrior.TwoHanded){
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
		else if(warrior == Warrior.Hammer){
			StartCoroutine(_LockMovementAndAttack(2.5f));
		}
		else if(warrior == Warrior.Knight){
			StartCoroutine(_LockMovementAndAttack(0.9f));
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_LockMovementAndAttack(1f));
		}
		else{
			StartCoroutine(_LockMovementAndAttack(0.9f));
		}
	}

	public void SpecialAttack(){
		StopAllCoroutines();
		attack = 6;
		animator.SetTrigger("SpecialAttack1Trigger");
		if(warrior == Warrior.Brute){
			StartCoroutine(_LockMovementAndAttack(2f));
		}
		else if(warrior == Warrior.Sorceress){
			StartCoroutine(_LockMovementAndAttack(1.5f));
		}
		else if(warrior == Warrior.Knight){
			StartCoroutine(_LockMovementAndAttack(1.1f));
		}
		else if(warrior == Warrior.Mage){
			StartCoroutine(_LockMovementAndAttack(1.95f));
		}
		else if(warrior == Warrior.TwoHanded){
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
		else if(warrior == Warrior.Swordsman){
			StartCoroutine(_LockMovementAndAttack(1f));
		}
		else if(warrior == Warrior.Spearman){
			StartCoroutine(_LockMovementAndAttack(0.9f));
		}
		else if(warrior == Warrior.Hammer){
			StartCoroutine(_LockMovementAndAttack(1.6f));
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_LockMovementAndAttack(1f));
		}
		else{
			StartCoroutine(_LockMovementAndAttack(1.7f));
		}
	}

	public IEnumerator _JumpAttack1(){
		yield return new WaitForFixedUpdate();
		jumpHold = true;
		rigidBody.velocity += jumpSpeed * -Vector3.up;
		animator.SetTrigger("JumpAttack1Trigger");
		if(warrior == Warrior.Knight){
			StartCoroutine(_LockMovementAndAttack(1f));
			yield return new WaitForSeconds(0.5f);
		}
		else if(warrior == Warrior.Hammer){
			StartCoroutine(_LockMovementAndAttack(1.2f));
			yield return new WaitForSeconds(0.7f);
		}
		else if(warrior == Warrior.Spearman){
			StartCoroutine(_LockMovementAndAttack(0.9f));
			yield return new WaitForSeconds(0.7f);
		}
		else{
			StartCoroutine(_LockMovementAndAttack(1f));
			yield return new WaitForSeconds(0.8f);
		}
		jumpHold = false;
	}

	#endregion

	#region Dashing

	public IEnumerator _DirectionalDash()
	{
		//check which way the dash is pressed relative to the character facing
		float angle = Vector3.Angle(dashInputVec, transform.forward);
		float sign = Mathf.Sign(Vector3.Dot(transform.up,Vector3.Cross(dashInputVec,transform.forward)));
		// angle in [-179,180]
		float signed_angle = angle * sign;
		//angle in 0-360
		float angle360 = (signed_angle + 180) % 360;
		//deternime the animation to play based on the angle
		if(angle360 > 315 || angle360 < 45)
		{
			StartCoroutine(_Dash(1));
		}
		if(angle360 > 45 && angle360 < 135)
		{
			StartCoroutine(_Dash(2));
		}
		if(angle360 > 135 && angle360 < 225)
		{
			StartCoroutine(_Dash(3));
		}
		if(angle360 > 225 && angle360 < 315)
		{
			StartCoroutine(_Dash(4));
		}
		yield return null;
	}

	public IEnumerator _Dash(int dashDirection){
		isDashing = true;
		animator.SetInteger("Dash", dashDirection);
		if(warrior == Warrior.Brute){
			StartCoroutine(_LockMovementAndAttack(1f));
		}
		else if(warrior == Warrior.Karate){
			StartCoroutine(_LockMovementAndAttack(0.7f));
		}
		else if(warrior == Warrior.Knight){
			StartCoroutine(_LockMovementAndAttack(1.15f));
		}
		else if(warrior == Warrior.Archer){
			StartCoroutine(_LockMovementAndAttack(0.6f));
		}
		else if(warrior == Warrior.Mage){
			StartCoroutine(_LockMovementAndAttack(0.8f));
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_LockMovementAndAttack(0.55f));
		}
		else if(warrior == Warrior.Hammer){
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
		else{
			StartCoroutine(_LockMovementAndAttack(0.65f));
		}
		yield return new WaitForSeconds(.1f);
		animator.SetInteger("Dash", 0);
		isDashing = false;
	}

	public IEnumerator _Dash2(int dashDirection){
		isDashing = true;
		animator.SetInteger("Dash2", dashDirection);
		yield return new WaitForEndOfFrame();
		animator.SetInteger("Dash2", dashDirection);
		yield return new WaitForEndOfFrame();
		StartCoroutine(_LockMovementAndAttack(0.45f));
		animator.SetInteger("Dash2", 0);
		yield return new WaitForSeconds(0.95f);
		isDashing = false;
	}

	#endregion

	#region Misc

	IEnumerator _SetInAir(float timeToStart, float lenthOfTime){
		yield return new WaitForSeconds(timeToStart);
		isInAir = true;
		yield return new WaitForSeconds(lenthOfTime);
		isInAir = false;
	}

	public IEnumerator _ChainWindow(float timeToWindow, float chainLength){
		yield return new WaitForSeconds(timeToWindow);
		canChain = true;
		animator.SetInteger("Attack", 0);
		yield return new WaitForSeconds(chainLength);
		canChain = false;
	}

	public IEnumerator _LockMovementAndAttack(float pauseTime){
		isStunned = true;
		animator.applyRootMotion = true;
		inputVec = new Vector3(0, 0, 0);
		newVelocity = new Vector3(0, 0, 0);
		animator.SetFloat("Input X", 0);
		animator.SetFloat("Input Z", 0);
		animator.SetBool("Moving", false);
		yield return new WaitForSeconds(pauseTime);
		animator.SetInteger("Attack", 0);
		canChain = false;
		isStunned = false;
		animator.applyRootMotion = false;
		//small pause to let blending finish
		yield return new WaitForSeconds(0.2f);
		attack = 0;
	}

	public void SheathWeapon(){
		animator.SetTrigger("WeaponSheathTrigger");
		if(warrior == Warrior.Archer){
			StartCoroutine(_WeaponVisibility(0.4f, false));
		} 
		else if(warrior == Warrior.Swordsman){
			StartCoroutine(_WeaponVisibility(0.4f, false));
			StartCoroutine(_SecondaryWeaponVisibility(0.4f, false));
		} 
		else if(warrior == Warrior.Spearman){
			StartCoroutine(_WeaponVisibility(0.26f, false));
		} 
		else if(warrior == Warrior.TwoHanded){
			StartCoroutine(_WeaponVisibility(0.5f, false));
			StartCoroutine(_BlendIKHandLeftRot(0, 0.3f, 0));
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_LockMovementAndAttack(1.1f));
		}
		else if(warrior == Warrior.Ninja){
			StartCoroutine(_LockMovementAndAttack(1.4f));
			StartCoroutine(_WeaponVisibility(0.5f, false));
		}
		else{
			StartCoroutine(_LockMovementAndAttack(1.4f));
			StartCoroutine(_WeaponVisibility(0.5f, false));
		}
		weaponSheathed = true;
	}

	public void UnSheathWeapon(){
		animator.SetTrigger("WeaponUnsheathTrigger");
		if(warrior == Warrior.Archer){
			StartCoroutine(_WeaponVisibility(0.4f, true));
		}
		else if(warrior == Warrior.TwoHanded){
			StartCoroutine(_WeaponVisibility(0.35f, true));
		}
		else if(warrior == Warrior.Swordsman){
			StartCoroutine(_WeaponVisibility(0.35f, true));
			StartCoroutine(_SecondaryWeaponVisibility(0.35f, true));
		}
		else if(warrior == Warrior.Spearman){
			StartCoroutine(_WeaponVisibility(0.45f, true));
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_WeaponVisibility(0.6f, true));
			StartCoroutine(_LockMovementAndAttack(1f));
		}
		else{
			StartCoroutine(_WeaponVisibility(0.6f, true));
			StartCoroutine(_LockMovementAndAttack(1.4f));
		}
		weaponSheathed = false;
	}

	public IEnumerator _WeaponVisibility(float waitTime, bool weaponVisiblity){
		yield return new WaitForSeconds(waitTime);
		weaponModel.SetActive(weaponVisiblity);
		if(secondaryWeaponModel != null){
			secondaryWeaponModel.SetActive(weaponVisiblity);
		}
	}

	IEnumerator _SecondaryWeaponVisibility(float waitTime, bool weaponVisiblity){
		yield return new WaitForSeconds(waitTime);
		secondaryWeaponModel.GetComponent<Renderer>().enabled = weaponVisiblity;
	}

	public IEnumerator _ArcherArrowOn(float waitTime){
		if(warrior != Warrior.Crossbow){
			secondaryWeaponModel.gameObject.SetActive(true);
		}
		yield return new WaitForSeconds(waitTime);
		if(warrior != Warrior.Crossbow){
			secondaryWeaponModel.gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(0.2f);
		animator.SetInteger("Attack", 0);
		attack = 0;
	}

	public IEnumerator _ArcherArrowOff(float waitTime){
		if(warrior != Warrior.Crossbow){
			secondaryWeaponModel.gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(waitTime);
		if(warrior != Warrior.Crossbow){
			secondaryWeaponModel.gameObject.SetActive(true);
		}
		yield return new WaitForSeconds(0.2f);
		animator.SetInteger("Attack", 0);
		attack = 0;
	}

	public IEnumerator _SetLayerWeightForTime(float time){
		animator.SetLayerWeight(1, 1);
		yield return new WaitForSeconds(time);
		float a = 1;
		for(int i = 0; i < 20; i++){
			a -= 0.05f;
			animator.SetLayerWeight(1, a);
			yield return new WaitForEndOfFrame();
		}
		animator.SetLayerWeight(1, 0);
	}

	public IEnumerator _SetLayerWeight(float amount){
		animator.SetLayerWeight(1, amount);
		yield return null;
	}

	public IEnumerator _BlockHitReact(){
		StartCoroutine(_LockMovementAndAttack(0.5f));
		animator.SetTrigger("BlockHitReactTrigger");
		yield return null;
	}

	public IEnumerator _BlockBreak(){
		StartCoroutine(_LockMovementAndAttack(1f));
		animator.SetTrigger("BlockBreakTrigger");
		yield return null;
	}

	public IEnumerator _GetHit(){
		animator.SetTrigger("LightHitTrigger");
		if(warrior == Warrior.Ninja){
			StartCoroutine(_LockMovementAndAttack(2.4f));
		}
		else if(warrior == Warrior.Archer){
			StartCoroutine(_LockMovementAndAttack(2.7f));
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_LockMovementAndAttack(2.5f));
		}
		else{
			StartCoroutine(_LockMovementAndAttack(2.8f));
		}
		yield return null;
	}

	void Dead(){
		animator.applyRootMotion = true;
		animator.SetTrigger("DeathTrigger");
		dead = true;
	}

	public IEnumerator _Revive(){
		animator.SetTrigger("ReviveTrigger");
		if(warrior == Warrior.Brute){
			StartCoroutine(_LockMovementAndAttack(1.75f));
		}
		else if(warrior == Warrior.Mage){
			StartCoroutine(_LockMovementAndAttack(1.2f));
		}
		else if(warrior == Warrior.Sorceress){
			StartCoroutine(_LockMovementAndAttack(0.7f));
		}
		else if(warrior == Warrior.Ninja){
			StartCoroutine(_LockMovementAndAttack(0.9f));
		}
		else if(warrior == Warrior.Crossbow){
			StartCoroutine(_LockMovementAndAttack(1.3f));
		}
		else{
			StartCoroutine(_LockMovementAndAttack(1.1f));
			yield return null;
		}
		dead = false;
	}

	//Placeholder functions for Animation events
	public void Hit(){
	}
	
	public void Shoot(){
	}
	
	public void FootR(){
	}
	
	public void FootL(){
	}
	
	public void Land(){
	}
	
	public void WeaponSwitch(){
	}

	#endregion

	#region IKHandsBlending

	IEnumerator _BlendIKHandLeftPos(float wait, float timeToBlend, float amount){
		yield return new WaitForSeconds(wait);
		float currentLeftPos = ikhands.leftHandPositionWeight;
		float diffOverTime = (Mathf.Abs(currentLeftPos) - amount) / timeToBlend;
		float time = 0f;
		if(currentLeftPos > amount){
			while(time < timeToBlend){
				time += Time.deltaTime;
				ikhands.leftHandPositionWeight -= diffOverTime;
				yield return null;
			}
		}
		if(currentLeftPos < amount){
			while(time < timeToBlend){
				time += Time.deltaTime;
				ikhands.leftHandPositionWeight += diffOverTime;
				yield return null;
			}
		}
	}

	IEnumerator _BlendIKHandRightPos(float wait, float timeToBlend, float amount){
		yield return new WaitForSeconds(wait);
		float currentRightPos = ikhands.rightHandPositionWeight;
		float diffOverTime = (Mathf.Abs(currentRightPos) - amount) / timeToBlend;
		float time = 0f;
		if(currentRightPos > amount){
			while(time < timeToBlend){
				time += Time.deltaTime;
				ikhands.rightHandPositionWeight -= diffOverTime;
				yield return null;
			}		
		}
		if(currentRightPos < amount){
			while(time < timeToBlend){
				time += Time.deltaTime;
				ikhands.rightHandPositionWeight += diffOverTime;
				yield return null;
			}
		}
	}

	IEnumerator _BlendIKHandLeftRot(float wait, float timeToBlend, float amount){
		yield return new WaitForSeconds(wait);
		float currentLeftRot = ikhands.leftHandRotationWeight;
		float diffOverTime = (Mathf.Abs(currentLeftRot) - amount) / timeToBlend;
		float time = 0f;
		while(time < timeToBlend){
			if(currentLeftRot > amount){
				ikhands.leftHandRotationWeight -= diffOverTime * Time.deltaTime;
				time += Time.deltaTime;
				yield return null;
			}
			if(currentLeftRot < amount){
				ikhands.leftHandRotationWeight += diffOverTime * Time.deltaTime;
				time += Time.deltaTime;
				yield return null;
			}
		}
	}

	IEnumerator _BlendIKHandRightRot(float wait, float timeToBlend, float amount){
		yield return new WaitForSeconds(wait);
		float currentRightRot = ikhands.rightHandRotationWeight;
		float diffOverTime = (Mathf.Abs(currentRightRot) - amount) / timeToBlend;
		float time = 0f;
		while(time < timeToBlend){
			if(currentRightRot > amount){
				ikhands.rightHandRotationWeight -= diffOverTime * Time.deltaTime;
				time += Time.deltaTime;
				yield return null;
			}
			if(currentRightRot < amount){
				ikhands.rightHandRotationWeight += diffOverTime * Time.deltaTime;
				time += Time.deltaTime;
				yield return null;
			}
		}
	}

	IEnumerator _BlendIKHandRight(float time, float amount){
		ikhands.rightHandPositionWeight = amount;
		ikhands.rightHandRotationWeight = amount;
		yield return new WaitForSeconds(time);
	}	

	#endregion
}