using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    Vector2 velocity;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    public AudioSource source;
    public AudioClip walkSound;
    private float nextFootstep = 0;
    public float footStepDelay;
    public bool changedSpeed = false;

    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (!player.GetOnGoingDialogue() && !PauseMenuController.IsMenuOpen)
        {
            // Move.
            IsRunning = canRun && Input.GetKey(runningKey);
            float movingSpeed = IsRunning ? runSpeed : speed;
            if (speedOverrides.Count > 0)
                movingSpeed = speedOverrides[speedOverrides.Count - 1]();
            velocity.y = Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime;
            velocity.x = Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime;
            transform.Translate(velocity.x, 0, velocity.y);

            nextFootstep -= Time.deltaTime;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)
                || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                if (nextFootstep <= 0)
                {
                    source.PlayOneShot(walkSound, 0.7f);
                    nextFootstep += footStepDelay;
                }
            }
            if (changedSpeed == false && Input.GetKeyDown(KeyCode.LeftShift))
            {
                footStepDelay /= 2;
                changedSpeed = true;
            }
            if (changedSpeed == true && Input.GetKeyUp(KeyCode.LeftShift))
            {
                footStepDelay *= 2;
                changedSpeed = false;
            }
        }
    }
}