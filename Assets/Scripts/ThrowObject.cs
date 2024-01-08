using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class ThrowObject : MonoBehaviour
{
    [SerializeField]
    private GameObject pokeball;

    [SerializeField]
    private GameObject pokemon;

    [SerializeField]
    private PokeballManager pokeballManager;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private float rotationSupport;

    private Rigidbody rbInstatiatedObj;
    private Camera arCamera;
    private MovementTouchSupport touchSupport;


    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        touchSupport = GetComponent<MovementTouchSupport>();
        arCamera = Camera.main;

    }
    private void OnEnable()
    {
        ETouch.EnhancedTouchSupport.Enable();
        ETouch.TouchSimulation.Enable();
        ETouch.Touch.onFingerDown += OnFingerDown;
        ETouch.Touch.onFingerMove += OnFingerMove;
        ETouch.Touch.onFingerUp += LoseFinger;
    }
    private void OnDisable()
    {
        ETouch.EnhancedTouchSupport.Disable();
        ETouch.TouchSimulation.Disable();
        ETouch.Touch.onFingerDown -= OnFingerDown;
        ETouch.Touch.onFingerMove -= OnFingerMove;
        ETouch.Touch.onFingerUp -= LoseFinger;
    }

    private void OnFingerDown(Finger finger)
    {
        Vector2 touchPosition = finger.currentTouch.screenPosition;
        Vector3 worldPosition = arCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, arCamera.nearClipPlane + 1f));


        //pokeball mode
        if (touchSupport.buttonIndex == 1 )
        {
            if (finger.index != 0)
            {
                return;
            }


            rbInstatiatedObj = pokeballManager.SpawnPokeball().GetComponent<Rigidbody>();
            rbInstatiatedObj.transform.position = new Vector3(touchPosition.x, touchPosition.y, 0);
            rbInstatiatedObj.transform.rotation = arCamera.transform.rotation;
        }
        //rotate mode
        if (touchSupport.buttonIndex == 2)
        {

        }

    }
    private void OnFingerMove(Finger finger)
    {
        Vector2 touchPosition = finger.currentTouch.screenPosition;
        Vector3 worldPosition = arCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, arCamera.nearClipPlane + 1f));
        //pokeball mode
        if (touchSupport.buttonIndex == 1 && touchPosition.y > 140)
        {
            if (finger.index != 0)
                return;
            if (rbInstatiatedObj.velocity == Vector3.zero)
            {
                rbInstatiatedObj.transform.position = worldPosition ;
            }
        }
        //rotate mode
        if (touchSupport.buttonIndex == 2 && touchPosition.y > 140)
        {

            rotationSupport = worldPosition.x * 200;
            pokemon.transform.rotation = Quaternion.Euler(0, -rotationSupport, 0);

        }

    }
    private void LoseFinger(Finger finger)
    {
        //pokeball mode
        if (touchSupport.buttonIndex == 1)
        {
            if (finger.index != 0)
                return;

            rbInstatiatedObj.AddForce(new Vector3(0, 1, 1) * 5.5f, ForceMode.Impulse);
            StartCoroutine(rbInstatiatedObj.GetComponent<Pokeball>().Lifetime());
        }
        if (touchSupport.buttonIndex == 2)
        {

        }
    }

   
}
