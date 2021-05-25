using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    public Transform playerRot;
    public Camera characterCamera;
    public Rigidbody rb;
    [SerializeField]
    private float movingSpeed = 15f;
    private bool delayCheck = true;
    private bool hitDelayCheck = true;
    private Vector3 target;
    

    public Transform bulletInstant;
    public GameObject bulletPrefab;
    public AudioSource aS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //이동 관련
        transform.Translate(
            Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime,
            0, 
            Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime
        );
        //총알 발사
        if (Input.GetMouseButton(0) && delayCheck)
        {

            Instantiate(bulletPrefab, bulletInstant.position, bulletInstant.rotation);
            aS.Play();
            StartCoroutine(WaitBullet());
        }

        LookMouseCursor();

    }

    IEnumerator WaitBullet()
    {
        delayCheck = false;
        yield return new WaitForSeconds(0.5f);
        delayCheck = true;
    }
    public void LookMouseCursor()
    {
        Ray ray = characterCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if(Physics.Raycast(ray, out hitResult))
        {
            target = new Vector3(hitResult.point.x, playerRot.position.y, hitResult.point.z);
        }
        playerRot.transform.LookAt(target);
    }

    IEnumerator HitCheck()
    {
        hitDelayCheck = false;
        GameManager.Hit();
        yield return new WaitForSeconds(1);
        hitDelayCheck = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && hitDelayCheck)
        {
            StartCoroutine("HitCheck");
        }
        if (other.tag == "Wall")
        {
            GameManager.Hit();
            this.transform.position = new Vector3(0, 0, 0);
        }
    }
    
}
