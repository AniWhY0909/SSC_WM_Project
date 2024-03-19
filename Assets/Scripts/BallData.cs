using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class BallData : ScriptableObject
{
    public Sprite ballImage;
    public Collider2D collider2D;
    public float size;
}
