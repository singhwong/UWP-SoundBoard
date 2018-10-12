using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newSoundBoard.Model
{
    public class Icon
    {
        public string IconFile { get; set; }
        public SoundCategory Category { get; set; }
    }
    public class SetIcon
    {
        public static List<Icon> GetIcon()
        {
            var value = new List<Icon>();
            value.Add(new Icon { IconFile = "Assets/Icons/animals.png",Category=SoundCategory.Animals});
            value.Add(new Icon { IconFile = "Assets/Icons/traffic.png",Category=SoundCategory.Traffic });
            value.Add(new Icon { IconFile = "Assets/Icons/nature.png",Category=SoundCategory.Nature });
            //value.Add(new Icon { IconFile = "Assets/Icons/setting.png", Category = SoundCategory.Setting });
            return value;
        }
    }
}
