using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement.MovementHandlers
{
    public class BaseMovementHandler : MonoBehaviour, IMovementHandler
    {
        [SerializeField] protected float forwardMovementSpeed = 80;
        [SerializeField] protected float backwardMovementSpeed = 80;
        [SerializeField] protected float sideMovementSpeed = 80;
        [SerializeField] protected float verticalMovementSpeed = 80;
        [SerializeField] protected float passiveMovementSpeed = 20;
        
        public virtual void MoveForward(float val)
        {
            Move(transform.forward, val, forwardMovementSpeed);
        }
        public virtual void MoveBackward(float val)
        {
            Move(transform.forward, val, backwardMovementSpeed);
        }
        public virtual void MoveSidewards(float val)
        {
            Move(transform.right, val, sideMovementSpeed);
        }

        public virtual void MoveVertical(float val)
        {
            Move(transform.up, val, verticalMovementSpeed);
        }

        protected virtual void Update()
        {
            Move(transform.forward,1, passiveMovementSpeed);
        }

        /// Translate Object in direction with speed
        protected void Move(Vector3 dir, float val, float speedMultiplier)
        {
            transform.Translate(dir * (val * (speedMultiplier * Time.deltaTime)), Space.World);
        }
    }
}