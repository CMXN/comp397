using UnityEngine;

namespace Platformer397
{
    [RequireComponent(typeof(Rigidbody))] //object requires this and will make one for it if it does not have it
    public class PlayerController : Subject
    {
        //player comps
        [SerializeField] private InputReader input;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Vector3 movement;
        [SerializeField] private Transform mainCam;

        //editable stats
        [SerializeField] private float moveSpeed = 200f;
        [SerializeField] private float rotationSpeed = 200f;


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            mainCam = Camera.main.transform;
        }
//
        private void Start()
        {
            input.EnablePlayerActions();
            NotifyObservers();
        }
        private void OnEnable()
        {
            input.Move += GetMovement;
        }
        private void OnDisable()
        {
            Debug.Log("[OnDisable]");
        }
        private void OnDestroy()
        {
            Debug.Log("[Destroy]");
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void HandleMovement(Vector3 adjustedMovement)
        {
            var velocity = adjustedMovement * moveSpeed * Time.fixedDeltaTime;
            rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        }

        private void HandleRotation(Vector3 adjustedMovement)
        {
            var targetRotation = Quaternion.LookRotation(adjustedMovement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * 
            Time.deltaTime);
            
        }

        private void UpdateMovement()
        {
            // float speed = 0f;
            // int moreSpeed = 1;
            // double evenMoreSpeed = 0.0f;
            //var auto identification of type but it has to be given a value to do so

            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            if(adjustedDirection.magnitude > 0f)
            {
                //handle the rotation and movement
                HandleRotation(adjustedDirection);
                HandleMovement(adjustedDirection);
            }
            else{
                //not change the rotation or movement, but need to apply rigidbody Y movement for gravity
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            }
        }

        private void GetMovement(Vector2 move)
        {
            movement.x = move.x;
            movement.z = move.y;
        }
    }
}
