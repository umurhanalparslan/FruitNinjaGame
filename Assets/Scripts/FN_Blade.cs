namespace OUAPP
{
    using UnityEngine;

    // Blade sinifi, oyuncunun fare hareketi ile meyveleri kesmesini saglar
    public class FN_Blade : MonoBehaviour
    {
        public float sliceForce = 5f; // Kesme gucu
        public float minSliceVelocity = 0.01f; // Minimum kesme hizi

        private Camera mainCamera; // Ana kamera referansi
        private Collider sliceCollider; // Kesme collider'i
        private TrailRenderer sliceTrail; // Kilicin biraktigi iz

        public Vector3 direction { get; private set; } // Kesme yonu
        public bool slicing { get; private set; } // Su anda kesiyor mu?

        private void Awake()
        {
            mainCamera = Camera.main;
            sliceCollider = GetComponent<Collider>();
            sliceTrail = GetComponentInChildren<TrailRenderer>();
        }

        private void OnEnable()
        {
            StopSlice();
        }

        private void OnDisable()
        {
            StopSlice();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartSlice();
                FN_AudioManager.instance.Play("CutSound"); // Kılıcı her salladığında ses çalsın
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopSlice();
            }
            else if (slicing)
            {
                ContinueSlice();
            }
        }

        // Kesme baslatildiginda cagrilir
        private void StartSlice()
        {
            Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0f;
            transform.position = position;

            slicing = true;
            sliceCollider.enabled = true;
            sliceTrail.enabled = true;
            sliceTrail.Clear();
        }

        // Kesme durduruldugunda cagrilir
        private void StopSlice()
        {
            slicing = false;
            sliceCollider.enabled = false;
            sliceTrail.enabled = false;
        }

        // Kesme devam ederken pozisyon guncellenir
        private void ContinueSlice()
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0f;
            direction = newPosition - transform.position;

            float velocity = direction.magnitude / Time.deltaTime;
            sliceCollider.enabled = velocity > minSliceVelocity;

            transform.position = newPosition;
        }
    }
}
