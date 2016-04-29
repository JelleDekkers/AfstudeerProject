using UnityEngine;

public class Arrow : MonoBehaviour {

    [SerializeField] private float speed = 20;
    [SerializeField] private float playerDamage = 10;
    [SerializeField] private float aiDamage = 10;
    [SerializeField] private LayerMask limbLayerMask;

    private bool move;
    private GameObject shooter;
    private Vector3 fwd;
    private float destroyTime = 20f;
    private Rigidbody rBody;
    
    public void Init(Transform shooter, float damage) {
        playerDamage = damage;
        aiDamage = damage;
        this.shooter = shooter.gameObject;
        transform.parent = shooter;
        rBody = GetComponent<Rigidbody>();
        rBody.useGravity = false;
        rBody.constraints = RigidbodyConstraints.FreezeAll;
        rBody.isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public void Fire() {
        move = true;
        transform.parent = null;
        rBody.useGravity = true;
        rBody.constraints = RigidbodyConstraints.None;
        rBody.isKinematic = false;
        fwd = shooter.transform.TransformDirection(Vector3.forward);
        GetComponent<Collider>().enabled = true;
        //TODO: add a small deviation in forward direction
        Destroy(gameObject, destroyTime);
    }

    private void Update() {
        if(move) {
            transform.position += fwd * speed * Time.deltaTime;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject == shooter)
            return; 

        if(col.gameObject.GetComponent<Actor>()) {
            Actor actor = col.gameObject.GetComponent<Actor>();
            if (actor.GetType() == typeof(Player))
                actor.TakeDamage(shooter, playerDamage);
            else
                actor.TakeDamage(shooter, aiDamage);
            //TODO: check for limb and parent arrow to it
        }

        move = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.parent = col.gameObject.transform;
        Destroy(GetComponent<Collider>());
    }
}
