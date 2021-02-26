using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;
    
    bool _isEmpty = false;
    protected override bool CanUse => _isEmpty == false;

    void Start()
    {
        if (CanUse)
        {
            _item.SetActive(false); //Turn off our item at start until we collide
        }
    }
    protected override void Use()
    {
        Debug.Log("Used itembox");
        _item = Instantiate(
            _itemPrefab, // GameObject
            transform.position + Vector3.up, // Position + 1
            Quaternion.identity, // == Default rotation.
            transform); // Parent GameObject that this is attached to.

        if (_item == null)
            return;

        base.Use();

        _isEmpty = true;
        _item.SetActive(true);
        var itemRigidBody = _item.GetComponent<Rigidbody2D>();
        if (itemRigidBody != null)
        {
            itemRigidBody.velocity = _itemLaunchVelocity;
        }        
    }
}