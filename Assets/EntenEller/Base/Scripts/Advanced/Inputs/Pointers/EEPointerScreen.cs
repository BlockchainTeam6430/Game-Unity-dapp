namespace Plugins.EntenEller.Base.Scripts.Advanced.Inputs.Pointers
{
    public class EEPointerScreen : EEPointer
    {
        protected override void EEAwake()
        {
            base.EEAwake();
            IsFocused = true;
        }
    }
}
