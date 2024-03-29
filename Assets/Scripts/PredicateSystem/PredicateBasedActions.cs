using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PredicateBasedActions : MonoBehaviour
{
    public UnityEvent OnAllPredicatesFulfilledEvent = new();
    public UnityEvent OnAnyPredicateFailedEvent = new();

    private List<Predicate> predicateList = new List<Predicate>();

    private void Start()
    {
        updateState();
    }

    private void OnDestroy()
    {
        predicateList.ForEach(_predicate => _predicate.OnPredicateStateUpdated -= updateState);
    }

    public void AddPredicate(Predicate _predicateToAdd)
    {
        predicateList.Add(_predicateToAdd);
        _predicateToAdd.OnPredicateStateUpdated += updateState;
        updateState();
    }
    
    private void updateState()
    {
        for (int i = 0; i < predicateList.Count; i++)
        {
            if (predicateList[i].IsFulfilled() == false)
            {
                OnAnyPredicateFailedEvent?.Invoke();
                return;
            }
        }
        
        OnAllPredicatesFulfilledEvent?.Invoke();
    }

} 
