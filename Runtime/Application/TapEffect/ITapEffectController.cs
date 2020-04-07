
namespace Gamebase.Application.TapEffect
{
    public interface ITapEffectController
    {
        void Enable();

        void Disable();

        void Change(string name);
    }
}