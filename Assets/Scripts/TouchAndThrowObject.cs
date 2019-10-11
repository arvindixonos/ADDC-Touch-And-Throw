using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAndThrowObject : MonoBehaviour
{
    [SerializeField]
    public string ID;
    private Rigidbody2D rigidbody;
    private TouchAndThrowController TouchAndThrowControllerReference;

    private void Start()
    {

    }
    public void Init(TouchAndThrowController TouchAndThrowControllerReference)
    {
        rigidbody = GetComponent<Rigidbody2D>();
        this.TouchAndThrowControllerReference = TouchAndThrowControllerReference;
    }
    public void Move(Vector3 position)
    {
        rigidbody.MovePosition(position);
    }
    public void SetLinearVelocity(Vector2 velocity)
    {
        rigidbody.velocity = velocity;
    }
    public void SetAngularVelocity(float angularVelocity)
    {
        rigidbody.angularVelocity = angularVelocity;
    }
    public void SetRotation(float rotationZ)
    {
        transform.rotation = Quaternion.Euler(0,0,rotationZ);
    }
    public void SetLayer(int layerID)
    {

    }
    private void OnBecameInvisible()
    {
        TouchAndThrowControllerReference.OnTouchAndThrowObjectGoesOFFScreen(this);
    }
}
