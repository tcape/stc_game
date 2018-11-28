using UnityEngine;
using System.Collections;

public class GUIControls : MonoBehaviour{
	WarriorAnimationDemo warriorAnimationDemo;
	bool blockGui;
	bool ledgeGui;
    public float speed;

	void Start(){
		warriorAnimationDemo = GetComponent<WarriorAnimationDemo>();
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = warriorAnimationDemo.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Move Agent
                warriorAnimationDemo.agent.SetDestination(hit.point);
                var speed = warriorAnimationDemo.agent.velocity.magnitude;
                if (speed > 0)
                {
                    warriorAnimationDemo.animator.SetBool("Moving", true);
                }
                warriorAnimationDemo.animator.SetFloat("Input Z", speed);
            }
        }
        //Vector3 pos = transform.position;

        //if (Input.GetKey("w"))
        //{
        //    pos.z += speed * Time.deltaTime;

        //}
        //if (Input.GetKey("s"))
        //{
        //    pos.z -= speed * Time.deltaTime;
        //}
        //if (Input.GetKey("d"))
        //{
        //    pos.x += speed * Time.deltaTime;
        //}
        //if (Input.GetKey("a"))
        //{
        //    pos.x -= speed * Time.deltaTime;
        //}


        //transform.position = pos;
    }

    void OnGUI(){
		if(!warriorAnimationDemo.dead){
			if(warriorAnimationDemo.warrior == Warrior.Mage || warriorAnimationDemo.warrior == Warrior.Ninja || warriorAnimationDemo.warrior == Warrior.Knight || warriorAnimationDemo.warrior == Warrior.Archer || warriorAnimationDemo.warrior == Warrior.TwoHanded || warriorAnimationDemo.warrior == Warrior.Swordsman || warriorAnimationDemo.warrior == Warrior.Spearman || warriorAnimationDemo.warrior == Warrior.Hammer || warriorAnimationDemo.warrior == Warrior.Crossbow){
				if(!warriorAnimationDemo.dead && warriorAnimationDemo.weaponSheathed & !warriorAnimationDemo.isSitting){
					if(GUI.Button(new Rect (30, 310, 100, 30), "Unsheath Weapon")){
						warriorAnimationDemo.UnSheathWeapon();
					}
					if(warriorAnimationDemo.warrior == Warrior.Ninja){
						if(GUI.Button(new Rect(30, 350, 100, 30), "Sit")){
							warriorAnimationDemo.isStunned = true;
							warriorAnimationDemo.isSitting = true;
							warriorAnimationDemo.animator.SetTrigger("Idle-Relax-ToSitTrigger");
						}
					}
				}
				if(warriorAnimationDemo.isSitting){
					if(GUI.Button(new Rect(30, 350, 100, 30), "Stand")){
						warriorAnimationDemo.animator.SetTrigger("Idle-Relax-FromSitTrigger");
						StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.2f));
						warriorAnimationDemo.isSitting = false;
					}
				}
			}
		}
		//if character isn't warriorAnimationDemo.dead or weapon is sheathed
		if(!warriorAnimationDemo.dead && !warriorAnimationDemo.weaponSheathed){
			//if character is not blocking
			if(warriorAnimationDemo.warrior == Warrior.Ninja){
				if(!warriorAnimationDemo.isBlocking && !blockGui){
					ledgeGui = GUI.Toggle(new Rect(245, 60, 100, 30), ledgeGui, "Ledge Jump");
					warriorAnimationDemo.ledgeGui = ledgeGui;
				}
				if(warriorAnimationDemo.ledge){
					if(GUI.Button(new Rect(245, 90, 100, 30), "Ledge Drop")){
						warriorAnimationDemo.animator.SetTrigger("Ledge-Drop");
						warriorAnimationDemo.ledge = false;
						warriorAnimationDemo.animator.SetBool("Ledge-Catch", false);
					}
					if(GUI.Button(new Rect(245, 20, 100, 30), "Ledge Climb")){
						warriorAnimationDemo.animator.SetTrigger("Ledge-Climb-Trigger");
						warriorAnimationDemo.ledge = false;
						warriorAnimationDemo.animator.SetBool("Ledge-Catch", false);
					}
				}
			}
			if(!warriorAnimationDemo.ledge){
				blockGui = GUI.Toggle(new Rect(25, 215, 50, 30), blockGui, "Block");
				warriorAnimationDemo.blockGui = blockGui;
				if(warriorAnimationDemo.warrior == Warrior.Archer){
					var aimingGui = GUI.Toggle(new Rect(80, 215, 50, 30), warriorAnimationDemo.isAiming, "Aiming");
					// Check if the toggle was toggled
					if(aimingGui != warriorAnimationDemo.isAiming){
						if(aimingGui == true){
							warriorAnimationDemo.animator.SetBool("Aiming", true);
							warriorAnimationDemo.animator.SetTrigger("AimTrigger");
							StartCoroutine(warriorAnimationDemo._ArcherArrowOn(0.2f));
							StartCoroutine(warriorAnimationDemo._SetLayerWeight(1));
						}
						else{
							warriorAnimationDemo.animator.SetBool("Aiming", false);
							StartCoroutine(warriorAnimationDemo._SetLayerWeight(0));
						}
						warriorAnimationDemo.isAiming = aimingGui;
					}
				}
			}
			if(!warriorAnimationDemo.ledge){
				if(!warriorAnimationDemo.isBlocking){
					if(!blockGui){
						warriorAnimationDemo.animator.SetBool("Block", false);
					} 
					else{
						warriorAnimationDemo.animator.SetBool("Block", true);
						warriorAnimationDemo.animator.SetFloat("Input X", 0);
						warriorAnimationDemo.animator.SetFloat("Input Z", -0);
						warriorAnimationDemo.newVelocity = new Vector3(0, 0, 0);
					}
				}
				if(blockGui){
					if(GUI.Button(new Rect(30, 240, 100, 30), "BlockHitReact")){
						StartCoroutine(warriorAnimationDemo._BlockHitReact());
					}
					if(GUI.Button(new Rect(30, 275, 100, 30), "BlockBreak")){
						StartCoroutine(warriorAnimationDemo._BlockBreak());
					}
				} 
				else if(!warriorAnimationDemo.inBlock){
					if(!warriorAnimationDemo.inBlock){
						if(GUI.Button(new Rect(30, 240, 100, 30), "Hit React")){
							StartCoroutine(warriorAnimationDemo._GetHit());
						}
					}
				}
			}
			if(!blockGui && !warriorAnimationDemo.isBlocking && !warriorAnimationDemo.ledge){
				if(GUI.Button(new Rect (25, 20, 100, 30), "Dash Forward")) 
					StartCoroutine(warriorAnimationDemo._Dash(1));
				if(GUI.Button(new Rect (135, 20, 100, 30), "Dash Right")) 
					StartCoroutine(warriorAnimationDemo._Dash(2));
				if(!warriorAnimationDemo.ledge){
					if(GUI.Button(new Rect(245, 20, 100, 30), "Jump")){
						if(warriorAnimationDemo.canJump == true && warriorAnimationDemo.isGrounded){
							StartCoroutine(warriorAnimationDemo._Jump(.8f));
						}
					}
				}
				if(GUI.Button(new Rect(25, 50, 100, 30), "Dash Backward")){
					StartCoroutine(warriorAnimationDemo._Dash(3));
				}
				if(GUI.Button(new Rect(135, 50, 100, 30), "Dash Left")){
					StartCoroutine(warriorAnimationDemo._Dash(4));
				}
				//2nd Dash/Roll animations for Knight
				if(warriorAnimationDemo.warrior == Warrior.Knight){
					if(GUI.Button(new Rect (355, 20, 100, 30), "Roll Forward"))
						StartCoroutine(warriorAnimationDemo._Dash2(1));
					if(GUI.Button(new Rect (355, 50, 100, 30), "Roll Backward")) 
						StartCoroutine(warriorAnimationDemo._Dash2(3));
					if(GUI.Button(new Rect (460, 20, 100, 30), "Roll Left")) 
						StartCoroutine(warriorAnimationDemo._Dash2(4));
					if(GUI.Button(new Rect (460, 50, 100, 30), "Roll Right")) 
						StartCoroutine(warriorAnimationDemo._Dash2(2));
				}
				//ATTACK CHAIN
				if(GUI.Button(new Rect(25, 85, 100, 30), "Attack Chain")){
					if(warriorAnimationDemo.attack <= 3){
						warriorAnimationDemo.AttackChain();
					}
				}
				if(warriorAnimationDemo.warrior == Warrior.Sorceress || warriorAnimationDemo.warrior == Warrior.Karate || warriorAnimationDemo.warrior == Warrior.Spearman){
					if(GUI.Button(new Rect(135, 85, 100, 30), "Attack 4")){
						warriorAnimationDemo.animator.SetInteger("Attack", 4);
						StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(.65f));
					}
				}
				if(warriorAnimationDemo.warrior == Warrior.Sorceress || warriorAnimationDemo.warrior == Warrior.Karate || warriorAnimationDemo.warrior == Warrior.Spearman){
					if(GUI.Button(new Rect(245, 85, 100, 30), "Attack 5")){
						warriorAnimationDemo.animator.SetInteger("Attack", 5);
						if(warriorAnimationDemo.warrior == Warrior.Spearman){
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.15f));
						}
						else{
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1f));
						}
					}
				}
				if(warriorAnimationDemo.warrior == Warrior.Sorceress || warriorAnimationDemo.warrior == Warrior.Karate){
					if(GUI.Button(new Rect(355, 85, 100, 30), "Attack 6")){
						warriorAnimationDemo.animator.SetInteger("Attack", 6);
						if(warriorAnimationDemo.warrior == Warrior.Sorceress){
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.25f));
						}
						if(warriorAnimationDemo.warrior == Warrior.Karate){
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.8f));
						}
					}
				}
				if(warriorAnimationDemo.warrior == Warrior.Sorceress || warriorAnimationDemo.warrior == Warrior.Karate){
					if(GUI.Button(new Rect(465, 85, 100, 30), "Attack 7")){
						warriorAnimationDemo.animator.SetInteger("Attack", 7);
						if(warriorAnimationDemo.warrior == Warrior.Sorceress){
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.25f));
						}
						if(warriorAnimationDemo.warrior == Warrior.Karate){
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.1f));
						}
					}
				}
				if(warriorAnimationDemo.warrior == Warrior.Karate){
					if(GUI.Button(new Rect(575, 85, 100, 30), "Attack 8")){
						warriorAnimationDemo.animator.SetInteger("Attack", 8);
						StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.7f));
					}
					if(GUI.Button(new Rect(685, 85, 100, 30), "Attack 9")){
						warriorAnimationDemo.animator.SetInteger("Attack", 9);
						StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.7f));
					}
				}
				if(warriorAnimationDemo.warrior == Warrior.Sorceress){
					if(GUI.Button(new Rect(585, 85, 100, 30), "Attack 8")){
						warriorAnimationDemo.animator.SetInteger("Attack", 8);
						StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.3f));
					}
				}
				if(warriorAnimationDemo.warrior == Warrior.Crossbow){
					if(GUI.Button(new Rect (135, 85, 100, 30), "Reload")){
						StartCoroutine(warriorAnimationDemo._SetLayerWeightForTime(1.2f));
						warriorAnimationDemo.animator.SetTrigger("Reload1Trigger");
					}
				}
				if(warriorAnimationDemo.warrior == Warrior.Ninja){
					if(GUI.Button(new Rect(135, 85, 100, 30), "Attack1_R")){
						if(warriorAnimationDemo.attack == 0){
							warriorAnimationDemo.animator.SetTrigger("Attack1RTrigger");
							warriorAnimationDemo.attack = 4;
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.8f));
						}
					}
					if(GUI.Button(new Rect(245, 85, 100, 30), "Attack2_R")){
						if(warriorAnimationDemo.attack == 0){
							warriorAnimationDemo.attack = 4;
							warriorAnimationDemo.animator.SetTrigger("Attack2RTrigger");
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.8f));
						}
					}
				}
				if(!blockGui && !warriorAnimationDemo.isBlocking){
					if(GUI.Button(new Rect(25, 115, 100, 30), "RangeAttack1")){
						if(warriorAnimationDemo.attack == 0){
							warriorAnimationDemo.RangedAttack();
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Ninja || warriorAnimationDemo.warrior == Warrior.Crossbow || warriorAnimationDemo.warrior == Warrior.Karate || warriorAnimationDemo.warrior == Warrior.Mage){
						if(GUI.Button(new Rect(135, 115, 100, 30), "RangeAttack2")){
							if(warriorAnimationDemo.warrior != Warrior.Karate){
								if(warriorAnimationDemo.attack == 0){
									warriorAnimationDemo.attack = 4;
									warriorAnimationDemo.animator.SetTrigger("RangeAttack2Trigger");
									if(warriorAnimationDemo.warrior == Warrior.Crossbow){
										//if character is Crossbow blend in the upper body animation
										if(warriorAnimationDemo.animator.GetBool("Moving") == true){
											StartCoroutine(warriorAnimationDemo._SetLayerWeightForTime(0.6f));
										} 
										else{
											StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.7f));
										}
										warriorAnimationDemo.animator.SetInteger("Attack", 0);
										warriorAnimationDemo.attack = 0;
									} 
									else{
										if(warriorAnimationDemo.warrior == Warrior.Mage){
											StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(2f));
										}
										else{
											StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.6f));
										}
									}
								}
							} 
							else{
								warriorAnimationDemo.animator.SetTrigger("RangeAttack2Trigger");
								StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1f));
							}
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Ninja){
						if(GUI.Button(new Rect(245, 115, 100, 30), "RangeAttack3")){
							if(warriorAnimationDemo.attack == 0){
								warriorAnimationDemo.attack = 4;
								warriorAnimationDemo.animator.SetTrigger("RangeAttack3Trigger");
								StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.9f));
							}
						}
					}
					if(GUI.Button(new Rect(25, 145, 100, 30), "MoveAttack1")){
						if(warriorAnimationDemo.attack == 0){
							warriorAnimationDemo.MoveAttack();
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Archer || warriorAnimationDemo.warrior == Warrior.Sorceress || warriorAnimationDemo.warrior == Warrior.Karate){
						if(GUI.Button(new Rect(135, 145, 100, 30), "MoveAttack2")){
							if(warriorAnimationDemo.warrior == Warrior.Archer){
								if(warriorAnimationDemo.attack == 0){
									warriorAnimationDemo.attack = 4;
									warriorAnimationDemo.animator.SetTrigger("MoveAttack2Trigger");
									StartCoroutine(warriorAnimationDemo._ArcherArrowOn(0.6f));
									StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.1f));
								}
							}
							if(warriorAnimationDemo.warrior == Warrior.Sorceress){
								warriorAnimationDemo.animator.SetTrigger("MoveAttack2Trigger");
								StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.3f));
							}
							if(warriorAnimationDemo.warrior == Warrior.Karate){
								warriorAnimationDemo.animator.SetTrigger("MoveAttack2Trigger");
								StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1f));
							}
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Sorceress){
						if(GUI.Button(new Rect(245, 145, 100, 30), "MoveAttack3")){
							warriorAnimationDemo.animator.SetTrigger("MoveAttack3Trigger");
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1f));
						}
					}
					if(GUI.Button(new Rect(25, 175, 100, 30), "SpecialAttack1")){
						if(warriorAnimationDemo.attack == 0){
							warriorAnimationDemo.SpecialAttack();
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Ninja || warriorAnimationDemo.warrior == Warrior.Sorceress || warriorAnimationDemo.warrior == Warrior.Karate || warriorAnimationDemo.warrior == Warrior.Knight || warriorAnimationDemo.warrior == Warrior.Mage){
						if(GUI.Button(new Rect(135, 175, 100, 30), "SpecialAttack2")){
							if(warriorAnimationDemo.attack == 0){
								if(warriorAnimationDemo.warrior == Warrior.Sorceress){
									if(!warriorAnimationDemo.specialAttack2Bool){
										warriorAnimationDemo.attack = 4;
										warriorAnimationDemo.animator.SetTrigger("SpecialAttack2Trigger");
										warriorAnimationDemo.animator.SetBool("warriorAnimationDemo.specialAttack2Bool", true);
										warriorAnimationDemo.specialAttack2Bool = true;
									} 
									else{
										warriorAnimationDemo.attack = 4;
										warriorAnimationDemo.specialAttack2Bool = false;
										warriorAnimationDemo.animator.SetBool("warriorAnimationDemo.specialAttack2Bool", false);
										warriorAnimationDemo.animator.SetBool("SpecialAttack2Trigger", false);
									}
								} 
								else{
									warriorAnimationDemo.attack = 4;
									warriorAnimationDemo.animator.SetTrigger("SpecialAttack2Trigger");
									if(warriorAnimationDemo.warrior == Warrior.Knight){
										StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.85f));
									}
									else if(warriorAnimationDemo.warrior == Warrior.Mage){
										StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.95f));
									}
									else{
										StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.25f));
									}
								}
							}
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Sorceress){
						if(GUI.Button(new Rect(245, 175, 100, 30), "SpecialAttack3")){
							warriorAnimationDemo.animator.SetTrigger("SpecialAttack3Trigger");
							StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.2f));
						}
					}
					if(GUI.Button(new Rect(30, 270, 100, 30), "Death")){
						warriorAnimationDemo.animator.SetTrigger("DeathTrigger");
						warriorAnimationDemo.dead = true;
					}
					if(warriorAnimationDemo.warrior == Warrior.Mage || warriorAnimationDemo.warrior == Warrior.Ninja || warriorAnimationDemo.warrior == Warrior.Knight || warriorAnimationDemo.warrior == Warrior.Archer || warriorAnimationDemo.warrior == Warrior.TwoHanded || warriorAnimationDemo.warrior == Warrior.Swordsman || warriorAnimationDemo.warrior == Warrior.Spearman || warriorAnimationDemo.warrior == Warrior.Hammer || warriorAnimationDemo.warrior == Warrior.Crossbow){
						if(!warriorAnimationDemo.dead && !warriorAnimationDemo.weaponSheathed && !warriorAnimationDemo.weaponSheathed2){
							if(GUI.Button(new Rect(30, 310, 100, 30), "Sheath Wpn")){
								warriorAnimationDemo.SheathWeapon();
							}
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Knight && !warriorAnimationDemo.weaponSheathed){
						if(!warriorAnimationDemo.dead && !warriorAnimationDemo.weaponSheathed2){
							if(GUI.Button(new Rect(140, 310, 100, 30), "Sheath Wpn2")){
								StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.4f));
								warriorAnimationDemo.animator.SetTrigger("WeaponSheath2Trigger");
								StartCoroutine(warriorAnimationDemo._WeaponVisibility(0.75f, false));
								warriorAnimationDemo.weaponSheathed2 = true;
							}
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Knight){
						if(!warriorAnimationDemo.dead && warriorAnimationDemo.weaponSheathed2){
							if(GUI.Button(new Rect(140, 310, 100, 30), "UnSheath Wpn2")){
								StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(1.4f));
								warriorAnimationDemo.animator.SetTrigger("WeaponUnsheath2Trigger");
								StartCoroutine(warriorAnimationDemo._WeaponVisibility(0.5f, true));
								warriorAnimationDemo.weaponSheathed2 = false;
								warriorAnimationDemo.weaponSheathed = false;
							}
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Ninja && !warriorAnimationDemo.isStealth){
						if(!warriorAnimationDemo.dead && !warriorAnimationDemo.weaponSheathed){
							if(GUI.Button(new Rect(30, 350, 100, 30), "Stealth")){
								warriorAnimationDemo.animator.SetBool("Stealth", true);
								warriorAnimationDemo.isStealth = true;
							}
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Ninja && warriorAnimationDemo.isStealth && !warriorAnimationDemo.isWall){
						if(!warriorAnimationDemo.dead && !warriorAnimationDemo.weaponSheathed){
							if(GUI.Button(new Rect(30, 350, 100, 30), "UnStealth")){
								warriorAnimationDemo.animator.SetBool("Stealth", false);
								warriorAnimationDemo.isStealth = false;
							}
						}
					}
					if(warriorAnimationDemo.warrior == Warrior.Ninja && warriorAnimationDemo.isStealth){
						if(!warriorAnimationDemo.dead && !warriorAnimationDemo.weaponSheathed){
							if(!warriorAnimationDemo.isWall){
								if(GUI.Button(new Rect(140, 350, 100, 30), "Wall On")){
									warriorAnimationDemo.animator.applyRootMotion = true;
									warriorAnimationDemo.animator.SetBool("Stealth-Wall", true);
									warriorAnimationDemo.isWall = true;
								}
							} 
							else{
								if(GUI.Button(new Rect(140, 350, 100, 30), "Wall Off")){
									warriorAnimationDemo.animator.SetBool("Stealth-Wall", false);
									warriorAnimationDemo.isWall = false;
									StartCoroutine(warriorAnimationDemo._LockMovementAndAttack(0.7f));
								}
							}
						}
					}
				}
			}
		}
		if(warriorAnimationDemo.dead){
			if(GUI.Button(new Rect (30, 270, 100, 30), "Revive")){
				StartCoroutine(warriorAnimationDemo._Revive());
			}
		}
	}
}