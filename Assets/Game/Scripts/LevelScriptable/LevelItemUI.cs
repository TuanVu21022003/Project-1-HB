using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeveItemUI : MonoBehaviour
{
    [SerializeField] private Text textLevel;

    public void OnInit(string textIndex)
    {
        textLevel.text = "Level " + textIndex;
    }
}
