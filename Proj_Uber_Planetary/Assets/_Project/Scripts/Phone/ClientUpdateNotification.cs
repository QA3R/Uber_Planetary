using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UberPlanetary.Rides;
using UnityEngine.UI;

namespace UberPlanetary.Phone
{
    public class ClientUpdateNotification : MonoBehaviour
    {
        private RideLoader _rideLoader;
        [SerializeField] private int displayTimer;
        [SerializeField] private RawImage icon;

        // Start is called before the first frame update
        void Start()
        {
            _rideLoader = GameObject.FindObjectOfType<RideLoader>();

        }

        private void OnEnable()
        {
            RideLoader.OnNotified += ToggleIcon;
        }

        private void OnDisable()
        {
            RideLoader.OnNotified -= ToggleIcon;
        }

        void ToggleIcon()
        {
            StartCoroutine("DisplayIcon");
            Debug.Log("Started Coroutine");
        }

        IEnumerator DisplayIcon()
        {
            icon.enabled = true;
            Debug.Log("Image Enabled");
            yield return new WaitForSeconds(displayTimer);
            icon.enabled = false;
            Debug.Log("Image Disabled");
        }
    }
}
