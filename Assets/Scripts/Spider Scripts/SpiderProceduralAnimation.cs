using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SpiderProceduralAnimation : MonoBehaviour
{
    public Leg[] legs; //ASSIGN IN INSPECTOR
    public GameObject[] leftLegTargets;
    public GameObject[] rightLegTargets;
    public GameObject spiderBody;

    //can only be used if the leg count is == 4
    public GameObject[] frontLegTargets;
    public GameObject[] backLegTargets;

    public Vector3 averageStandingPos;

    public bool isLeftToMove = true;
    public bool anyLegsMoving = false;

    //fine tuning values
    public float maxReyDistance;

    //ASSIGN IN INSPECTOR
    public float maxStepHeight;
    public float maxDistanceBeforeStep;
    public float timePerStep;

    // Start is called before the first frame update
    void Start()
    {
        spiderBody = transform.gameObject;
        SetupLegs();
    }

    void FixedUpdate()
    {
        //for each leg
        for (int i = 0; i < legs.Length; i++)
        {
            //raycast down to check for uneven ground and adjust legStandardPos correspondingly
            RaycastHit hit;
            var rayStart = legs[i].reyStart.transform.position;
            var rayDir = (legs[i].nextLegTarget.transform.position - rayStart).normalized;
            Physics.Raycast(rayStart, rayDir, out hit, maxReyDistance);
            //Debug.DrawRay(rayStart, rayDir, Color.black);

            if (hit.collider != null)
            {
                legs[i].nextLegTarget.transform.position = hit.point;
                legs[i].nextLegTarget.transform.up = hit.normal;
            }

            //if this leg has to stand, make it stand
            if (!legs[i].legToMove)
            {
                legs[i].legTarget.transform.position = legs[i].standingPos;
            }

            //determine distance to check if a step needs to be made
            if (Vector3.Distance(legs[i].nextLegTarget.transform.position, legs[i].legTarget.transform.position) > maxDistanceBeforeStep && legs[i].legToMove == false)
            {
                StartCoroutine(Step(legs[i].isLegWalkingWithLeft));
            }
        }
        //CalculateBodyAdjustment();
    }

    //sets up legIndex and sorts legTargets into their correct left/right/front/back locations
    void SetupLegs()
    {
        leftLegTargets = new GameObject[legs.Length / 2];
        rightLegTargets = new GameObject[legs.Length / 2];

        if (legs.Length == 4)
        {
            frontLegTargets = new GameObject[2];
            backLegTargets = new GameObject[2];
        }

        for (int i = 0; i < legs.Length; i++)
        {
            //finding various stuff in scene, setting up lastStandingPos for startingPos
            legs[i].nextLegTarget = legs[i].leg.transform.Find(legs[i].leg.name.ToString() + "_standardPos").gameObject;
            legs[i].legTarget = legs[i].leg.transform.Find(legs[i].leg.name.ToString() + "_target").gameObject;
            legs[i].reyStart = legs[i].leg.transform.Find(legs[i].leg.name.ToString() + "_reyStart").gameObject;
            legs[i].lastStandingPos = legs[i].nextLegTarget.transform.position;
            legs[i].defaultLegPos = legs[i].legTarget.transform.localPosition;

            //setup for legIndex
            var intForLegIndex = i % 4;
            switch (intForLegIndex)
            {
                case 0: legs[i].legIndex = (i / 2, 0); break;
                case 1: legs[i].legIndex = ((i - 1) / 2, 1); break;
                case 2: legs[i].legIndex = (i / 2, 1); break;
                case 3: legs[i].legIndex = ((i - 1) / 2, 0); break;
            }

            //if the leg is on the left side of the body
            if (legs[i].legIndex.side == 0)
            {
                leftLegTargets[legs[i].legIndex.row] = legs[i].legTarget;
            }
            else if (legs[i].legIndex.side == 1)
            {
                rightLegTargets[legs[i].legIndex.row] = legs[i].legTarget;
            }
            //Setup front and backLegs, if there are only 4 legs
            if (legs.Length == 4)
            {
                if (i < 2)
                {
                    frontLegTargets[i] = legs[i].legTarget;
                }
                else if (i <= 3)
                {
                    backLegTargets[i - 2] = legs[i].legTarget;
                }
            }

            //if i is even the legTarget belongs to the leftCloningLegs and vise versa
            if (i % 2 == 0)
            {
                legs[i].isLegWalkingWithLeft = true;
            }
            else
            {
                legs[i].isLegWalkingWithLeft = false;
            }
        }
        GetComponent<SpiderLevelInteraction>().Setup();
    }

    IEnumerator Step(bool leftOrRight)
     {
        if (anyLegsMoving)
        {
            yield break;
        }
        anyLegsMoving = true;

        var walkers = ReturnWalkers(leftOrRight);

        float timeElapsed = 0;
        var height = 0f;

        while (timeElapsed < timePerStep)
        {
            if (timeElapsed <= timePerStep / 2)
            {
                height = (timeElapsed / timePerStep) * maxStepHeight;
            }
            else
            {
                height = ((timePerStep - timeElapsed) / timePerStep) * maxStepHeight;
            }

            foreach (Leg walker in walkers)
            {
                walker.legToMove = true;

                //lerp legs to target
                var targetPosFlat = Vector3.Lerp(walker.lastStandingPos, walker.nextLegTarget.transform.position, timeElapsed / timePerStep);
                walker.legTarget.transform.position = targetPosFlat + (walker.legTarget.transform.up * height);

                timeElapsed += Time.deltaTime;
            }
            yield return null;
        }

        //to make sure the exact endPos are reached
        foreach (Leg walker in walkers)
        {
            walker.legToMove = false;
            walker.legTarget.transform.position = walker.nextLegTarget.transform.position;
            ResetLegAfterStep(walker);
        }

        //invert legsToMove value
        isLeftToMove = !isLeftToMove;
        anyLegsMoving = false;
    }

    public void ResetLegAfterStep(Leg leg)
    {
        //reset values
        leg.lastStandingPos = leg.legTarget.transform.position;
        leg.standingPos = leg.legTarget.transform.position;
    }

    public void CalculateBodyAdjustment()
    {
        var lastBodyUp = transform.up;

        Vector3 v1 = legs[0].legTarget.transform.position - legs[1].legTarget.transform.position;
        Vector3 v2 = legs[3].legTarget.transform.position - legs[2].legTarget.transform.position;
        Vector3 normal = Vector3.Cross(v1, v2).normalized;
        Vector3 up = Vector3.Lerp(lastBodyUp, normal, 1f / (float)(GetComponent<SpiderController>().smoothness + 1));
        transform.up = up;
        transform.rotation = Quaternion.LookRotation(transform.forward, up);
    }

    public List<Leg> ReturnWalkers(bool leftOrRight)
    {
        List<Leg> leftWalkers = new List<Leg>();
        List<Leg> rightWalkers = new List<Leg>();
        if (leftOrRight)
        {
            for (int i = 0; i < legs.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leftWalkers.Add(legs[i]);
                }
            }
            return leftWalkers;
        }
        else
        {
            for (int i = 0; i < legs.Length; i++)
            {
                if (i % 2 == 1)
                {
                    rightWalkers.Add(legs[i]);
                }
            }
            return rightWalkers;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < legs.Length; i++)
        {
            if (legs[i].nextLegTarget != null)
            {
                //Gizmos.color = Color.red;
                //Gizmos.DrawSphere(legs[i].nextLegTarget.transform.position, 0.1f);
                //Gizmos.color = Color.green;
                //Gizmos.DrawLine(legs[i].legTarget.transform.position, legs[i].nextLegTarget.transform.position);
            }

            //Gizmos.color = Color.green;
            //if (legs[i].legTarget != null)
            //{
            //    Gizmos.DrawWireSphere(legs[i].legTarget.transform.position, maxDistanceBeforeStep);
            //}
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(averageStandingPos, 0.3f);
    }    
}

[System.Serializable]
public class Leg
{
    public GameObject leg;
    public (int row, int side) legIndex;
    public GameObject nextLegTarget;
    public GameObject legTarget;
    public GameObject reyStart;
    public bool isLegWalkingWithLeft;
    public Vector3 standingPos;
    public Vector3 lastStandingPos;
    public Vector3 defaultLegPos;

    public bool legToMove;
    public bool standingPositionSet;
    public bool isGrounded;
}
