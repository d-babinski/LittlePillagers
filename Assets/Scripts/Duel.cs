using UnityEngine;

public class Duel
    {
        public Soldier Attacker = null;
        public Soldier Defender = null;

        public bool HasBegun => currentState != State.Initial;
        public bool AreSoldiersMoving => currentState == State.Moving;
        
        private Vector3 attackerDestination = Vector3.zero;
        private Vector3 defenderDestination = Vector3.zero;
        
        private enum State
        {
            Initial = 0,
            Moving = 1,
            Fighting = 2,
            Resolved = 3,
        }

        private State currentState = State.Initial;
        
        public Duel(Soldier _attacker, Soldier _defender)
        {
            Attacker = _attacker;
            Defender = _defender;
        }

        public void MeetInTheMiddle()
        {
            Vector3 _vectorBetweenSoldiers = Defender.transform.position - Attacker.transform.position;
            Vector3 _poinInTheMiddle = Attacker.transform.position + _vectorBetweenSoldiers.magnitude/2f*_vectorBetweenSoldiers.normalized;
            attackerDestination = _poinInTheMiddle - _vectorBetweenSoldiers.normalized*1f/32f;
            defenderDestination = _poinInTheMiddle + _vectorBetweenSoldiers.normalized*1f/32f;
            
            Attacker.MoveTo(attackerDestination);
            Defender.MoveTo(defenderDestination);

            currentState = State.Moving;
        }

        public bool HaveReachedPositions()
        {
            float _attackerDistanceToDestination = Vector2.Distance(attackerDestination, Attacker.transform.position);
            float _defenderDistanceToDestination = Vector2.Distance(defenderDestination, Defender.transform.position);

            return _attackerDistanceToDestination < Mathf.Epsilon && _defenderDistanceToDestination < Mathf.Epsilon;
        }
        
        public void PlayAttacks()
        {
            Attacker.MakeAttack();
            Defender.MakeAttack();
            currentState = State.Fighting;
        }

        public void ResolveDuel()
        {
            Attacker.TakeDamage(Defender.Attack);
            Defender.TakeDamage(Attacker.Attack);

            if (Defender.AttackLeft <= 0)
            {
                Defender.Die();
            }
            else
            {
                Defender.Wait();
            }

            if (Attacker.AttackLeft <= 0)
            {
                Attacker.Die();
            }
            else
            {
                Attacker.Wait();
            }

            currentState = State.Resolved;
        }
    }