using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 initPos;
    private float x, z;
    private float bulletSpeed = 100.0f;
    private float bulletRange = 50f;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        x = transform.position.x - initPos.x;
        z = transform.position.z - initPos.z;
        transform.Translate(0, 0, bulletSpeed * Time.deltaTime);
        if(Mathf.Sqrt(x*x + z*z) > bulletRange)
        {
            Destroy(this.gameObject);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            var eff = Instantiate(effect, other.transform.position, Quaternion.identity);
            Destroy(eff, 1f);
            Destroy(other.gameObject);
            GameManager.score++;
            Destroy(this.gameObject);
        }
    }
}
