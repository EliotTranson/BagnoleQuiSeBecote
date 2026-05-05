using System;
using NUnit.Framework;
using UnityEngine;


public class SimpleCarController : MonoBehaviour
{
    public CarInput input;
    [SerializeField] private Rigidbody rb;

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
    private bool canJump;

    private void Start()
    {
        rb.transform.parent = null;
    }

    private void Update()
    {
        MyInputs();
        UpdateAnimation();
        
        HandleJump();
        
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
        grounded = Physics.Raycast(groundRayPoint.position, Vector3.down, out hit, groundRayLength, lmGround);

        if (!canJump && grounded && input.jump == 0)
        {
            canJump = true;
        }
    }

    public void HandleJump()
    {
        if (canJump && input.jump == 1)
        {
            Jump();
            canJump = false;
        }
    }

    private void Jump()
    {
        Debug.Log("Jump");
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
