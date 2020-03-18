using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    protected Animator animator;

    protected PersonData data;
    protected Sprite sprite;
    protected Wallet wallet;
    protected Hostel hostel;

    public string Name { get { return data.Name; } }
    public Sex Sex { get { return data.Sex; } }
    public Sprite Avatar { get { return data.Avatar; } }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Init(Hostel hostelToStayIn, PersonData pData)
    {
        name = pData.Name;

        transform.parent = hostelToStayIn.World.transform;
        transform.position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);

        hostel = hostelToStayIn;
        data = pData;

        wallet = new Wallet(Random.Range(20f, 100f));
    }

    public bool Pay(float price, string remark = null)
    {
        if (wallet.CanAfford(price))
        {
            wallet.Pay(price, hostel.GetWallet(), remark);
            return true;
        }
        else
        {
            Debug.Log($"{ data.Name } can't pay { price }, he has only { wallet.Money }");
            return false;
        }
    }

    public PersonData GetData()
    {
        return data;
    }
}