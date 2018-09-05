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
        public UnityEvent onGazeQCQ;
        public UnityEvent onGazeQCF; //TODO: CHANGE! <- note that this is only test implementation.
        public UnityEvent onGazeEnd;
        // public UnityEvent onGazeEndQCQ;
        // public UnityEvent onGazeEndQCF;
        bool isGazing;
        public QualityControlVariant qualityControlVariant;
        private float _lambda = 0f;
        private float _delta = 0.05f;
        private Color _colorDull = new Color(0.05f, 0.05f, 0.05f);
        private Color _colorHighlighted;
        private Material _renderMaterial;



        private void Start()
        {
            //Obtain a reference to the material assigned to this game object. 
            Renderer renderer = GetComponent<Renderer>();
            _renderMaterial = renderer.material;

            //Calculate a random highlighted color for this game object
            _colorHighlighted = Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
        }
        // Use this for initialization
        private void Awake()
        {
            if (onGaze == null)
            {
                onGaze = new UnityEvent();
            }

            if (onGazeEnd == null)
            {
                onGazeEnd = new UnityEvent();
            }
            if (onGazeQCQ == null)
            {
                onGazeQCQ = new UnityEvent();
            }
            if (onGazeQCF == null)
            {
                onGazeQCQ = new UnityEvent();
            }

        }
        public void OnGazeStart()
        {
            isGazing = true;
            switch (qualityControlVariant)
            {
                case QualityControlVariant.NONE:
                    onGaze.Invoke();
                    break;
                case QualityControlVariant.FULL:
                    onGazeQCF.Invoke();
                    break;
                case QualityControlVariant.QUICK:
                    onGazeQCQ.Invoke();
                    break;
            }
        }

        private void Update()
        {
            ChangeColor();
        }

        public void OnGazeEnd()
        {
            isGazing = false;
            onGazeEnd.Invoke();
        }

        private void ChangeColor()
        {
            //A bias for the interpolation. Positive values make highlighting occur quicker and dulling occur slower.
            float lerpBias = 0.75f;

            //Whether to incrementally increase/decrease the vibrance of the object in this frame.
            float sign = isGazing ? 1 : -1;

            //Modify lambda to incrementally increase/decrease the vibrance of the game object. 
            _lambda += (sign + lerpBias) * _delta;
            _lambda = Mathf.Clamp(_lambda, 0f, 2f); //Allow lambda beyond 1 so that the game object may glow for a little longer. 

            //Calculate the color of the object. Lambda greater than 1 are clamped to 1.
            Color color = Color.Lerp(_colorDull, _colorHighlighted, Mathf.Clamp(_lambda, 0f, 1f));
            _renderMaterial.color = color;
        }

    }

}