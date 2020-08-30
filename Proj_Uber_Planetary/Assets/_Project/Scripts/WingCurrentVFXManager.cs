using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace UberPlanetary
{
    public class WingCurrentVFXManager : MonoBehaviour
    {
        [SerializeField] private Transform[] wingContactPoints;
        [SerializeField] private Transform[] bodyContactPoints;
        [SerializeField] private VisualEffect[] vfxArray;
        
        [SerializeField] private string leftPropertyName;
        [SerializeField] private string rightPropertyName;

        private void Start()
        {
            StartCoroutine(UpdateVFX());
        }

        private IEnumerator UpdateVFX()
        {
            while (true)
            {
                for (int i = 0; i < vfxArray.Length; i++)
                {
                    vfxArray[i].SetVector3(leftPropertyName, wingContactPoints[i].transform.position);
                    vfxArray[i].SetVector3(rightPropertyName, bodyContactPoints[i].transform.position);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
