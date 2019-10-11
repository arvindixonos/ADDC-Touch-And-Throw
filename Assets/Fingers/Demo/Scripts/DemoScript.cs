//
// Fingers Gestures
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DigitalRubyShared
{
    public class DemoScript : MonoBehaviour
    {
   
        private LongPressGestureRecognizer longPressGesture;

        private GameObject draggingAsteroid;




        private void BeginDrag(float screenX, float screenY)
        {
            Vector3 pos = new Vector3(screenX, screenY, 0.0f);
            pos = Camera.main.ScreenToWorldPoint(pos);
            RaycastHit2D hit = Physics2D.CircleCast(pos, 10.0f, Vector2.zero);
            if (hit.transform != null && hit.transform.gameObject.name == "Asteroid")
            {
                draggingAsteroid = hit.transform.gameObject;
                draggingAsteroid.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                draggingAsteroid.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
            }
            else
            {
                longPressGesture.Reset();
            }
        }

        private void DragTo(float screenX, float screenY)
        {
            if (draggingAsteroid == null)
            {
                return;
            }

            Vector3 pos = new Vector3(screenX, screenY, 0.0f);
            pos = Camera.main.ScreenToWorldPoint(pos);
            draggingAsteroid.GetComponent<Rigidbody2D>().MovePosition(pos);
        }

        private void EndDrag(float velocityXScreen, float velocityYScreen)
        {
            if (draggingAsteroid == null)
            {
                return;
            }

            Vector3 origin = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector3 end = Camera.main.ScreenToWorldPoint(new Vector3(velocityXScreen, velocityYScreen, 0.0f));
            Vector3 velocity = (end - origin);
            draggingAsteroid.GetComponent<Rigidbody2D>().velocity = velocity;
            draggingAsteroid.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(5.0f, 45.0f);
            draggingAsteroid = null;

        }




        private void LongPressGestureCallback(DigitalRubyShared.GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Began)
            {
                BeginDrag(gesture.FocusX, gesture.FocusY);
            }
            else if (gesture.State == GestureRecognizerState.Executing)
            {
                DragTo(gesture.FocusX, gesture.FocusY);
            }
            else if (gesture.State == GestureRecognizerState.Ended)
            {
                EndDrag(longPressGesture.VelocityX, longPressGesture.VelocityY);
            }
        }

        private void CreateLongPressGesture()
        {
            longPressGesture = new LongPressGestureRecognizer();
            longPressGesture.MaximumNumberOfTouchesToTrack = 1;
            longPressGesture.StateUpdated += LongPressGestureCallback;
            FingersScript.Instance.AddGesture(longPressGesture);
        }




        private void Start()
        {

            CreateLongPressGesture();

         

            // prevent the one special no-pass button from passing through,
            //  even though the parent scroll view allows pass through (see FingerScript.PassThroughObjects)

            // show touches, only do this for debugging as it can interfere with other canvases
            FingersScript.Instance.ShowTouches = true;
        }

    }

}
