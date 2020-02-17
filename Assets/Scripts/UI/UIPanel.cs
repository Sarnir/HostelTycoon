using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    protected bool needsInit = true;

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void Open()
    {
        if (needsInit)
            Init();

        OnOpened();
        this.gameObject.SetActive(true);
    }

    virtual protected void Init()
    {
        needsInit = false;
    }

    virtual protected void OnOpened()
    {
        Debug.Log(name + " was opened.");
    }
}
