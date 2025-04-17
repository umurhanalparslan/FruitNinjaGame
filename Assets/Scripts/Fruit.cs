namespace OUAPP
{
    using UnityEngine;

    // Fruit sinifi, meyvenin kesilmesi ve parcacik efektlerini kontrol eder
    public class Fruit : MonoBehaviour
    {
        public GameObject whole; // Butun meyve
        public GameObject sliced; // Kesilmis meyve hali

        private Rigidbody fruitRigidbody; // Rigidbody referansi
        private Collider fruitCollider; // Collider referansi
        private ParticleSystem juiceEffect; // Kesme efekti

        public int points = 1; // Bu meyveden kazanilacak puan

        private void Awake()
        {
            fruitRigidbody = GetComponent<Rigidbody>();
            fruitCollider = GetComponent<Collider>();
            juiceEffect = GetComponentInChildren<ParticleSystem>();
        }

        private void Slice(Vector3 direction, Vector3 position, float force)
        {
            GameManager.Instance.IncreaseScore(points);

            fruitCollider.enabled = false;
            whole.SetActive(false);

            sliced.SetActive(true);
            juiceEffect.Play();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody slice in slices)
            {
                slice.velocity = fruitRigidbody.velocity;
                slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Blade blade = other.GetComponent<Blade>();
                Slice(blade.direction, blade.transform.position, blade.sliceForce);
            }
        }
    }
}
