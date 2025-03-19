using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace ObjectManage.OtherObjects
{
    public class InstantFlashLight : MonoBehaviour
    {
        [SerializeField] private Light2D _light;

        [SerializeField] private float _lightIntensity;
        [SerializeField] private float _duration;


        public void Play()
        {
            StartCoroutine(LightFlashCoroutine());
        }

        private IEnumerator LightFlashCoroutine()
        {
            SetLightIntensity(_lightIntensity);
            float currentTime = 0f;
            while (currentTime < _duration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / _duration;
                SetLightIntensity(Mathf.Lerp(_lightIntensity, 0f, ratio));
                yield return null;

            }
            SetLightIntensity(0f);
        }

        private void SetLightIntensity(float value)
        {
            _light.intensity = value;
        }


    }
}