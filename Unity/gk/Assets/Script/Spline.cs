using UnityEngine;

public class Spline : MonoBehaviour
{
    [Header("=== ĐIỂM ĐƯỜNG CONG ===")]
    public Transform[] points; // Danh sách các điểm tạo đường cong (ít nhất 4 điểm)

    [Header("=== DI CHUYỂN ===")]
    public float speed = 0.5f; // tốc độ bay dọc theo đường
    private float t = 0f;      // vị trí tổng (0 -> 1 trên toàn spline)

    [Header("=== LỆCH TRÁI PHẢI ===")]
    public float maxOffset = 2f;   // lệch tối đa sang trái/phải
    public float offsetSpeed = 5f; // tốc độ lệch
    private float offset = 0f;     // độ lệch hiện tại

    void Update()
    {
        // ===== KIỂM TRA =====
        if (points.Length < 4) return; // cần ít nhất 4 điểm để tạo spline

        // Số đoạn cong (mỗi đoạn cần 4 điểm)
        int numSections = points.Length - 3;

        // ===== 1. DI CHUYỂN TRÊN SPLINE =====
        t += speed * Time.deltaTime;

        // dừng
        if(t >= 1) return;


        // Chuyển t tổng thành t theo từng đoạn
        float totalT = t * numSections;
        

        // Xác định đang ở đoạn thứ mấy
        int currentSection = Mathf.FloorToInt(totalT);

        // Giới hạn để không bị lỗi index
        currentSection = Mathf.Clamp(currentSection, 0, numSections - 1);

        // t cục bộ trong đoạn (0 → 1)
        float localT = totalT - currentSection;

        // ===== 2. LẤY 4 ĐIỂM CỦA ĐOẠN HIỆN TẠI =====
        Vector3 p0 = points[currentSection].position;
        Vector3 p1 = points[currentSection + 1].position;
        Vector3 p2 = points[currentSection + 2].position;
        Vector3 p3 = points[currentSection + 3].position;

        // ===== 3. TÍNH VỊ TRÍ TRÊN ĐƯỜNG CONG =====
        Vector3 pos = GetCatmullRom(p0, p1, p2, p3, localT);

        // ===== 4. TÍNH HƯỚNG BAY =====
        Vector3 forward = GetTangent(p0, p1, p2, p3, localT).normalized;

        // ===== 5. NHẬP INPUT TRÁI PHẢI =====
        float input = Input.GetAxis("Horizontal"); // A/D hoặc ← →

        // thay đổi độ lệch
        offset += input * offsetSpeed * Time.deltaTime;

        // giới hạn không cho bay quá xa
        offset = Mathf.Clamp(offset, -maxOffset, maxOffset);

        // tính vector sang phải (vuông góc với hướng bay)
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

        // ===== 6. ÁP DỤNG VỊ TRÍ CUỐI =====
        transform.position = pos + right * offset;

        // ===== 7. XOAY THEO HƯỚNG BAY =====
        transform.rotation = Quaternion.LookRotation(forward);
    }

    // ===== HÀM TÍNH VỊ TRÍ TRÊN SPLINE =====
    Vector3 GetCatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Công thức Catmull-Rom spline
        return 0.5f * (
            (2 * p1) +                         // điểm chính
            (-p0 + p2) * t +                   // hướng ban đầu
            (2*p0 - 5*p1 + 4*p2 - p3) * t*t +  // độ cong
            (-p0 + 3*p1 - 3*p2 + p3) * t*t*t   // điều chỉnh mượt
        );
    }

    // ===== HÀM TÍNH HƯỚNG (TIẾP TUYẾN) =====
    Vector3 GetTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // đạo hàm của Catmull-Rom (để lấy hướng)
        return 0.5f * (
            (-p0 + p2) +
            2*(2*p0 - 5*p1 + 4*p2 - p3) * t +
            3*(-p0 + 3*p1 - 3*p2 + p3) * t*t
        );
    }
}
