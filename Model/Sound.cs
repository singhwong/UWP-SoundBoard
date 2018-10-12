using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newSoundBoard.Model
{
    public class Sound
    {
        public string Name { get; set; }
        public SoundCategory Category { get; set; }
        public string ImageFile { get; set; }
        public string AudioFile { get; set; }
        public Sound(string name,SoundCategory category)
        {
            Name = name;
            Category = category;
            ImageFile = string.Format("/Assets/Images/{0}/{1}.png",category,name);
            AudioFile = string.Format("/Assets/Audio/{0}/{1}.wav",category,name);
        }
    }
    public enum SoundCategory
    {
        Animals,
        Traffic,
        Nature,
        Setting
    }
}
