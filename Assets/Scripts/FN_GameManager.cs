namespace OUAPP
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    // GameManager sinifi, oyunun genel durumunu ve skoru kontrol eder
    [DefaultExecutionOrder(-1)]
    public class FN_GameManager : MonoBehaviour
    {
        public static FN_GameManager Instance { get; private set; } // Singleton ornegi

        [SerializeField] private FN_Blade blade; // Kesme kontrolu yapan sinif
        [SerializeField] private FN_Spawner spawner; // Obje spawn islemleri
        [SerializeField] private Text scoreText; // Skor gosterimi icin UI
        [SerializeField] private Image fadeImage; // Patlama fade efekti

        public int score { get; private set; } = 0; // Mevcut skor

        private void Awake()
        {
            // Singleton yapisi: baska bir kopya varsa yok et
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnDestroy()
        {
            // Sahne degisirse singleton referansini temizle
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            // Oyun basladiginda yeni oyun baslat
            NewGame();
        }

        // Oyunu sifirlayip yeniden baslatan metod
        private void NewGame()
        {
            Time.timeScale = 1f; // Zaman akisini sifirla
            ClearScene(); // Eski meyve/bomba temizle

            blade.enabled = true; // Kesme aktif
            spawner.enabled = true; // Obje spawn aktif

            score = 0; // Skoru sifirla
            scoreText.text = score.ToString(); // UI guncelle
        }

        // Sahnedeki tum meyve ve bombalari yok et
        private void ClearScene()
        {
            FN_Fruit[] fruits = FindObjectsOfType<FN_Fruit>();
            foreach (FN_Fruit fruit in fruits)
            {
                Destroy(fruit.gameObject);
            }

            FN_Bomb[] bombs = FindObjectsOfType<FN_Bomb>();
            foreach (FN_Bomb bomb in bombs)
            {
                Destroy(bomb.gameObject);
            }
        }

        // Puan artirma fonksiyonu
        public void IncreaseScore(int points)
        {
            score += points; // Yeni skoru ekle
            scoreText.text = score.ToString(); // UI guncelle

            float hiscore = PlayerPrefs.GetFloat("hiscore", 0); // Kayitli en yuksek skor
            if (score > hiscore)
            {
                hiscore = score;
                PlayerPrefs.SetFloat("hiscore", hiscore); // Yeni en yuksek skoru kaydet
            }
        }

        // Oyuncu bomba ile carpistiginda patlama baslatilir
        public void Explode()
        {
            blade.enabled = false; // Kesme durdurulur
            spawner.enabled = false; // Spawn durdurulur

            StartCoroutine(ExplodeSequence()); // Patlama animasyonu baslat
        }

        // Patlama animasyonunu ve yeniden baslamayi kontrol eden coroutine
        private IEnumerator ExplodeSequence()
        {
            float elapsed = 0f;
            float duration = 0.5f;

            // Beyaza fade gecisi ve zamanin yavaslamasi
            while (elapsed < duration)
            {
                float t = Mathf.Clamp01(elapsed / duration);
                fadeImage.color = Color.Lerp(Color.clear, Color.white, t);
                Time.timeScale = 1f - t;
                elapsed += Time.unscaledDeltaTime;

                yield return null;
            }

            // 1 saniye bekle
            yield return new WaitForSecondsRealtime(1f);

            NewGame(); // Oyun sifirlanir

            elapsed = 0f;

            // Fade geri donusu
            while (elapsed < duration)
            {
                float t = Mathf.Clamp01(elapsed / duration);
                fadeImage.color = Color.Lerp(Color.white, Color.clear, t);
                elapsed += Time.unscaledDeltaTime;

                yield return null;
            }
        }
    }
}
