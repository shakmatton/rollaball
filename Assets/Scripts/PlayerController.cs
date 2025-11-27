// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;                    

    // Number of picked-up objects
    private int count;
    
    // Player movement axis
    private float movementX;
    private float movementY;
    
    // Speed at which the player moves (not initialized here, but inside Unity editor).
    public float speed;    

    // UI for the counter score.
    public TextMeshProUGUI countText;

    // UI for the winning message.
    public GameObject winTextObject; 
    
    
    
    // Start is called before the first frame update.
    void Start()
    {
        rb = GetComponent<Rigidbody>();     // Get & store Rigidbody component attached to player.
        count = 0;                          // Starting picked-up objects quantity. 
        
        SetCountText();                     // Screen starts with Count = 0.
        winTextObject.SetActive(false);     // Victory message initially hidden.
    }
    
    
    // Function called when a move input is detected.
    void OnMove(InputValue movementValue)   
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }
    
    
    // Logic for the UI counter score.
    void SetCountText()
    {
        // resulting int value converted to string text
        countText.text = "Count: " + count.ToString();   
        
        if (count >= 8){                    // number of total pickup objects: 8.
            winTextObject.SetActive(true);  // displays winning message
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
   
    
    // Function activated when player object enters another pickup object space.
    void OnTriggerEnter(Collider other)     // other = pickup object
    {
        if (other.gameObject.CompareTag("PickUp")) {    // if is a "pickable" object   
            other.gameObject.SetActive(false);          // picked-up object is vanished
            count++;                                    // counting adds +1 to the score 
            SetCountText();               // calls function for displaying updated score
        }
    }
    
    
    // FixedUpdate called once per fixed frame-rate frame.
    private void FixedUpdate()               
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        // speed is controlled in the Unity panel
        rb.AddForce(movement * speed);      
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }        
    }
    
}















