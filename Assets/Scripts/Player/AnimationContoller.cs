
using UnityEngine;

public class AnimatorContoller : MonoBehaviour
{
    private Animator _animator;
    private TouchingDirrections _touchingDirrections;
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _touchingDirrections = GetComponent<TouchingDirrections>();
    }
    private void Update()
    {
        _animator.SetBool("IsMoving", _playerController.IsMoving);
        _animator.SetBool("IsRunning", _playerController.IsRunning);
        _animator.SetBool("IsPlatform", _touchingDirrections.IsPlatform);
        _animator.SetFloat("FlyVector", _playerController.FlyVector.y);
    }
    public void Flip(float x)
    {
        if ((x>0 && transform.localScale.x <0)||(x<0 && transform.localScale.x >0) )
        { 
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
