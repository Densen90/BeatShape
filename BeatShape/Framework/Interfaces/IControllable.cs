namespace BeatShape.Framework
{
    interface IControllable
    {
        void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e);
        void OnKeyUp(OpenTK.Input.KeyboardKeyEventArgs e);
        void OnKeyPress(OpenTK.KeyPressEventArgs e);
    }
}
