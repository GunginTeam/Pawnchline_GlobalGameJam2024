using Zenject;

public class BonusCard : BaseCard
{
    protected IScoreService _scoreService;
    
    [Inject]
    public void Construct(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }
    
    public BonusCard Initialize()
    {
        gameObject.name = "Bonus Card";

        return this;
    }

    protected override void OnConsume()
    {
        
    }
}