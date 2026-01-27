using UnityEngine;

public class BG_Control : MonoBehaviour
{
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] Camera cam;

    [Header("Parallax (ใกล้ = ค่าเยอะ)")]
    [Range(0f, 1f)]
    [SerializeField] float baseParallax = 0.3f;

    [Header("Down Scroll")]
    [SerializeField] float downSpeed = 0.5f;

    private float[] parallaxFactor;
    private float[] downOffset;

    void Start()
    {
        parallaxFactor = new float[backgrounds.Length];
        downOffset = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            // ใกล้สุด (i น้อย) = factor เยอะ
            parallaxFactor[i] = Mathf.Clamp01(baseParallax + (backgrounds.Length - i - 1) * 0.1f);
            downOffset[i] = 0f;
        }
    }

    void LateUpdate()
    {
        if (GameManager.instance.isdead) return;
        float camY = cam.transform.position.y + 8f;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            // ใกล้เลื่อนมาก ไกลเลื่อนน้อย
            downOffset[i] += downSpeed * parallaxFactor[i] * Time.deltaTime;

            float y = camY - downOffset[i];

            backgrounds[i].transform.position = new Vector3(
                backgrounds[i].transform.position.x,
                y,
                backgrounds[i].transform.position.z
            );
        }
    }
}
