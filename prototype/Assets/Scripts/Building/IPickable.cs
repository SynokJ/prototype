public interface IPickable 
{
    void OnPicked(UnityEngine.Transform snapPoint);
    bool OnDropped();
}
