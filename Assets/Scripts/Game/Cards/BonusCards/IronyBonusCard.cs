public class IronyBonusCard : BonusCard
{
    protected override void OnConsume()
    {
        _scoreService.SetIrony();
    }
}