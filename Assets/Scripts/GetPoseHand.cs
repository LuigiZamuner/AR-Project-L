using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPoseHand : MonoBehaviour
{
    [Header("finger booleans")]
    public bool thumbUp;
    public bool indexUp;
    public bool middleUp ;
    public bool ringUp ;
    public bool littleUp ;
    
    [Header("Gameobject")]
    [SerializeField]
    private GameObject pokemon;

    [Header("Hand Gameobject")]
    [SerializeField]
    private GameObject handGameobject;

    [Header("Animator")]
    [SerializeField]
    private Animator anim;

    private bool canChange = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            Debug.Log(PointListAnnotation.instance.importantPoints.Count + "opa2");
            GetFingerBools();
            GetPoses();

    }

      private void GetFingerBools()
      {

             //thumb finger confirmation
            if (PointListAnnotation.instance.importantPoints[5].gameObject.transform.position.x > PointListAnnotation.instance.importantPoints[2].gameObject.transform.position.x)
            {
                if (PointListAnnotation.instance.importantPoints[4].gameObject.transform.position.x > PointListAnnotation.instance.importantPoints[2].gameObject.transform.position.x)
                {
                    thumbUp = false;
                }
                else
                {
                    thumbUp = true;
                }
            }
            else
            {
                if (PointListAnnotation.instance.importantPoints[4].gameObject.transform.position.x > PointListAnnotation.instance.importantPoints[2].gameObject.transform.position.x)
                {
                    thumbUp = true;
                }
                else
                {
                    thumbUp = false;
                }
            }

            //index finger confirmation
            if (PointListAnnotation.instance.importantPoints[8].gameObject.transform.position.y <= PointListAnnotation.instance.importantPoints[5].gameObject.transform.position.y)
            {
                indexUp = false;
            }
            else
            {
                indexUp = true;
            }

            //middle finger confirmation
            if (PointListAnnotation.instance.importantPoints[12].gameObject.transform.position.y <= PointListAnnotation.instance.importantPoints[9].gameObject.transform.position.y)
            {
                middleUp = false;
            }
            else
            {
                middleUp = true;
            }

            //ring finger confirmation
            if (PointListAnnotation.instance.importantPoints[16].gameObject.transform.position.y <= PointListAnnotation.instance.importantPoints[13].gameObject.transform.position.y)
            {
                ringUp = false;
            }
            else
            {
                ringUp = true;
            }

            //little finger confirmation
            if (PointListAnnotation.instance.importantPoints[20].gameObject.transform.position.y <= PointListAnnotation.instance.importantPoints[17].gameObject.transform.position.y)
            {
                littleUp = false;
            }
            else
            {
                littleUp = true;
            }
      }

  
    private void GetPoses()
    {
        //bunnyPose
        if(!thumbUp && indexUp && middleUp && !ringUp && !littleUp && canChange)
        {
            Debug.Log("Bunnyyy");
            anim.SetTrigger("bunny");
            StartCoroutine(TimeToCanChange());
        }
        //GunPose
        if (thumbUp && indexUp && !middleUp && !ringUp && !littleUp && canChange)
        {
            Debug.Log("dead");
            anim.SetTrigger("dead");
            StartCoroutine(TimeToCanChange());
        }
        //HangLoosePose
        if (thumbUp && !indexUp && !middleUp && !ringUp && littleUp && canChange)
        {
            Debug.Log("hangloose");
            anim.SetTrigger("hangloose");
            StartCoroutine(TimeToCanChange());
        }
        //PunchPose
        if (!thumbUp && !indexUp && !middleUp && !ringUp && !littleUp && canChange)
        {
            Debug.Log("punch");
            anim.SetTrigger("punch");
            StartCoroutine(TimeToCanChange());
        }
        //likePose
        if (thumbUp && !indexUp && !middleUp && !ringUp && !littleUp && canChange)
        {
            Debug.Log("like");
            anim.SetTrigger("like");
            StartCoroutine(TimeToCanChange());
        }
        if (!thumbUp && indexUp && !middleUp && !ringUp && !littleUp && canChange)
        {
            Debug.Log("nerd");
        }
    }

    private IEnumerator TimeToCanChange()
    {
        canChange = false;
        yield return new WaitForSeconds(4);
        canChange = true;
    }
}
