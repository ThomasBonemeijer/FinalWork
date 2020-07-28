using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceScreenHandler : MonoBehaviour
{
    public Image backGImage;
    public GameObject choiceObject;
    public GameObject UIRoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MakeChoice(bool theChoice) {
        if (theChoice == true) {
            // Debug.Log("Accepted");
            if (choiceObject.name.Contains("Boat")) {
                choiceObject.GetComponent<BoatHandler>().isSkipTutorialBoat = false;
                choiceObject.GetComponent<BoatHandler>().SwitchScene();
            }
            else if (choiceObject.name == "UI") {
                choiceObject.GetComponent<UIHandler>().SaveAndExit();
            }
            UIRoot.GetComponent<UIHandler>().gameManager.GetComponent<CanvasHandler>().CloseChoiceScreen();
        } else {
            // Debug.Log("Decline");
            UIRoot.GetComponent<UIHandler>().gameManager.GetComponent<CanvasHandler>().CloseChoiceScreen();
        }
    }
}
