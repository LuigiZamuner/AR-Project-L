using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPose : MonoBehaviour
{
    [Header("Gameobject")]
    [SerializeField]
    private GameObject pokemon;

    [Header("finger points")]
    [SerializeField]
    private GameObject thumbFingerTop;
    [SerializeField]
    private GameObject thumbFingerBase;

    [SerializeField]
    private GameObject indexFingerTop;
    [SerializeField]
    private GameObject indexFingerBase;

    [SerializeField]
    private GameObject middleFingerTop;
    [SerializeField]
    private GameObject middleFingerBase;

    [SerializeField]
    private GameObject ringFingerTop;
    [SerializeField]
    private GameObject ringFingerBase;

    [SerializeField]
    private GameObject littleFingerTop;
    [SerializeField]
    private GameObject littleFingerBase;

    [Header("finger booleans")]
    public bool thumbUp;
    public bool indexUp;
    public bool middleUp ;
    public bool ringUp ;
    public bool littleUp ;

    [Header("Images")]
    [SerializeField]
    private Texture pikachu;
    [SerializeField]
    private Texture sadchu;
    [SerializeField]
    private Texture deadchu;
    [SerializeField]
    private Texture surfchu;
    [SerializeField]
    private Texture likechu;
    [SerializeField]
    private Texture bunnychu;

    private bool canChangeImage = true;

    // Update is called once per frame
    void Update()
    {

        GetFingerBools();
        GetPoses();
    }

    private void GetFingerBools()
    {
        //thumb finger confirmation
        if (indexFingerBase.transform.position.x > thumbFingerBase.transform.position.x)
        {
            if (thumbFingerTop.transform.position.x > thumbFingerBase.transform.position.x)
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
            if (thumbFingerTop.transform.position.x > thumbFingerBase.transform.position.x)
            {
                thumbUp = true;
            }
            else
            {
                thumbUp = false;
            }
        }

        //index finger confirmation
        if (indexFingerTop.transform.position.y <= indexFingerBase.transform.position.y)
        {
            indexUp = false;
        }
        else
        {
            indexUp = true;
        }

        //middle finger confirmation
        if (middleFingerTop.transform.position.y <= middleFingerBase.transform.position.y)
        {
            middleUp = false;
        }
        else
        {
            middleUp = true;
        }

        //ring finger confirmation
        if (ringFingerTop.transform.position.y <= ringFingerBase.transform.position.y)
        {
            ringUp = false;
        }
        else
        {
            ringUp = true;
        }

        //little finger confirmation
        if (littleFingerTop.transform.position.y <= littleFingerBase.transform.position.y)
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
        if(!thumbUp && indexUp && middleUp && !ringUp && !littleUp && canChangeImage)
        {
            Debug.Log("Bunnyyy");
            StartCoroutine(ImageTimer(1));
        }
        //GunPose
        if (thumbUp && indexUp && !middleUp && !ringUp && !littleUp && canChangeImage)
        {
            Debug.Log("dead");
            StartCoroutine(ImageTimer(2));
        }
        //HangLoosePose
        if (thumbUp && !indexUp && !middleUp && !ringUp && littleUp && canChangeImage)
        {
            Debug.Log("hangloose");
            StartCoroutine(ImageTimer(3));
        }
        //PunchPose
        if (!thumbUp && !indexUp && !middleUp && !ringUp && !littleUp && canChangeImage)
        {
            Debug.Log("punch");
            StartCoroutine(ImageTimer(4));
        }
        //likePose
        if (thumbUp && !indexUp && !middleUp && !ringUp && !littleUp && canChangeImage)
        {
            Debug.Log("like");
            StartCoroutine(ImageTimer(5));
        }
        if (!thumbUp && indexUp && !middleUp && !ringUp && !littleUp && canChangeImage)
        {
            Debug.Log("nerd");
            StartCoroutine(ImageTimer(5));
        }
    }

    private IEnumerator ImageTimer(int imageNumber)
    {
        canChangeImage = false;
        switch (imageNumber)
        {
            case 1:
                pokemon.GetComponent<Renderer>().material.mainTexture = bunnychu;
                break;
            case 2:
                pokemon.GetComponent<Renderer>().material.mainTexture = deadchu;
                break;
            case 3:
                pokemon.GetComponent<Renderer>().material.mainTexture = surfchu;
                break;
            case 4:
                pokemon.GetComponent<Renderer>().material.mainTexture = sadchu;
                break;
            case 5:
                pokemon.GetComponent<Renderer>().material.mainTexture = likechu;
                break;
        }
        yield return new WaitForSeconds(1.5f);

        pokemon.GetComponent<Renderer>().material.mainTexture = pikachu;
        canChangeImage = true;
    }
}
