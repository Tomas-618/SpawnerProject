using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip[] _skeletonDeathSounds;

    [SerializeField] private float _delay;

    private Animator _skeletonAnimator;
    private AudioSource _skeletonAudio;

    private void OnValidate()
    {
        int minDelay = 1;

        if (_delay < minDelay)
            _delay = minDelay;
    }

    private void Awake()
    {
        _skeletonAnimator = GetComponent<Animator>();
        _skeletonAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        int walkingSoundsDelay = 1;

        _skeletonAudio.PlayDelayed(walkingSoundsDelay);
        StartCoroutine(WaitBeforeDeath(_delay));
    }

    private IEnumerator WaitBeforeDeath(float delay)
    {
        int deleteAnimatorDelay = 5;

        yield return new WaitForSeconds(delay);

        _skeletonAudio.Stop();
        _skeletonAnimator.SetTrigger(SkeletonAnimatorData.Params.Death);
        _skeletonAudio.PlayOneShot(_skeletonDeathSounds[Random.Range(0, _skeletonDeathSounds.Length)]);
        
        Destroy(this);
        Destroy(_skeletonAnimator, deleteAnimatorDelay);
    }
}
