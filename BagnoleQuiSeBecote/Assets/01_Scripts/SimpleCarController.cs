using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;


public class SimpleCarController : MonoBehaviour
{
    public CarInput input;
    [SerializeField] private Rigidbody rb;
    private CarInputMode mode;

    private float speed, turn;
    [SerializeField] private float maxSpeed = 5000;
    [SerializeField] private float turnStrength = 180;
    [SerializeField] private float dragOnGround = 3;
    
    [Header("Gravity & Ground")]
    [SerializeField] private float gravityForce = 10f;
    [SerializeField] private LayerMask lmGround;
    [SerializeField] private float groundRayLength = 0.5f;
    [SerializeField] private Transform groundRayPoint;
    private bool grounded;
    private RaycastHit hit;

    [Header("Feedbacks")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform leftFrontWheel;
    [SerializeField] private Transform rightFrontWheel;
    [SerializeField] private float maxWheelTurn = 25f;

    [Header("Actions")]
    [SerializeField] private float jumpForce;
    private bool j1Input, j2Input;
    private bool j1Charge, j2Charge;
    private bool j1Call, j2Call;
    private bool j1ActionDone, j2ActionDone;
    private bool checkForActionReset;

    private void Start()
    {
        rb.transform.parent = null;
        mode = GetComponent<CarInputMode>();
    }

    private void Update()
    {
        MyInputs();
        UpdateAnimation();
        UpdateActionResets();
        
        transform.position = rb.transform.position;

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turn * turnStrength * Time.deltaTime * speed, 0f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turn * turnStrength/3 * Time.deltaTime * speed, 0f));
        }
        
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localEulerAngles.x, (turn * maxWheelTurn) - 180, leftFrontWheel.localEulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localEulerAngles.x, (turn * maxWheelTurn) - 180, rightFrontWheel.localEulerAngles.z);
    }

    private void MyInputs()
    {
        float targetSpeed = input.accel - input.decel;
        speed = Mathf.Clamp(Mathf.Lerp(speed, targetSpeed, 1 * Time.deltaTime), -0.5f, 1) ;
        
        turn = Mathf.Lerp(turn, input.turn, 8 * Time.deltaTime);

        if (mode.activeMode == CarInputMode.CarMode.Twice)
        {
            //Get Action Inputs changes
            if (j1Input != InputHandler.Instance.J1Input.jump && !j1ActionDone)
            {
                j1Input = InputHandler.Instance.J1Input.jump;
                if (j1Input)
                {
                    Charge(1);
                }
                else
                {
                    CallAction(1);
                }
            }
            if (j2Input != InputHandler.Instance.J2Input.jump && !j2ActionDone)
            {
                j2Input = InputHandler.Instance.J2Input.jump;
                if (j2Input)
                {
                    Charge(2);
                }
                else
                {
                    CallAction(2);
                }
            }
        }
        else
        {
            if (j1Input != input.jump && !j1ActionDone)
            {
                j1Input = input.jump;
                
                if (j1Input)
                {
                    Charge(1);
                }
                else
                {
                    CallAction(1);
                }
            }
        }
        
        
    }

    private float animTurn;
    private void UpdateAnimation()
    {
        if (grounded && Mathf.Abs(speed) > 0.4f) 
            animTurn = Mathf.Lerp(animTurn, turn, 2 * Time.deltaTime);
        else 
            animTurn = Mathf.Lerp(animTurn, 0, 2 * Time.deltaTime);
        
        animator.SetFloat("Turn", animTurn);
        animator.SetFloat("Speed", speed);
    }

    private void UpdateActionResets()
    {
        if (grounded)
        {
            if (!j1Input && j1ActionDone)
            {
                j1ActionDone = false;
            }
            
            if (!j2Input && j2ActionDone)
            {
                j2ActionDone = false;
            }
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();
        
        if (grounded)
        {
            rb.linearDamping = dragOnGround;
            rb.linearVelocity += (transform.forward * speed * 0.01f * maxSpeed);
            
            var targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 6f * Time.deltaTime);
        }
        else
        {
            rb.linearDamping = 0.1f;
            rb.AddForce(Vector3.down * gravityForce);
        }
    }

    private void GroundCheck()
    {
        if (checkForActionReset) grounded = false;
        else grounded = Physics.Raycast(groundRayPoint.position, Vector3.down, out hit, groundRayLength, lmGround);
    }

    private void Charge(int playerIndex)
    {
        //Debug.Log($"J{playerIndex} Charge Start");
        
        if (playerIndex == 1)
        {
            j1Charge = true;
        }
        if (playerIndex == 2)
        {
            j2Charge = true;
        }
    }
    
    private void CallAction(int playerIndex)
    {
        //Debug.Log($"J{playerIndex} Charge Stopped");
        
        StartCoroutine(MakeAction(playerIndex));
    }

    private IEnumerator MakeAction(int playerIndex)
    {
        if (playerIndex == 1)
        {
            j1Charge = false;
            j1Call = true;
        }
        if (playerIndex == 2)
        {
            j2Charge = false;
            j2Call = true;
        }
        
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ActionCD());
        yield return new WaitForSeconds(0.1f);

        //Debug.Log($"{j1Call}/{j2Call} called");

        if (j1Call && j2Call)
        {
            Dash();
            j1Call = false;
            j2Call = false;
        }
        else if (j1Call)
        {
            Jump(playerIndex);
            j1Call = false;
        }
        else if (j2Call)
        {
            Jump(playerIndex);
            j2Call = false;
        }
    }

    private void Jump(int playerIndex)
    {
        //Debug.Log("Jump");

        if (playerIndex == 1)
        {
            j1ActionDone = true;
        }
        if (playerIndex == 2)
        {
            j2ActionDone = true;
        }
        
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void Dash()
    {
        //Debug.Log("Dash");
        
        j1ActionDone = true;
        j2ActionDone = true;
    }

    private IEnumerator ActionCD()
    {
        checkForActionReset = true;
        yield return new WaitForSeconds(0.4f);
        checkForActionReset = false;
    }

    
}
