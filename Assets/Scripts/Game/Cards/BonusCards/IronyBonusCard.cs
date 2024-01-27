public class IronyBonusCard : BonusCard
{
    const string TextKey = "BONUS_CARD_TEXT_3";
    protected override string GetTextKey() => TextKey;
    protected override void OnConsume()
    {
        _scoreService.SetIrony();
    }
}