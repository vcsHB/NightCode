using UnityEngine;
using UnityEngine.Rendering;
namespace Core.VolumeControlSystem
{

    public abstract class VolumeController : MonoBehaviour
    {

        public abstract void Initialize(Volume globalVolume);

    }
}