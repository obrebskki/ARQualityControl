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
        public UnityEvent onGazeEnd;

        public QualityControlVariant qualityControlVariantChoice;
        public static QualityControlVariant qualityControlVariantBeeingGazedOn;
        // Use this for initialization
        private void Awake()
        {
            if (onGaze == null)
            {
                onGaze = new UnityEvent();
            }

            if (onGaze == null)
            {
                onGazeEnd = new UnityEvent();
            }
        }
        public void OnGazeStart()
        {
            onGaze.Invoke();
        }

        public void OnGazeEnd()
        {
            onGazeEnd.Invoke();

        }


    }

}