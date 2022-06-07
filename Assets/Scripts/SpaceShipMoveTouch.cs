using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMoveTouch : MonoBehaviour
{
   // public const string ON_COROUTINE = "MoveTowardsTouch";
    Rigidbody2D rb;

     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        GalaxySpaceShooter.UserInput.OnPanGestureMovement += PositionOfShip;
    }
    private void OnDisable()
    {
        GalaxySpaceShooter.UserInput.OnPanGestureMovement -= PositionOfShip;
    }



    public void PositionOfShip(Touch touch)
    {
        Debug.Log("Method");
        Vector3 shipPosition = Camera.main.ScreenToWorldPoint(touch.position); //Changing the pixelcoordinate to world coordinates
        shipPosition.z = transform.position.z;

        shipPosition.y = transform.position.y;

        float distance = Vector3.Distance(transform.position, shipPosition); //Calculating distance between player current position and touch position
        for (float i = 0; i < distance; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, shipPosition, i); // Transforming from current position slowly to the touch position 
           // yield return null;
        }
        transform.position = shipPosition;
    }


    
}
