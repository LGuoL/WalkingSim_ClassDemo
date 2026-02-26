using System;
using TMPro;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //action is a delegate
    //a delegate is a var that can store a function
    //int number vs action myFunction
    //can be used by anyone


    //static belongs to the class itself not a specific instance meaning we dont need a reference the specific game object
    //ou can just += instead of FindObjsectOfType
    //one shared version across the entire program

    //event is a special type of delegate
    //it is protected if u do this without event it can break
    //other scripts can subscribe and unsubscribe but they can not invoke it

    public static event Action onButtonPressed;


    public void OnButtonPressed()
    {
        onButtonPressed?.Invoke();
    }
    


    //this is not sclable, if we want 10 things to react when we press the button
    //then we would have 10 freferences and have to keep changing this script
    //also what if we delete something later? it can break this script
    /*private void PressMe()
    {
        Light light = GetComponent<Light>();
        TextMeshProUGUI statusText = GetComponent<TextMeshProUGUI>();

        light.color = Color.white;
        statusText.text = "Pressed";
    }*/
}
