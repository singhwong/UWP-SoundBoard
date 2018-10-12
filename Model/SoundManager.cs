using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newSoundBoard.Model
{
    class SoundManager
    {
        public static void GetSound(ObservableCollection<Sound> getsound)
        {
            var value = SetSound();
            getsound.Clear();
            value.ForEach(p=>getsound.Add(p));
        }
        public static void GetCategorySound(ObservableCollection<Sound> getsound,SoundCategory category)
        {
            var value = SetSound();
            getsound.Clear();
            var category_value = value.Where(p=>p.Category==category).ToList();
            category_value.ForEach(p=>getsound.Add(p));
        }
        public static void GetSoundByName(ObservableCollection<Sound> getsound,string name)
        {
            var value = SetSound();
            getsound.Clear();
            var category_value = value.Where(p => p.Name == name).ToList();
            category_value.ForEach(p=>getsound.Add(p));
        }
        public static List<Sound> SetSound()
        {
            var setsound = new List<Sound>();
            setsound.Add(new Sound("Cat",SoundCategory.Animals));
            setsound.Add(new Sound("Cow", SoundCategory.Animals));
            setsound.Add(new Sound("Tiger", SoundCategory.Animals));
            setsound.Add(new Sound("Dog", SoundCategory.Animals));
            setsound.Add(new Sound("Bear", SoundCategory.Animals));
            setsound.Add(new Sound("Mouse", SoundCategory.Animals));
            setsound.Add(new Sound("Sheep", SoundCategory.Animals));
            setsound.Add(new Sound("Camel", SoundCategory.Animals));
            setsound.Add(new Sound("Eagle", SoundCategory.Animals));
            setsound.Add(new Sound("Panda", SoundCategory.Animals));
            setsound.Add(new Sound("Dragon", SoundCategory.Animals));
            setsound.Add(new Sound("Deer", SoundCategory.Animals));
            setsound.Add(new Sound("Crocodile", SoundCategory.Animals));
            setsound.Add(new Sound("Lion", SoundCategory.Animals));
            setsound.Add(new Sound("Duck", SoundCategory.Animals));
            setsound.Add(new Sound("Hippo", SoundCategory.Animals));
            setsound.Add(new Sound("Elephant", SoundCategory.Animals));
            setsound.Add(new Sound("Koala", SoundCategory.Animals));
            setsound.Add(new Sound("Horse", SoundCategory.Animals));
            setsound.Add(new Sound("Wolf", SoundCategory.Animals));
            setsound.Add(new Sound("Chicken", SoundCategory.Animals));
            setsound.Add(new Sound("Monkey", SoundCategory.Animals));
            setsound.Add(new Sound("Pig", SoundCategory.Animals));
            setsound.Add(new Sound("Rabbit", SoundCategory.Animals));
            setsound.Add(new Sound("MotorCycle", SoundCategory.Animals));
            setsound.Add(new Sound("Moose", SoundCategory.Animals));
            setsound.Add(new Sound("Penguin", SoundCategory.Animals));
            setsound.Add(new Sound("Squirrel", SoundCategory.Animals));
            setsound.Add(new Sound("Rattlesnake", SoundCategory.Animals));
            setsound.Add(new Sound("Seal", SoundCategory.Animals));
            setsound.Add(new Sound("Fish", SoundCategory.Animals));



            setsound.Add(new Sound("Plane", SoundCategory.Traffic));
            setsound.Add(new Sound("Train", SoundCategory.Traffic));
            setsound.Add(new Sound("Ship", SoundCategory.Traffic));
            setsound.Add(new Sound("Police", SoundCategory.Traffic));
            setsound.Add(new Sound("Ambulance", SoundCategory.Traffic));
            setsound.Add(new Sound("Helicopter", SoundCategory.Traffic));
            setsound.Add(new Sound("Bicycle", SoundCategory.Traffic));
            setsound.Add(new Sound("SpaceShip", SoundCategory.Traffic));
            //setsound.Add(new Sound("Bullet train", SoundCategory.Traffic));


            setsound.Add(new Sound("Rain", SoundCategory.Nature));
            setsound.Add(new Sound("Lightning", SoundCategory.Nature));
            setsound.Add(new Sound("Fire", SoundCategory.Nature));
            setsound.Add(new Sound("Blizzard", SoundCategory.Nature));
            setsound.Add(new Sound("Ice", SoundCategory.Nature));
            setsound.Add(new Sound("Raindrop", SoundCategory.Nature));
            
            return setsound;
        }
    }
}
