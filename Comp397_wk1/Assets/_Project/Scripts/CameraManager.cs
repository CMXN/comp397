using UnityEngine;
using Unity.Cinemachine;

namespace Platformer397
{
    public class CameraManager : MonoBehaviour
    {
        //references to the cinemachineVirtual Camera adn the transform of our player
        [SerializeField] private CinemachineCamera freeLookCam;
        [SerializeField] private Transform player;
     //in awake, want to lock the mouse into the game view in unity and turn the cursor invisible

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if(player != null) {return;} 
            player = GameObject.FindWithTag("Player").transform;
            
        }


     //On Enable, I want to to associate the transform of our player into the target of your cinemachine camera
     private void OnEnable()
     {
        freeLookCam.Target.TrackingTarget = player;
     }

    }
}
