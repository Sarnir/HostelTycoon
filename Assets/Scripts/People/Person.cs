using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public Animator Animator;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected StateMachine stateMachine;

    protected PersonData data;
    //protected Sprite sprite;
    protected Wallet wallet;
    public Hostel hostel;

    public float speed = 0.5f;
    Vector2 direction;

    [HideInInspector]
    public Vector2 desiredPosition;

    public Tile CurrentTile { get { return hostel.World.GetTileAtPosition(rb.position.x, rb.position.y); } }

    public Vector2 CurrentPosition {  get { return rb.position; } }

    public string Name { get { return data.Name; } }
    public Sex Sex { get { return data.Sex; } }
    public Sprite Avatar { get { return data.Avatar; } }

    [SerializeField]
    float sleepingNeedGrowthRate = 0.00013f;

    [HideInInspector]
    public float SleepIncreaseModifier = 1f;
    public float SleepingNeed { get; private set; }

    public bool IsOut { get { return !sr.enabled; } set { sr.enabled = !value; } }

    public int MoneyAmount { get { return (int)wallet.Money; } }
    public string CurrentState { get { return stateMachine.CurrentStateString; } }

    public Vector2 CenterPosition { get { return sr.bounds.center; } }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
        sr = GetComponent<SpriteRenderer>();
    }

    public virtual void Init(Hostel hostelToStayIn, PersonData pData)
    {
        name = pData.Name;

        Animator.SetFloat("Offset", Random.value);
        Animator.SetFloat("DirectionX", (Random.value - 0.5f) * 2f);
        Animator.SetFloat("DirectionY", (Random.value - 0.5f) * 2f);

        rb = GetComponent<Rigidbody2D>();

        transform.parent = hostelToStayIn.World.transform;
        transform.position = hostelToStayIn.World.SpawnPoint.position;

        hostel = hostelToStayIn;
        data = pData;

        wallet = new Wallet(Random.Range(20f, 100f));

        SleepingNeed = 0.4f + 0.2f * Random.value;
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

    protected virtual void Update()
    {
        if(rb.position != desiredPosition)
        {
            direction = (desiredPosition - rb.position).normalized;
            Animator.SetFloat("DirectionX", direction.x);
            Animator.SetFloat("DirectionY", direction.y);

            //?????? czemu to służy
            //rb.MovePosition(Vector2.up * speed * Time.deltaTime);
        }

        SleepingNeed += sleepingNeedGrowthRate * Time.timeScale * SleepIncreaseModifier;
    }

    protected void FixedUpdate()
    {
        if ((rb.position - desiredPosition).sqrMagnitude <= 0.0001f)
            Stop();
        else
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void Stop()
    {
        direction = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(rb.position, rb.position + direction);
        Gizmos.DrawSphere(desiredPosition, 0.5f);
    }
}