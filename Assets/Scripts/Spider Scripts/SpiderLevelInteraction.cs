using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLevelInteraction : MonoBehaviour
{
    public GameObject[] legTargets;
    public Vector3 currentDown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCanvas();
    }

    public void Setup()
    {
        legTargets = new GameObject[GetComponent<SpiderProceduralAnimation>().legs.Length];
        for (int i = 0; i < legTargets.Length; i++)
        {
            legTargets[i] = GetComponent<SpiderProceduralAnimation>().legs[i].legTarget;
        }
    }

    public void CheckCanvas()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + transform.up * 2, -transform.up);
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.gameObject.GetComponent<CanvasObject>() != null)
            {
                var legToCanvasCounter = 0;
                var lastHitCollider = new Collider();
                foreach (var legTarget in legTargets)
                {
                    RaycastHit legTargetHit;
                    Ray legTargetRay = new Ray(legTarget.transform.position + transform.up * 2, -transform.up);
                    if (Physics.Raycast(legTargetRay, out legTargetHit, 5f))
                    {
                        if (legToCanvasCounter == 0)
                        {
                            lastHitCollider = legTargetHit.collider;
                            if (legTargetHit.collider.gameObject.GetComponent<CanvasObject>() != null)
                            {
                                legToCanvasCounter += 1;
                            }
                        }

                        else if (legToCanvasCounter > 0)
                        {
                            if (legTargetHit.collider != lastHitCollider)
                            {
                                legToCanvasCounter = 0;
                            }
                            else
                            {
                                lastHitCollider = legTargetHit.collider;
                                if (legTargetHit.collider.gameObject.GetComponent<CanvasObject>() != null)
                                {
                                    legToCanvasCounter += 1;
                                }
                            }
                        }
                    }
                }
                if (legToCanvasCounter >= legTargets.Length - 1)
                {
                    hit.collider.gameObject.GetComponent<CanvasObject>().PaintMe();
                }
            }
        }

    }
}
