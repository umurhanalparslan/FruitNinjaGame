namespace OUAPP
{
    using System.Collections;
    using UnityEngine;

    // Spawner sinifi, meyve ve bomba objelerini sahneye rastgele yerlestirir
    [RequireComponent(typeof(Collider))]
    public class FN_Spawner : MonoBehaviour
    {
        private Collider spawnArea; // Obje spawn edilecek alan

        public GameObject[] fruitPrefabs; // Farkli meyve prefablari
        public GameObject bombPrefab; // Bomba prefab'i
        [Range(0f, 1f)] public float bombChance = 0.05f; // Bomba cikma ihtimali

        public float minSpawnDelay = 0.25f; // Spawn arasi minimum sure
        public float maxSpawnDelay = 1f; // Spawn arasi maksimum sure

        public float minAngle = -15f; // Obje atilma acisi minimum
        public float maxAngle = 15f; // Obje atilma acisi maksimum

        public float minForce = 18f; // Obje fırlatma gucu minimum
        public float maxForce = 22f; // Obje fırlatma gucu maksimum

        public float maxLifetime = 5f; // Obje sahnede ne kadar sure kalacak

        private void Awake()
        {
            // Spawn alani component'tan alinir
            spawnArea = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            // Obje aktif oldugunda spawn islemi baslatilir
            StartCoroutine(Spawn());
        }

        private void OnDisable()
        {
            // Obje devre disi oldugunda coroutine durdurulur
            StopAllCoroutines();
        }

        private IEnumerator Spawn()
        {
            // Baslamadan once 2 saniye bekle
            yield return new WaitForSeconds(2f);

            while (enabled)
            {
                // Rastgele bir meyve prefab'i sec
                GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

                // Bomba cikma ihtimaline gore prefab'i degistir
                if (Random.value < bombChance)
                {
                    prefab = bombPrefab;
                }

                // Spawn pozisyonunu rastgele belirle
                Vector3 position = new Vector3
                {
                    x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                    y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                    z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
                };

                // Rastgele bir donus acisi ata
                Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

                // Prefab'i instantiate et
                GameObject fruit = Instantiate(prefab, position, rotation);

                // Belirli bir sure sonra yok et
                Destroy(fruit, maxLifetime);

                // Rastgele bir kuvvet uygula
                float force = Random.Range(minForce, maxForce);
                fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

                // Yeni spawn icin bekleme suresi
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }
        }
    }
}
