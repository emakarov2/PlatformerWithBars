using UnityEngine;

interface IMover
{
   public bool IsDirectionDefault { get; }

    public void Move(Vector2 target);
    public void Stop();
    public void Continue();    
}