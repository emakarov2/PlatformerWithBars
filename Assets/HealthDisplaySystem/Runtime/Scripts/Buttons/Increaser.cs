public class Increaser : ButtonChanger
{
    public override void OnClick()
    {
        if (Health != null)
        {
            Health.Increase(Amount);
        }
    }
}
