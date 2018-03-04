using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutDestroy : MonoBehaviour {

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
        if (collision.transform.name.Contains("Player") && !collision.transform.name.Contains(transform.name.Substring(8, transform.name.Length - 8)))
        {
            print(collision.transform.name.Substring(7, collision.transform.name.Length - 7));
            collision.transform.GetComponent<Hp>().CmdDamage(50, collision.transform.name, transform.name.Substring(8, transform.name.Length - 8));
        }
    }
}
