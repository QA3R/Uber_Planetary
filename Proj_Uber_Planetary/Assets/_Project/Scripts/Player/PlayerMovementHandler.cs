using System;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    public class PlayerMovementHandler : MonoBehaviour, IMovementHandler
    {
        [SerializeField] private float movementSpeed = 80;
        
        /// <summary>
        /// Translate object forward
        /// </summary>
        /// <param name="val"></param>
        public void Move(float val)
        {
            transform.Translate(transform.forward * (val * (movementSpeed * Time.deltaTime)), Space.World);
        }
    }
}