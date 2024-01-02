using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPoseHand : MonoBehaviour
{
    


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
            GetPoses();

    }
  
    private void GetPoses()
    {
        //bunnyPose
        if(!HandTrackingSolution.Instance.indexDown && !HandTrackingSolution.Instance.middleDown && HandTrackingSolution.Instance.ringDown && HandTrackingSolution.Instance.littleDown && canChange)
        {
            Debug.Log("Bunnyyy");
            anim.SetTrigger("bunny");
            StartCoroutine(TimeToCanChange());
        }
        //GunPose
        if (!HandTrackingSolution.Instance.indexDown && HandTrackingSolution.Instance.middleDown && HandTrackingSolution.Instance.ringDown && HandTrackingSolution.Instance.littleDown && canChange)
        {
            Debug.Log("dead");
            anim.SetTrigger("dead");
            StartCoroutine(TimeToCanChange());
        }
        //PunchPose
        if (HandTrackingSolution.Instance.indexDown && HandTrackingSolution.Instance.middleDown && HandTrackingSolution.Instance.ringDown && HandTrackingSolution.Instance.littleDown && canChange)
        {
            Debug.Log("punch");
            anim.SetTrigger("punch");
            StartCoroutine(TimeToCanChange());
        }
        //likePose
        if (HandTrackingSolution.Instance.indexDown && HandTrackingSolution.Instance.middleDown && HandTrackingSolution.Instance.ringDown && !HandTrackingSolution.Instance.littleDown && canChange)
        {
            Debug.Log("like");
            anim.SetTrigger("like");
            StartCoroutine(TimeToCanChange());
        }
    }

    private IEnumerator TimeToCanChange()
    {
        canChange = false;
        yield return new WaitForSeconds(4);
        canChange = true;
    }
}
