using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoundManage
{

    public class SoundController : MonoSingleton<SoundController>
    {
        [SerializeField] private int _defaultPoolAmount;
        private Queue<SoundPlayer> _pool = new();
        [SerializeField] private SoundPlayer _soundPlayerPrefab;


        private void Start()
        {
            for (int i = 0; i < _defaultPoolAmount; i++)
            {
                GeneratePoolObject();
            }
        }

        private void OnDestroy()
        {
            // for (int i = 0; i < _pool.Count; i++)
            // {
            //     SoundPlayer player = _pool.Dequeue();
            //     Destroy(player.gameObject);
            // }
            _pool.Clear();
        }


        public void PlaySound(SoundSO soundSO, Vector2 position)
        {
            SoundPlayer player = _pool.Count > 0
                    ? _pool.Dequeue()
                    : Instantiate(_soundPlayerPrefab, transform);
            player.transform.position = position;
            player.PlaySound(soundSO);
            player.OnSoundPlayCompleteEvent += HandleSoundPlayOver;
        }

        private void HandleSoundPlayOver(SoundPlayer player)
        {
            player.OnSoundPlayCompleteEvent -= HandleSoundPlayOver;
            _pool.Enqueue(player);
        }

        private void GeneratePoolObject()
        {
            SoundPlayer newPlayer = Instantiate(_soundPlayerPrefab, transform);
            _pool.Enqueue(newPlayer);
        }


    }
}