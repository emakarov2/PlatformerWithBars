public class Decreaser : ButtonChanger
{
public override void OnClick()
    {
        if(Health != null)
        {
            Health.Decrease(Amount);
        }
    }
}
