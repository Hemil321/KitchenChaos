using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //Singleton pattern
    //Make the player singleton, because in this game, we will only have one instance of the player
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7f;

    [SerializeField] private GameInput gameInput;

    //Layer mask is a useful field for only doing raycast on a specific type of objects which are in the countersLayerMask here
    [SerializeField] private LayerMask countersLayerMask;

    [SerializeField] private Transform kitchenObjectHoldPoint;

    //This event will be fired when the player is looking at a different counter and it will be recieved by the selectedCounterVisual to update the visual on the correct counter
    public EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public event EventHandler OnPickedSomething;
    public event EventHandler OnDroppedSomething;

    //The event recieves the selected counter as its argument
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    private bool isWalking;
    private Vector3 lastInteractionDir;
    private BaseCounter selectedCounter;
    //Kitchen object is the object that the player is holding
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one instance of player!");
        }
        Instance = this;
    }

    //Event subscribing should be done in Start function
    private void Start()
    {
        //we will append the GameInput_OnInteractAction function as a subscriber to the OnInteractAction event
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    //Game input will fire off an event called onInteract, when player presses the interact key
    private void GameInput_OnInteractAction(object Sender, System.EventArgs e)
    {
        //Check if we are currently playing the game
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        //If the player interacts with a counter, then call the interact function of that counter
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    //Intercat Alternate is only useful for the cutting counter object
    private void GameInput_OnInteractAlternateAction(object Sender, System.EventArgs e)
    {
        //Check if we are currently playing the game
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    //Return if the player is walking or not, to update the animations
    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()
    {
        //Get the input vector based on the keys the player is pressing
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            //If we don't do this, then when the player stops moving and is looking at a counter, it will not update that counter's animation to selected
            lastInteractionDir = moveDir;
        }

        float interactDistance = 2f;
        //Raycast from the player to the direction he is facing and if the raycast hit something, then we will try to get its base counter component
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) { 
            if (raycastHit.transform.TryGetComponent(out BaseCounter counter))
            {
                //Has ClearCounter
                //We will set the selected counter to the counter that was on the way of the raycast
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerRadius = .7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;

        //Capsule cast needs to be done, because a normal raycast fires off a ray from the player's center, which can lead to the player moving in an object
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //Cannot move towards MoveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -0.5f || moveDir.x > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move on the X

                //Attempt only Z Movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -0.5f || moveDir.z > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can move on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move on either directions
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;

        //slerp function is used to change the player's forward direction slowly and not instantly
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }
    public void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        //this event will be fired off to change the visual to a different counter, if the player is looking at something else
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

    //These are all the IKitchenObjectParent Interface's functions
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if( kitchenObject != null )
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;

        OnDroppedSomething?.Invoke(this, EventArgs.Empty);
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
