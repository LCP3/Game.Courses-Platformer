using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] int _totalCoins = 3; //Adjustable coin count in box

    int _remainingCoins;

    protected override bool CanUse => _remainingCoins > 0; //Return bool to CanUse

    private void Start()
    {
        _remainingCoins = _totalCoins;
    }

    protected override void Use()
    {
        base.Use(); //Run the code in the base class (HittableFromBelow)
        _remainingCoins--;        
        Coin.CoinsCollected++;
        Debug.Log("Added coin");
    }
}
