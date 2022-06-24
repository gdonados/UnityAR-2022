using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtY : MonoBehaviour
{
    public Transform target;
    public Vector3 NoY;
    public bool reverse = false;

    private void Start()
    {

    }
    void Update()
    {
        if (target)
        {
            if (!reverse)
            {
                NoY = new Vector3(target.position.x, transform.position.y, target.position.z);
                transform.LookAt(NoY);
            }
            else
            {
                Vector3 to = (target.position - this.transform.position).normalized;
                Vector3 flip = this.transform.position - to;
                transform.LookAt(flip);
            }

        }

    }
}
