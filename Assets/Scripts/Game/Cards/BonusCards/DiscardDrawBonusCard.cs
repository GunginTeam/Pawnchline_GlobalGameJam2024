public class DiscardDrawBonusCard : BonusCard
{
    const string TextKey = "BONUS_CARD_TEXT_0";
    protected override string GetTextKey() => TextKey;
    protected override void OnConsume()
    {
        _scoreService.DiscardDrawPlayed();
    }
}