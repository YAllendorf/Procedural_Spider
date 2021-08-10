using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderController : MonoBehaviour
{
    //ASSIGN IN INSPECTOR
    [Header("Walking Variables")]
    public float movementSpeed;
    public float rotationSpeed;
    [Space(10)]

    //ASSIGN IN INSPECTOR
    [Header("Spider Adjusting Variables")]
    public int raysNumber;
    [Space(10)]
    public float rayStartHeight;
    public float raysAngle;
    public float offsetOuterRays;
    public float offsetInnerRays;
    public float halfRange;
    [Space(10)]
    public float fallingRayDistance;
    public float fallingSmoothness;
    [Space(10)]
    public float smoothness;
    public float velocityThreshold;

    //ASSIGN IN INSPECTOR
    [Header("Jumping")]
    public Rigidbody rb;
    public float jumpPowerIncrement;
    public float jumpPower;
    public float jumpMultiplier;
    public float maxJumpPower;
    public Vector3 jumpTarget;
    public Vector3 jumpDirection;
    private bool jumpInput;
    //private bool isGrounded;
    //private bool isLookingForLanding;
    public float gravity;

    [Header("Camera")]
    private GameObject cameraFocus;
    public float inputThreshold;

    [Header("Level Managing")]
    private int currentLevel; 

    private Vector3 velocity;
    private Vector3 lastVelocity;
    private Vector3 lastPosition;

    private float inputX;
    private float inputZ;
    private float angle;
    private Vector2 cameraRotation;

    private InputAction walking;
    private InputAction rotateCamera;
    private InputAction jump;

    public MovementType movementType = MovementType.Walk;

    public bool pauseAdjusting;

    void Start()
    {
        //assign variables, find them via name
        walking = GetComponent<PlayerInput>().actions.FindAction("WalkingX");
        rotateCamera = GetComponent<PlayerInput>().actions.FindAction("RotateCamera");
        jump = GetComponent<PlayerInput>().actions.FindAction("Jump");

        cameraFocus = transform.Find("CameraFocus").gameObject;
        rb = transform.GetComponent<Rigidbody>();
        movementType = MovementType.Walk;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastPosition = transform.position;

        velocity = (smoothness * velocity + (transform.position - lastPosition)) / (1f + smoothness);
        if (velocity.magnitude < velocityThreshold)
        {
            velocity = lastVelocity;
        }
        lastVelocity = velocity;

        Input();
        if (movementType == MovementType.Walk)
        {
            Walking();
        }

        if (movementType == MovementType.Fall)
        {
            Falling();
        }
    }

    public void Input()
    {
        //walking
        #region walking
        if (walking.ReadValue<Vector2>().x != 0f)
        {
            inputZ = walking.ReadValue<Vector2>().x;
        }
        else
        {
            inputZ = 0f;
        }

        if (walking.ReadValue<Vector2>().y != 0f)
        {
            inputX = walking.ReadValue<Vector2>().y;
        }
        else
        {
            inputX = 0f;
        }      
        #endregion

        //camera and rotating
        #region camera and rotating
        if (rotateCamera.ReadValue<Vector2>().x != 0f)
        {
            if (rotateCamera.ReadValue<Vector2>().x > inputThreshold)
            {
                angle = rotationSpeed;
            }
            else if (rotateCamera.ReadValue<Vector2>().x < -inputThreshold)
            {
                angle = -rotationSpeed;
            }
            else if (rotateCamera.ReadValue<Vector2>().x < inputThreshold && rotateCamera.ReadValue<Vector2>().x > -inputThreshold)
            {
                angle = 0f;
            }
        } 

        if (rotateCamera.ReadValue<Vector2>().x == 0f)
        {
            angle = 0f;
        }

        if (rotateCamera.ReadValue<Vector2>().y != 0f)
        {
            var rotCam = rotateCamera.ReadValue<Vector2>();

            cameraFocus.transform.RotateAround(transform.position, transform.forward, rotCam.y * Time.deltaTime * rotationSpeed * 5f);
        }
        #endregion

        //jumping
        #region jumping        
        if (jump.ReadValue<float>() > 0.5f)
        {
            jumpInput = true;
            if (jumpPower <= maxJumpPower)
            {
                jumpPower += jumpPowerIncrement;
            }
        }
        else
        {
            if (movementType == MovementType.Walk && jumpInput)
            {
                movementType = MovementType.Fall;
                jumpInput = false;
                StartCoroutine(Jump());
            }
        }
        #endregion
    }

    public void Walking()
    {
        var nextPos = WalkAdjustBody()[0];
        var up = WalkAdjustBody()[1];
        var right = Vector3.Cross(up, transform.forward);
        var forward = Vector3.Cross(right, up);
        var quaternion = Quaternion.LookRotation(forward, up);

        transform.position = Vector3.Lerp(lastPosition, nextPos, 1f / (1f + smoothness));

        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, 1f / (1f + smoothness));

        transform.Translate(new Vector3(movementSpeed * Time.deltaTime * inputX, 0, movementSpeed * Time.deltaTime * -inputZ), Space.Self);
        transform.Rotate(transform.up, angle * Time.deltaTime * rotationSpeed, Space.World);
    }

    public void Falling()
    {
        var up = (FallAdjustBody().normalized + Vector3.up).normalized;
        var right = Vector3.Cross(up, transform.forward);
        var forward = Vector3.Cross(right, up);
        var quaternion = Quaternion.LookRotation(forward, up);

        transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, 1f / (1f + fallingSmoothness));

        transform.Rotate(transform.up, angle * Time.deltaTime * rotationSpeed, Space.World);
    }

    IEnumerator Jump()
    {
        if (jumpPower < 1f)
        {
            jumpPower = 1f;
        }
        if (movementType != MovementType.Walk && !jumpInput)
        {
            rb.useGravity = true;

            jumpDirection = new Vector3(inputX, 0.5f, -inputZ).normalized;
            rb.AddRelativeForce(jumpDirection * jumpPower * jumpMultiplier, ForceMode.Impulse);
        }

        StartCoroutine(ResetJump(0.25f));
        yield break;
    }

    IEnumerator ResetJump(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        movementType = MovementType.Fall;
        jumpPower = 0f;
    }

    public void StopFalling()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        movementType = MovementType.Walk;
    }

    //Vector[0] is average of hit.points
    //Vector[1] is average of hit.normals
    public Vector3[] WalkAdjustBody()
    {
        //for cleaner code
        Vector3 position = transform.position;
        Vector3 up = transform.up;
        Vector3 forward = transform.forward;
        Vector3 right = Vector3.Cross(up, forward);

        Vector3[] forCalculation = new Vector3[2] { position, up };
        int numberOfNormals = 1;
        int numberOfPositions = 1;

        Vector3[] rayDirs = new Vector3[raysNumber];
        float angleStep = 2 * Mathf.PI / raysNumber;
        float angleCurrent = angleStep / 2;

        for (int i = 0; i < raysNumber; i++)
        {
            rayDirs[i] = -up + (right * Mathf.Cos(angleCurrent) + forward * Mathf.Sin(angleCurrent)) * raysAngle;
            angleCurrent += angleStep;
        }

        foreach (Vector3 dir in rayDirs)
        {
            //inner raycasts
            RaycastHit hit;
            Vector3 largener = Vector3.ProjectOnPlane(dir, up);
            Ray ray = new Ray(position + up * rayStartHeight - (dir + largener) * 0.5f + largener.normalized * offsetInnerRays / 100f, dir);
            Debug.DrawRay(ray.origin, ray.direction * halfRange, Color.red);
            if (Physics.SphereCast(ray, 0.01f, out hit, 2f * halfRange))
            {
                forCalculation[0] += hit.point;
                forCalculation[1] += hit.normal;
                numberOfNormals += 1;
                numberOfPositions += 1;
            }
            //outer raycasts
            ray = new Ray(position + up * rayStartHeight - (dir + largener) * 0.5f + largener.normalized * offsetOuterRays / 100f, dir);
            Debug.DrawRay(ray.origin, ray.direction * halfRange, Color.blue);
            if (Physics.SphereCast(ray, 0.01f, out hit, 2f * halfRange))
            {
                forCalculation[0] += hit.point;
                forCalculation[1] += hit.normal;
                numberOfNormals += 1;
                numberOfPositions += 1;
            }
        }
        forCalculation[0] /= numberOfPositions;
        forCalculation[1] /= numberOfNormals;
        return forCalculation;
    }

    public Vector3 FallAdjustBody()
    {
        //for cleaner code
        Vector3 position = transform.position;
        Vector3 up = transform.up;
        Vector3 forward = transform.forward;
        Vector3 right = Vector3.Cross(up, forward);

        Vector3 forCalculation = Vector3.zero;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, rb.velocity);
        //Debug.DrawRay(ray.origin, ray.direction * halfRange, Color.cyan);
        if (Physics.SphereCast(ray, 0.1f, out hit, fallingRayDistance))
        {
            //turn on gravity, if raycast hit anything
            StopFalling();
        }
        forCalculation = -rb.velocity;

        return forCalculation;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + transform.right);
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, transform.position + transform.up);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}

public enum MovementType
{
    Walk = 0,
    Fall = 1,
}



