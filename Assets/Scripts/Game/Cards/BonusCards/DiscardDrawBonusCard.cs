public class DiscardDrawBonusCard : BonusCard
{
    protected override void OnConsume()
    {
        _scoreService.DiscardDrawPlayed();
    }
}