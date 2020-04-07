using Gamebase.Data.Entity;
using Gamebase.Domain.Model;

namespace Gamebase.Data.Translator
{
    public static class SoundConfigTranslator
    {
        public static SoundConfigModel Translate(SoundConfigEntity entity)
        {
            return new SoundConfigModel()
            {
                Volume = entity.Volume,
            };
        }

        public static SoundConfigEntity Translate(SoundConfigModel model)
        {
            return new SoundConfigEntity()
            {
                Volume = model.Volume,
            };
        }
    }
}