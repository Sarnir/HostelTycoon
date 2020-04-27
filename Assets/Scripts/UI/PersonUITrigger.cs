using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonUITrigger : MonoBehaviour
{
    CharacterDetailsPanel PanelToOpen = default;
    Person owner;

    private void Start()
    {
        owner = GetComponentInParent<Person>();
        PanelToOpen = owner.hostel.UIManager.CharacterDetailsPanel;
    }

    private void OnMouseUp()
    {
        (PanelToOpen.transform as RectTransform).pivot = new Vector2(
            Input.mousePosition.x < Screen.width * 0.5f ? 0f : 1f,
            Input.mousePosition.y < Screen.height * 0.5f ? 0f : 1f);
        PanelToOpen.transform.position = Input.mousePosition;

        PanelToOpen.SetTarget(owner);
        PanelToOpen.Open();
    }
}
