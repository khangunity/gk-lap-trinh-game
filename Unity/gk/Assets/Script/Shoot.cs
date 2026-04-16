using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Shoot : MonoBehaviour
{
    public Transform gunPoint;          // đầu nòng súng
    public float maxDistance = 100f;    // khoảng cách tối đa

    private LineRenderer line;
    private Camera cam;

    public GameObject laser;
    public GameObject laserCham;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        cam = Camera.main;

        line.positionCount = 2;
        line.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ShootFunc();

            laser.SetActive(true);
            laserCham.SetActive(true);
        }
        else
        {
            laser.SetActive(false);
            laserCham.SetActive(false);
        }
    }

    void ShootFunc()
    {
        // 1. Ray từ chuột
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPoint;

        // 2. Xác định điểm chuột chỉ tới
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            targetPoint = hit.point;
            if(hit.transform.tag == "thienthach")
            {
                Destroy(hit.transform.gameObject);
            }
        }
        else
        {
            targetPoint = ray.origin + ray.direction * maxDistance;
        }

        // 3. Ray từ súng (để không xuyên tường)
        Vector3 direction = (targetPoint - gunPoint.position).normalized;
        RaycastHit gunHit;

        if (Physics.Raycast(gunPoint.position, direction, out gunHit, maxDistance))
        {
            targetPoint = gunHit.point;
        }

        laserCham.transform.position = targetPoint;

        laser.transform.position = gunPoint.position;
        laser.transform.forward = direction;
    }
}