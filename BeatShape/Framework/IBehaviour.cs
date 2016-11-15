namespace BeatShape.Framework
{
    interface IBehaviour
    {
        void Update();
        void Render();
        void OnCollision(GameObject other);

    }
}
