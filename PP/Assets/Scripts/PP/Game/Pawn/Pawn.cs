using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(Rigidbody))]
public class Pawn : MonoBehaviour
{
    public int team = -1;
    public bool mobility = true;
    public bool isActive = true;

    public float acceleration = 30.0f;
    public float maxSpeed = 3.0f;
    public float maxSpeed_mod = 1.0f;

    public float jumpForce = 6.0f;

    public bool isSpeedLimited = true;
    public bool isRecievingMovingVector = true;
    public int faceDirection
    {
        get => _faceDirection;
        set
        {
            int sign = System.Math.Sign(value);
            if (sign != _faceDirection)
            {
                _faceDirection = sign;
                Vector3 vScale = transform.localScale;
                vScale.x = sign;
                animator.transform.localScale = vScale;
            }
        }
    }
    protected int _faceDirection = 1;

    public Animator animator;

    protected Rigidbody rigidbody = null;

    public Vector3 inputVector_moving = Vector3.zero;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator ??= GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (animator != null)
        {
            if (inputVector_moving.x != 0 || inputVector_moving.z != 0)
                animator.SetBool("MoveDown", true);
            else
                animator.SetBool("MoveDown", false);

            animator?.SetFloat("YSpeed", GetComponent<Rigidbody>().velocity.y);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (mobility) Move();
    }

    public void Move()
    {
        if (!mobility) return;

        if (inputVector_moving.x != 0)
        faceDirection = System.Math.Sign(inputVector_moving.x);

        rigidbody.AddForce(
            new Vector3(inputVector_moving.x, 0, inputVector_moving.z) * acceleration,
            ForceMode.Force
        );

        Vector2 velocity_2d = new Vector2(
            rigidbody.velocity.x,
            rigidbody.velocity.z
        );

        if (isSpeedLimited) velocity_2d = Vector2.ClampMagnitude(velocity_2d, maxSpeed * maxSpeed_mod);
        
        rigidbody.velocity = new Vector3(velocity_2d.x, rigidbody.velocity.y, velocity_2d.y);
    }
}
