using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Meta.Examples
{
    public class GazableButton : MonoBehaviour, IGazeStartEvent, IGazeEndEvent
    {
        public UnityEvent onGaze;
        public QualityControlVariant qualityControlVariantChoice;
        public static QualityControlVariant qualityControlVariantBeeingGazedOn;
        // Use this for initialization
        private void Awake()
        {
            if (onGaze == null)
            {
                onGaze = new UnityEvent();
            }
        }
        public void OnGazeStart()
        {
            onGaze.Invoke();
        }

        public void OnGazeEnd()
        {

        }

      public void GazeTest()
        {
            Debug.Log("Gaze test passed");
        }
    }

}