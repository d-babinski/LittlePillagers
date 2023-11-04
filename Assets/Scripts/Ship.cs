using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ship : MonoBehaviour
{
    private enum  State
    {
        Unassigned = -1,
        MovingTowards = 0,
        Waiting = 1,
        Returning = 2,
        Sunk = 3,
    }

    public event Action<Ship> OnMissionFinished = null;
    public List<Soldier> GetSoldiers => soldiers;
    public int TemplateId = 0;
    public int SoldierCapacity = 0;
    public string ShipName = "";
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Animator Animator => animator;
    public int SoldierCount => soldiers.Count;
    public bool HasSunk => currentState == State.Sunk;

    private List<Soldier> soldiers = new List<Soldier>();

    private Mission currentMission = new Mission();
    private int returnedSoldiers = 0;
    private int deadSoldiers = 0;
    private float speed = 0f;
    private State currentState = State.Unassigned;
    private Vector3 startingPoint = Vector3.zero;
    
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Animator animator = null;
    [FormerlySerializedAs("shipNavigator")][SerializeField] private Navigator navigator = null;

    public void AssignMission(Mission _mission)
    {
        currentMission = _mission;
        currentState = State.MovingTowards;
        navigator.MoveTo(_mission.Target.ClosestDockingPoint(transform.position), speed);
        navigator.OnPointReached += onIsleReached;
        startingPoint = transform.position;
        animator.SetTrigger("Swim");
    }
    
    private void onIsleReached()
    {
        SendUnits();
        currentState = State.Waiting;
        navigator.OnPointReached -= onIsleReached;
        animator.SetTrigger("Idle");
    }

    private void Update()
    {
        if (currentState is State.MovingTowards or State.Returning)
        {
            if (navigator.NextPosition.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            
            transform.position = navigator.NextPosition;
        }
        
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void FinishMission()
    {
        if (deadSoldiers < SoldierCount)
        {
            currentMission.SucceedMission();
        }
        else
        {
            currentMission.FailMission();
        }
        
        OnMissionFinished?.Invoke(this);
    }

    public void AddSoldier(Soldier _soldier)
    {
        soldiers.Add(_soldier);
    }
    
    public void SendUnits()
    {
        Battlefield _battlefield = currentMission.Target.GetAttacked(currentMission);
        
        soldiers.ForEach(_soldier =>
        {
            _soldier.gameObject.SetActive(true);
            _soldier.OnDeath += registerSoldierDeath;
            _soldier.OnReturn += soldierReturned;
            _soldier.SetRallyPoint(transform.position);
            _soldier.transform.position = transform.position;
            _soldier.GiveOrders(currentMission.Type);
            _battlefield.AddPlayerUnit(_soldier);
        });
    }
    
    private void soldierReturned(Soldier _soldier)
    {
        _soldier.OnDeath -= registerSoldierDeath;
        _soldier.OnReturn -= soldierReturned;
        returnedSoldiers++;

        if (returnedSoldiers + deadSoldiers >= soldiers.Count)
        {
            navigator.MoveTo(startingPoint, speed);
            currentState = State.Returning;
            animator.SetTrigger("Swim");
            navigator.OnPointReached += onReturn;
        }
    }
    
    private void onReturn()
    {
        navigator.OnPointReached -= onReturn;
        FinishMission();
        Destroy(gameObject);
    }

    private void registerSoldierDeath(Soldier _soldier)
    {
        _soldier.OnDeath -= registerSoldierDeath;
        _soldier.OnReturn -= soldierReturned;
        currentMission.ReportDeadSoldier();
        deadSoldiers++;

        if (deadSoldiers >= soldiers.Count)
        {
            currentState = State.Sunk;
            animator.SetTrigger("Sink");
            FinishMission();
        }
    }

    public void SetSoldierCapacity(int _soldierCapacity)
    {
        SoldierCapacity = _soldierCapacity;
    }
    
    public void SetShipName(string _templateShipName)
    {
        ShipName = _templateShipName;
    }
    
    public void SetId(int _templateShipId)
    {
        TemplateId = _templateShipId;
    }
}
