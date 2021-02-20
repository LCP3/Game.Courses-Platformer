using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;
    
    bool _isEmpty = false;
    protected override bool CanUse => _isEmpty == false && _item != null;

    void Start()
    {
        if (CanUse)
        {
            _item.SetActive(false); //Turn off our item at start until we collide
        }
    }
    protected override void Use()
    {
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