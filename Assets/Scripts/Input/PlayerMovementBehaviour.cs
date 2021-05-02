using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementBehaviour : MonoBehaviour
{
    //A variable used to store and adjust the player's movement speed
    public float speed;

    //A variable used to store the value of the collectables
    private int score;

    //A reference to the UI Text
    public TextMeshProUGUI scoreText;

    //A variable used to store the player's mouse position
    private Vector3 _mousePosition;

    //A ray variable used to represent a ray to the mouse
    private Ray _mouseRay;

    //A reference to the CharacterController
    public CharacterController characterController;

    //Start is called before the first frame update
    void Start()
    {
        score = 0;

        SetScoreText();
    }

    /// <summary>
    /// A function that displays the number of collectables found
    /// </summary>
   void SetScoreText()
   {
        scoreText.text = "Score: " + score.ToString();
   }

    //Update is called once per frame
    void FixedUpdate()
    {
        //A new vector3 direction that x is set to "Horizontal" and y is set to "Vertical"
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //If the Left mouse button is pressed
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Sets the _mousePos variable to be the mouse position
            _mousePosition = Input.mousePosition;

            //Sets the _mouseRay variable to the main camera and calls the ScreenPointToRay function with _mousePos passed in
            _mouseRay = Camera.main.ScreenPointToRay(_mousePosition);

            //Creates a plane at the player's position
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            //Finds how far from the camera the ray intersects the plane;
            float rayDistance = 0.0f;
            playerPlane.Raycast(_mouseRay, out rayDistance);

            //Sets targetpoint to be the ray distance to the plane
            Vector3 targetpoint = _mouseRay.GetPoint(rayDistance);

            //Gets the direction
            direction = (targetpoint - transform.position).normalized;
        }

        //Velocity variable is set to the direction scaled by the player's speed
        Vector3 velocity = direction * speed;

        //Moves the rigidbody to position
        characterController.SimpleMove(velocity);
    }

    /// <summary>
    /// A function that checks if the player has collided with the collectables and sets the collectables to false and collects them
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            score++;

            SetScoreText();
        }
    }
}
