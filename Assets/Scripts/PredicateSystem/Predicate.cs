using System;

public abstract class Predicate
{
    public Action OnPredicateStateUpdated = null;

    public abstract bool IsFulfilled();
}
