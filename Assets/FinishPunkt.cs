using UnityEngine;

public class FinishPunkt : MonoBehaviour
{
    [SerializeField] private GameObject Friedolin;
    private void onTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            
        }
    }
}
