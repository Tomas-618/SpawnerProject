using UnityEngine;

public abstract class BasicController : MonoBehaviour, IMovable, IRotatable
{
    public abstract void Move();

    public abstract void Rotate();
}
