using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordPuzzle : MonoBehaviour
{
    [SerializeField] private string Code;
    [SerializeField] private NewPlayerMovement newPlayerController;
    [SerializeField] private GameObject bookUI;
    private GameObject inputText;

    private bool canInteract = false;
    private bool isDoor1Open;

    void Start()
    {
        inputText = bookUI.transform.Find("Input").GetChild(0).Find("Text").gameObject;
    }
    private void Update()
    {
        Keybinds(); // Fires function KeyBinds() 
    }
    private void Keybinds()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract) // Checks if the player can interact and is pressing E
        {
            Cursor.lockState = CursorLockMode.None; // Unlocking the cursor
            

            newPlayerController.canJump = false;
            newPlayerController.canLook = false;
            newPlayerController.canWalk = false;
        }

        // Activating the book gui
        bookUI.SetActive(true);
    }
    public void _ConfirmButton()
    {
        string stringInput = inputText.GetComponent<TMP_Text>().text; // Getting the string
        int openDoor = -1;

        stringInput = stringInput.ToLower(); // Lowering the string input

        // Setting the correct chars to 0
        int correctCharsDoor1 = 0;

        // Checking if the string is the same
        if (stringInput.Length == Code.Length + 1 && !isDoor1Open)
        {
            for (int i = 0; i < Code.Length; i++)
            {
                if (Code[i] == stringInput[i])
                {
                    correctCharsDoor1 += 1;
                }
            }
        }

        // Checking if the string is the same
        if (correctCharsDoor1 == Code.Length)
        {
            isDoor1Open = true;
            openDoor = 1;
        }

        if (openDoor != -1) // If the anwser was right
        {
            bookUI.SetActive(false); // Disabling the bookui
            Cursor.lockState = CursorLockMode.Locked; // Locking the cursor

            newPlayerController.canJump = true;
            newPlayerController.canLook = true;
            newPlayerController.canWalk = true;

            // Opening the correct door
           // crawlDoorHandler.OpenDoor(openDoor);
        }

    }
    public void _ExitUI()
    {
        bookUI.gameObject.SetActive(false); // Disabling the bookui
        Cursor.lockState = CursorLockMode.Locked; // Locking the cursor

       

        newPlayerController.canJump = true;
        newPlayerController.canLook = true;
        newPlayerController.canWalk = true;
    }
}
