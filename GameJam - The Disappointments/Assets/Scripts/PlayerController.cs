using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Rigidbody Reference
    private Rigidbody rb;

    //Animator Reference
    private Animator animator;

    //Movement Vector
    private Vector3 movement;

    //Flag Controlling if Player's can Jump (NOT WORKING BECAUSE UNITY SUCKS)
    [SerializeField] private bool isGrounded;

    //Flag Controlling Player's Jump
    private bool jump;

    //Ray Distance
    [SerializeField] private float rayDistance;

    //Animation States
    private Dictionary<string, string> AnimationStates = new Dictionary<string, string>() {
        ["Idle"] = "Idle",
        ["RunLeft"] = "Run_Left",
        ["RunRight"] = "Run_Right",
        ["JumpInPlace"] = "Jump_InPlace",
        ["JumpLeft"] = "Jump_Left",
        ["JumpRight"] = "Jump_Right",
        ["DieInPlace"] = "Die_InPlace",
        ["DieLeft"] = "Die_Left",
        ["DieRight"] = "Die_Right",
        ["ThrowLeft"] = "Throw_Left",
        ["ThrowRight"] = "Throw_Right"
    };

    //Holds the Current Animation State
    private string currentState;

    //Speed Array
    [SerializeField] private float[] speedSet;

    //Flag Controlling if Player got Hit
    private bool hit;

    //Flag Controlling if Player's Alive
    private bool isAlive;

    //Flag Controlling if Player has attacked
    private bool hasShoot;

    //Particle System
    [Header("Particle Systems")]

    [Tooltip("Teleport Particle System")]
    [SerializeField] GameObject teleportPS;

    [Tooltip("Weapon Particle System")]
    [SerializeField] GameObject weaponPS;

    //Weapon Reference
    [Header("Weapon References")]
    [SerializeField] private Transform gear;

    [Range(2f, 15f)]
    [SerializeField] private float throwPower;

    //Weapon shooting location reference
    [SerializeField] private Transform shootFrom;
    //Weapon parent reference
    [SerializeField] Transform parentReference;
    [SerializeField] Transform curvePoint;

    [Header("Camera Controller")]
    //Camera Reference
    [SerializeField] MouseControl mControl;

    //God of war Effect

    bool isToReturn = false;
    float time = 0.0f;
    Vector3 gearLastPosition;



    // Start is called before the first frame update
    void Start() {
        //Get References
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //Set Flag
        hit = false;
        isAlive = true;
        hasShoot = false;
    }

    // Update is called once per frame
    void Update() {
        //Detect Player Movement Input
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = new Vector3(0.5f, .01f, 0);
            hit = false;
            isAlive = true;
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            hit = true;
        }

        if (!hasShoot && !isToReturn) {
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log(mControl.GetMousePosition().x - transform.position.x);
                if (mControl.GetMousePosition().x - transform.position.x >= 0) {
                    ChangeAnimationState(AnimationStates["ThrowRight"]);
                } else {
                    ChangeAnimationState(AnimationStates["ThrowLeft"]);
                }
            }
        } else {
            if (Input.GetMouseButtonDown(0) && !isToReturn) {
                TeleportToGear();
            }
            if (Input.GetMouseButtonDown(1)) {
                ReturnGear();
            }
        }

        if (isToReturn) {
            if (time < 1.0f) {
                gear.transform.position = getBQCPoint(time, gearLastPosition, curvePoint.position, transform.position);
                time += Time.deltaTime;
            }
            if (Vector3.Distance(transform.position, gear.position) < 0.5f) {
                GearCatch();
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.N) && !hasShoot)
        {
            ChangeAnimationState(AnimationStates["ThrowLeft"]);
        }

        if (Input.GetKeyDown(KeyCode.M) && !hasShoot)
        {
            ChangeAnimationState(AnimationStates["ThrowRight"]);
        }
        */
    }

    private void FixedUpdate() {
        Vector3 velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);

        if (!hit) {
            //Check if Player in on the Ground
            if (Physics.Raycast(transform.position, Vector3.down, rayDistance)) {
                isGrounded = true;
            } else {
                isGrounded = false;
            }

            //Check Movement Based on Input
            if (movement.x < 0) {
                velocity.x = -speedSet[1];
            } else if (movement.x > 0) {
                velocity.x = speedSet[1];
            } else {
                velocity.x = speedSet[0];
            }

            //Check if Trying to Jump
            if (jump && isGrounded) {
                rb.AddForce(Vector3.up * 1000, ForceMode.Force);
                jump = false;
            }


            //Animation Control
            if (isGrounded) {
                if (movement.x < 0) {
                    ChangeAnimationState(AnimationStates["RunLeft"]);
                } else if (movement.x > 0) {
                    ChangeAnimationState(AnimationStates["RunRight"]);
                } else {
                    ChangeAnimationState(AnimationStates["Idle"]);
                }
            } else {
                if (movement.x < 0) {
                    ChangeAnimationState(AnimationStates["JumpLeft"]);
                } else if (movement.x > 0) {
                    ChangeAnimationState(AnimationStates["JumpRight"]);
                } else {
                    ChangeAnimationState(AnimationStates["JumpInPlace"]);
                }
            }

            //Assign new Velocity to Rigidbody
            rb.velocity = velocity;
        } else {
            if (isAlive) {
                if (movement.x < 0) {
                    ChangeAnimationState(AnimationStates["DieLeft"]);
                } else if (movement.x > 0) {
                    ChangeAnimationState(AnimationStates["DieRight"]);
                } else {
                    ChangeAnimationState(AnimationStates["DieInPlace"]);
                }
                isAlive = false;
            }
        }

        if (!isAlive) {
            velocity.x = 0;
            rb.velocity = velocity;
        }
    }


    public void Die() {
        hit = true;
    }

    /// <summary>
    /// Function that Teleports the player to the gear after he throw it
    /// </summary>
    private void TeleportToGear() {
        GameObject playerPosition = Instantiate(teleportPS, transform);
        GameObject gearPosition = Instantiate(teleportPS, gear);
        playerPosition.transform.parent = null;
        playerPosition.GetComponentsInChildren<ParticleSystem>().Initialize();
        gearPosition.transform.parent = null;
        gearPosition.GetComponentsInChildren<ParticleSystem>().Initialize();
        Destroy(playerPosition, 2f);
        Destroy(gearPosition, 2f);

        transform.position = new Vector3(gear.position.x, gear.position.y + 0.1f, 0);

        GearCatch();
    }

    /// <summary>
    /// Function that resets the gear into the hand of the player
    /// </summary>
    private void GearCatch() {
        hasShoot = false;
        isToReturn = false;
        gear.SetParent(parentReference);
        gear.GetComponent<Gear>().SetDefaultSize();
    }

    /// <summary>
    /// Function that returns the gear to the player
    /// </summary>
    private void ReturnGear() {
        time = 0.0f;
        gearLastPosition = gear.position;

        isToReturn = true;
        gear.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    #region Animator Controller Methods
    private void ChangeAnimationState(string newState) {
        //Stop the same animation from interrupting itself - Guard
        if (currentState == newState) {
            return;
        }

        //Play the Animation
        animator.Play(newState);

        //Reassign the current state
        currentState = newState;
    }
    #endregion

    #region Animation Events
    public void Throw() {
        hasShoot = true;
        Rigidbody gearRB = gear.GetComponent<Rigidbody>();
        gearRB.isKinematic = false;
        gear.GetComponent<BoxCollider>().enabled = true;
        gear.GetComponent<Gear>().SetBigSize();
        gear.SetParent(null);
        gearRB.AddForce(shootFrom.forward * throwPower, ForceMode.Impulse);
    }

    public void FinishedAnimation() {
        ChangeAnimationState(AnimationStates["Idle"]);
    }
    #endregion


    // Bezier Quadratic Curve formula
    // Learn more:
    // https://en.wikipedia.org/wiki/B%C3%A9zier_curve
    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        // "t" is always between 0 and 1, so "u" is other side of t
        // If "t" is 1, then "u" is 0
        float u = 1 - t;
        // "t" square
        float tt = t * t;
        // "u" square
        float uu = u * u;
        // this is the formula in one line
        // (u^2 * p0) + (2 * u * t * p1) + (t^2 * p2)
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}
