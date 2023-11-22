using UnityEngine;

namespace Game.Scripts.Common
{
    public class AudioSourceController : Singleton<AudioSourceController>
    {
        [SerializeField] private AudioSource sourcePrefab;


        public void EmitAudio(AudioClip clip)
        {
            var source = Instantiate(sourcePrefab, transform).GetComponent<AudioSource>();
            source.clip = clip;
            source.Play();
        }
    }
}