namespace OUAPP
{
    using UnityEngine;

    // Fruit sinifi, meyvenin kesilmesi ve parcacik efektlerini kontrol eder
    public class FN_Fruit : MonoBehaviour
    {
        public GameObject whole; // Butun meyve objesi
        public GameObject sliced; // Kesilmis meyve hali (2 parca gibi)

        private Rigidbody fruitRigidbody; // Ana meyvenin fizik bileeni
        private Collider fruitCollider; // Ana meyvenin carpisma kutusu
        private ParticleSystem juiceEffect; // Kesilince cikan meyve suyu efekti

        public int points = 1; // Bu meyveden kazanilacak puan

        private void Awake()
        {
            // Bilesen referanslarini aliyoruz
            fruitRigidbody = GetComponent<Rigidbody>();
            fruitCollider = GetComponent<Collider>();
            juiceEffect = GetComponentInChildren<ParticleSystem>();
        }

        // Meyve kesildiginde cagrilan fonksiyon
        private void Slice(Vector3 direction, Vector3 position, float force)
        {

            // Skoru artir
            FN_GameManager.Instance.IncreaseScore(points);

            // Carpisma kapat, butun hali gizle
            fruitCollider.enabled = false;
            whole.SetActive(false);

            // Kesilmis hali goster ve efekti calistir
            sliced.SetActive(true);
            juiceEffect.Play();

            // Kesme yonune gore kesilmis meyveyi dondur
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // Her bir dilime fiziksel kuvvet uygula
            Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody slice in slices)
            {
                slice.velocity = fruitRigidbody.velocity; // Eski hizi ver
                slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse); // Ekstra guc ver
            }
        }

        // Oyuncunun kiliciyla temas ederse kesilme islemi baslatilir
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FN_Blade blade = other.GetComponent<FN_Blade>();
                Slice(blade.direction, blade.transform.position, blade.sliceForce);
            }
        }
    }
}
