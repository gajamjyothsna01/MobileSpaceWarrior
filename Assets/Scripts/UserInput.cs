using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GalaxySpaceShooter
{
    public class UserInput : MonoBehaviour
    {

        #region EVENTMETHODS
        //when the user presses and drags on the screen. if the user removes too quickly then its considered as tap
        public delegate void PanGestureMovement(Touch t);
        public static event PanGestureMovement OnPanGestureMovement;

        public delegate void TapGesture(Touch t);
        public static event TapGesture OnTapGesture;
        /*

        public delegate void PanHeldAction(Touch t);
        public static event PanHeldAction OnPanHeld;

        public delegate void PanEndedAction(Touch t);
        public static event PanEndedAction OnPanEnded; */

        //To check accelerometer action
        public delegate void AccelerometerChangedAction(Vector3 acceleration);
        public static event AccelerometerChangedAction OnAccelerometerChanged;

        #endregion



        // Start is called before the first frame update
        #region PUBLIC VARIABLES
        public float tapMaxMovement = 50; //Maximum pixel tap can move
        public float panMinTime = 0.4f;//tap gesture lasts more than minumum time


        #endregion

        #region PRIVATE VARAIBLES
        private Vector2 movement;           //Movement vector will track who far you move.

       private bool tapGestureFailed = false;  //tap Gesture will become,
        private float startTime;//will keep time when our gesture begins
        private Vector3 defaultAcceleration;
        private bool panGestureRecognized = false;// when we recognize gesture we gone make true
        #endregion

        #region MONOBEHAVIOUR METHODS
        #endregion
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {
            if (OnAccelerometerChanged != null)
            {
                Vector3 acceleration = new Vector3(Input.acceleration.x, Input.acceleration.y, -1 * Input.acceleration.z);
                acceleration -= defaultAcceleration;
                OnAccelerometerChanged(acceleration);
            }
            if (Input.touchCount > 0) //To finding out,no.of touches greater than 0 or not. If no touches, then no movement.
            {
                Touch touch = Input.touches[0]; //Need to find out, number of touches on screen. If there are more no.oc touches, need to call array.
                if (touch.phase == TouchPhase.Began) // We have several touch phases, began enters the first frame of the touch.
                {
                   // movement = Vector2.zero; //We made our movement to zero.
                    startTime = Time.time;

                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    if (Time.time - startTime >= 0.1f)  //Enabling pangesture
                    {
                        Debug.Log("PanGesture Enabled");


                        if (!panGestureRecognized)
                        {

                            //isGestureRecognised = true;
                            if (OnPanGestureMovement != null)
                            {
                                OnPanGestureMovement(touch);
                            }

                        }
                    }
                    if (!tapGestureFailed)
                    {
                        if (OnTapGesture != null)
                        {
                            OnTapGesture(touch);
                        }
                    }

                }
                else
                {
                    panGestureRecognized = false; // ready for the next pan gesture
                    tapGestureFailed = false; //Making ready for the next tap.
                }










            }
            void OnEnable()
            {
                defaultAcceleration = new Vector3(Input.acceleration.x, Input.acceleration.y, -1 * Input.acceleration.z);
            }




        }
        #region MY PUBLIC METHODS
        #endregion

    }
}


