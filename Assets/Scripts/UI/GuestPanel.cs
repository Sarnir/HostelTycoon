using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestPanel : MonoBehaviour
{
    public Image Avatar;
    public Text NameText;
    public Text LengthOfStayText;

    public void Init(Sprite avatar, string name, int lengthOfStay)
    {
        Avatar.sprite = avatar;
        NameText.text = name;
        LengthOfStayText.text = "Staying for " + lengthOfStay + " days";
    }
}
