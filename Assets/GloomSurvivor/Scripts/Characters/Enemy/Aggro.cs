using System;
using System.Collections;
using UnityEngine;

namespace GloomSurvivor.Scripts.Characters.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveToPlayer _followPlayer;

        private float _cooldown = 3f;
        private Coroutine _cooldownCoroutine;
        private bool _hasHeroInZone;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
            
            SetFollowPlayerEnabled(false);
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasHeroInZone)
            {
                _hasHeroInZone = true;
                StopCooldownCoroutine();
                SetFollowPlayerEnabled(true);   
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasHeroInZone)
            {
                _hasHeroInZone = false;
                _cooldownCoroutine = StartCoroutine(SwitchFollowPlayerAfterCooldown());
            }
        }

        private void StopCooldownCoroutine()
        {
            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
                _cooldownCoroutine = null;                
            }
        }

        private IEnumerator SwitchFollowPlayerAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SetFollowPlayerEnabled(false);
        }

        private void SetFollowPlayerEnabled(bool enabled) => 
            _followPlayer.enabled = enabled;
    }
}