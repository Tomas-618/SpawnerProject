using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SpawnerBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent _spawned;

    [SerializeField] private EnemyBehavior _enemy;

    [SerializeField] private int _spawnCount;
    [SerializeField] private float _delay;

    private Point[] _points;

    private void OnValidate()
    {
        int minDelay = 1;

        if (_spawnCount < 0)
            _spawnCount = 0;

        if (_delay < minDelay)
            _delay = minDelay;
    }

    private void Awake() =>
        _points = GetComponentsInChildren<Point>();

    private void Start() =>
        StartCoroutine(CreateEnemy(_delay));

    private IEnumerator CreateEnemy(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        int maxAngle = 360;
        
        for (int i = 0; i < _spawnCount; i++)
        {
            yield return wait;

            Vector3 position = _points[Random.Range(0, _points.Length)].transform.position;
            Vector3 direction = Vector3.up * Random.Range(0, maxAngle);

            EnemyBehavior enemy = Instantiate(_enemy, position, Quaternion.identity);

            enemy.transform.rotation = Quaternion.Euler(direction);
            _spawned.Invoke();
        }
    }
}
