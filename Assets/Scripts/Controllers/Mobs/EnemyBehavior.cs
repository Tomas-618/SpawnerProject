using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip[] _skeletonDeathSounds;

    [SerializeField] private float _delay;

    private Animator _skeletonAnimator;
    private AudioSource _skeletonVoice;

    private void OnValidate()
    {
        int minDelay = 1;

        if (_delay < minDelay)
            _delay = minDelay;
    }

    private void Awake()
    {
        _skeletonAnimator = GetComponent<Animator>();
        _skeletonVoice = GetComponent<AudioSource>();
    }

    private void Start()
    {
        int walkingSoundsDelay = 1;

        _skeletonVoice.PlayDelayed(walkingSoundsDelay);
        StartCoroutine(WaitBeforeDeath(_delay));
    }

    private IEnumerator WaitBeforeDeath(float delay)
    {
        int deleteAnimatorDelay = 5;

        yield return new WaitForSeconds(delay);

        _skeletonVoice.Stop();
        _skeletonAnimator.SetTrigger(SkeletonAnimatorData.Params.Death);
        _skeletonVoice.PlayOneShot(_skeletonDeathSounds[Random.Range(0, _skeletonDeathSounds.Length)]);
        
        Destroy(this);
        Destroy(_skeletonAnimator, deleteAnimatorDelay);
    }
}
