using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        transform.position = target.position;
    }
}
