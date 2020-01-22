using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    public Canvas MainUI;
    public Canvas BagUI;

    public void OpenBag () {
        MainUI.enabled = false;
        BagUI.enabled = true;
    }

    public void CloseBag () {
        BagUI.enabled = false;
        MainUI.enabled = true;
    }
}
