namespace OUAPP
{
    using UnityEngine;

    // Bomb sinifi, oyuncunun bombaya temas ettiginde oyunu bitirir
    public class Bomb : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GetComponent<Collider>().enabled = false;
                GameManager.Instance.Explode();
            }
        }
    }
}
