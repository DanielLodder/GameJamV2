using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordPuzzle : MonoBehaviour
{
    [SerializeField] private string code;
    [SerializeField] private PlayerMovement playerController;
    [SerializeField] private GameObject crateUI;
    [SerializeField] private GameObject interactionUI;
    [SerializeField] private GameObject topCrate;
    private GameObject inputText;

    private bool canInteract = false;
    private bool codeUsed;

    void Start()
    {
        inputText = crateUI.transform.Find("Input").GetChild(0).Find("Text").gameObject;
    }
    private void Update()
    {
        Keybinds(); // Fires function KeyBinds() 
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) // Checks if the collider that entered, is the collider of the player
        {
            if (codeUsed == false) // Checks if there is any door closed
            {
                canInteract = true; // Sets canInteract to true
                interactionUI.SetActive(true); // Activates the gui
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player")) // Checks if the collider that left, is the collider of the player
        {
            canInteract = false; // Sets canInteract to false
            interactionUI.SetActive(false); // Deactivates the gui
        }
    }

    private void Keybinds()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract) // Checks if the player can interact and is pressing E
        {
            playerController.canLook = false;
            playerController.canWalk = false;

            // Activating the book gui
            crateUI.SetActive(true);
        }

        
    }
    public void _ConfirmButton()
    {
        Debug.Log("Confirmed");
        string stringInput = inputText.GetComponent<TMP_Text>().text; // Getting the string
        int openCrate = -1;

        stringInput = stringInput.ToLower(); // Lowering the string input

        // Setting the correct chars to 0
        int correctCharCrate = 0;

        // Checking if the string is the same
        if (stringInput.Length == code.Length + 1 && !codeUsed)
        {
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == stringInput[i])
                {
                    Debug.Log(correctCharCrate);
                    correctCharCrate += 1;
                }
            }
        }

        // Checking if the string is the same
        if (correctCharCrate + 1 == code.Length)
        {
            Debug.Log("Code is correct");
            codeUsed = true;
            openCrate = 1;
        }

        if (openCrate != -1) // If the anwser was right
        {
            crateUI.SetActive(false); // Disabling the bookui
            Cursor.lockState = CursorLockMode.Locked; // Locking the cursor
            topCrate.SetActive(false);
            playerController.canLook = true;
            playerController.canWalk = true;

            // Opening the correct door
           // crawlDoorHandler.OpenDoor(openDoor);
        }

    }
    public void _ExitUI()
    {
        crateUI.gameObject.SetActive(false); // Disabling the bookui
        Cursor.lockState = CursorLockMode.Locked; // Locking the cursor

       

        playerController.canLook = true;
        playerController.canWalk = true;
    }
}
