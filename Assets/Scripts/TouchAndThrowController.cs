using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class TouchAndThrowController : MonoBehaviour
{
    private TouchScreenController TouchScreenController;
    private Camera mainCamera;
    [SerializeField]
    private List<Transform> defaulTransforms = new List<Transform>();
    [SerializeField]
    private List<TouchAndThrowObject> TouchAndThrowObjects = new List<TouchAndThrowObject>();

    private float minVelocity = 5;
    private LongPressGestureRecognizer longPressGesture;
    private TouchAndThrowObject draggingTouchAndThrowObject;

    public void Init(TouchScreenController TouchScreenController)
    {
        this.TouchScreenController = TouchScreenController;
        mainCamera = Camera.main;

        for (int i = 0; i < TouchAndThrowObjects.Count; i++)
        {
            TouchAndThrowObjects[i].Init(this);
        }
        ResetTouchAndThrowObjects();

        CreateLongPressGesture();
        FingersScript.Instance.ShowTouches = false;
    }
    public void Reset()
    {
        ResetTouchAndThrowObjects();
    }
    public void ResetTouchAndThrowObjects()
    {
        for (int i = 0; i < TouchAndThrowObjects.Count; i++)
        {
            TouchAndThrowObjects[i].Move(defaulTransforms[i].position);
            TouchAndThrowObjects[i].SetAngularVelocity(0);
            TouchAndThrowObjects[i].SetLinearVelocity(Vector2.zero);
            TouchAndThrowObjects[i].SetRotation(0);
        }
    }
    private void CreateLongPressGesture()
    {
        //longPressGesture.Dispose();
        longPressGesture = new LongPressGestureRecognizer();
        longPressGesture.MinimumDurationSeconds = .05f;
        longPressGesture.MaximumNumberOfTouchesToTrack = 1;
        longPressGesture.StateUpdated += LongPressGestureCallback;
        FingersScript.Instance.AddGesture(longPressGesture);
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

    private void BeginDrag(float screenX, float screenY)
    {
        Vector3 pos = new Vector3(screenX, screenY, 0.0f);
        pos = mainCamera.ScreenToWorldPoint(pos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100f);
        if (hit.transform != null && hit.transform.gameObject.GetComponent<TouchAndThrowObject>())
        {
            draggingTouchAndThrowObject = hit.transform.gameObject.GetComponent<TouchAndThrowObject>();
            draggingTouchAndThrowObject.SetLinearVelocity(Vector2.zero);
            draggingTouchAndThrowObject.SetAngularVelocity(0);
        }
        else
        {
            longPressGesture.Reset();
        }
    }

    private void DragTo(float screenX, float screenY)
    {
        if (draggingTouchAndThrowObject == null)
        {
            return;
        }

        Vector3 pos = new Vector3(screenX, screenY, 0.0f);
        pos = mainCamera.ScreenToWorldPoint(pos);
        draggingTouchAndThrowObject.Move(pos);
    }

    private void EndDrag(float velocityXScreen, float velocityYScreen)
    {
        if (draggingTouchAndThrowObject == null)
        {
            return;
        }

        Vector3 origin = mainCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 end = mainCamera.ScreenToWorldPoint(new Vector3(velocityXScreen, velocityYScreen, 0.0f));
        Vector3 velocity = (end - origin);
        if (velocity.magnitude >= minVelocity)
        {
            draggingTouchAndThrowObject.SetLinearVelocity(velocity);
            draggingTouchAndThrowObject.SetAngularVelocity(UnityEngine.Random.Range(5.0f, 45.0f));
        }
        else
        {
            draggingTouchAndThrowObject.SetLinearVelocity(velocity);
            draggingTouchAndThrowObject.SetAngularVelocity(UnityEngine.Random.Range(5.0f, 45.0f));
            Invoke("ResetTouchAndThrowObjects", 1f);
        }
        draggingTouchAndThrowObject = null;
    }

    public void OnTouchAndThrowObjectGoesOFFScreen(TouchAndThrowObject touchAndThrowObject)
    {
        if (!mainCamera)
            return;

        //checkin if the object is above visible y axis of viewport
        Vector3 worldPosition = touchAndThrowObject.transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(worldPosition);

        if (viewportPosition.y > 1f)
        {
            Debug.Log("Object in +y axis");
            TouchScreenController.SendMessageToMonitors(touchAndThrowObject.ID);
        }
        else
        {
            Debug.Log("Object not in +y axis");
        }
        Invoke("ResetTouchAndThrowObjects", 2f);
    }

}


