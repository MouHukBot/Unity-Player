using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TouchingDirrections : MonoBehaviour
{
    private CapsuleCollider2D touchingCollider;
    public bool IsPlatform {  get; private set; }
    private RaycastHit2D[] platformHits = new RaycastHit2D[5];
    private float platformDistance = 0.1f;
    private void Awake()
    {
        touchingCollider = GetComponent<CapsuleCollider2D>();
    }
    private void FixedUpdate()
    {
        IsPlatform = touchingCollider.Cast(Vector2.down, platformHits, platformDistance) > 0;
    }
}
