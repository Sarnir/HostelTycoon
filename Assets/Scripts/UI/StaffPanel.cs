using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffPanel : UIPanel
{
    [SerializeField]
    Image myStaffBookmark = default;
    [SerializeField]
    Image hireEmployeesBookmark = default;

    [SerializeField]
    UIPanel myStaffPage = default;
    [SerializeField]
    UIPanel hireEmployeesPage = default;

    UIPanel currentPage;
    Image currentBookmark;

    [SerializeField]
    Color activeColor = default;
    [SerializeField]
    Color inactiveColor = default;

    protected override void Init()
    {
        base.Init();

        OpenMyStaffPage();
    }

    public void OpenMyStaffPage()
    {
        OpenPage(myStaffBookmark, myStaffPage);
    }

    public void OpenHireEmployeesPage()
    {
        OpenPage(hireEmployeesBookmark, hireEmployeesPage);
    }

    void OpenPage(Image bookmark, UIPanel page)
    {
        SelectBookmark(bookmark);

        if (currentPage != null)
            currentPage.Close();

        page.Open();
        currentPage = page;
    }

    void SelectBookmark(Image bookmark)
    {
        if(currentBookmark != null)
        {
            currentBookmark.color = inactiveColor;
        }

        bookmark.color = activeColor;
        currentBookmark = bookmark;
    }
}
