public class BonusCard : BaseCard
{
    public BonusCard Initialize()
    {
        gameObject.name = "Bonus Card";

        return this;
    }

    protected override void OnConsume()
    {
        
    }
}