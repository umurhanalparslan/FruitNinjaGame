namespace OUAPP
{
    using UnityEngine;

    // Bomb sinifi, oyuncunun bombaya temas ettiginde oyunu bitirir
    public class FN_Bomb : MonoBehaviour
    {
        // Oyuncu ile carpisma oldugunda cagrilir
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Bombanin collider'ini devre disi birak (tekrar tetiklenmesin)
                GetComponent<Collider>().enabled = false;

                // Oyunu patlat - GameManager uzerinden oyunu durdurur
                FN_GameManager.Instance.Explode();
            }
        }
    }
}
