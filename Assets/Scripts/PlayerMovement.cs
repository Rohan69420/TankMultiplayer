using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();

    private bool forward = false;
    private bool left = false;
    private bool right = false;

    // Update is called once per frame
    void Update()
    {
       GetBodyMovement();
       GetTurretMovement();
       GetShootingInput();

       //mobile control
       TankControl();
    }

    private void GetShootingInput(){
        if(Input.GetMouseButtonDown(0)){
            OnShoot?.Invoke();
        }
    }

    private void GetTurretMovement(){
        OnMoveTurret?.Invoke(GetMousePosition());

    }

    private Vector2 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }

    private void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);
    }

    //for mobile controllers
    public void TankControl()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
    if (forward){
       movementVector.y = 1;
    }
    if(left && !right){
       movementVector.x = -1;
    }
    else if(!left && right){
       movementVector.x = 1;
    }
   
        OnMoveBody?.Invoke(movementVector.normalized);
        
    }

    public void ForwardDown(){
        forward = true;
    }
    
    public void ForwardUp(){
        forward = false;
    }

    public void LeftDown(){
        left = true;
    }

    public void LeftUp(){
        left = false;
    }

    public void RightDown(){
        right = true;
    }

    public void RightUp(){
        right = false;
    }
}
