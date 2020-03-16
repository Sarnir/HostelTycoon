using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSprite : MonoBehaviour
{
    Person person;

    public void Init(Person p)
    {
        person = p;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

public class Person : MonoBehaviour
{
    protected PersonData data;
    protected Sprite sprite;
    protected Wallet wallet;
    protected Hostel hostel;

    public string Name { get { return data.Name; } }
    public Sex Sex { get { return data.Sex; } }
    public Sprite Avatar { get { return data.Avatar; } }

    public virtual void Init(Hostel hostelToStayIn, PersonData pData)
    {
        transform.parent = hostelToStayIn.transform;
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

    /*public PersonSprite Spawn()
    {
        if (sprite != null)
            Debug.LogError("SPRITE IS ALREADY INSTANTIATED");

        sprite = hostel.World.SpawnPerson(this);

        return sprite;
    }*/

    public void Despawn()
    {
        Destroy(gameObject);
    }
}