using UnityEngine;

namespace QtNameSpace
{
    public class Projectile : MonoBehaviour
    {
        [Header("Preference")]
        public GameObject effect;
        public GameObject effectTwo;
        
        [Header("Config")]
        public Vector3 sizeScale;
        public Vector2 bulletVector2 = Vector2.right;
        public float speed;
        
        [Header("Debug")]
        [SerializeField]
        private int damaged;


        public void Release()
        {
            var position = transform.position;
            if (effect != null)
                Instantiate(effect, position, Quaternion.identity);
            if (effectTwo != null)
                Instantiate(effectTwo, position, Quaternion.identity);
            Destroy(gameObject);
        }

        public void SetScaleSize(Vector3 size)
        {
            transform.localScale = sizeScale + size;
        }

        public void SetDamaged(int damage)
        {
            damaged = damage;
        }

        private void Update()
        {
            transform.Translate(bulletVector2 * (speed * Time.deltaTime));
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<IHeart>() is { } heart)
            {
                heart.TakeDamage(damaged);
                Release();
            }
        }
    }
}
