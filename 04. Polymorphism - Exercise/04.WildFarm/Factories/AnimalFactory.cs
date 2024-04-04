using WildFarm.Factories.Interfaces;
using WildFarm.Models.Animals;
using WildFarm.Models.Interfaces;

namespace WildFarm.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        public IAnimal CreateAnimal(string[] animalTokens)
        {
            string animalType = animalTokens[0];
            string animalName = animalTokens[1];
            double animalWeight = double.Parse(animalTokens[2]);

            switch (animalType)
            {
                case "Owl":
                    double owlWingSize = double.Parse(animalTokens[3]);
                    return new Owl(animalName, animalWeight, owlWingSize);
                case "Hen":
                    double henWingSize = double.Parse(animalTokens[3]);
                    return new Hen(animalName, animalWeight, henWingSize);
                case "Mouse":
                    string mouseLivingRegion = animalTokens[3];
                    return new Mouse(animalName, animalWeight, mouseLivingRegion);
                case "Dog":
                    string dogLivingRegion = animalTokens[3];
                    return new Dog(animalName, animalWeight, dogLivingRegion);
                case "Cat":
                    string catLivingRegion = animalTokens[3];
                    string catBreed = animalTokens[4];
                    return new Cat(animalName, animalWeight, catLivingRegion, catBreed);
                case "Tiger":
                    string tigerLivingRegion = animalTokens[3];
                    string tigerBreed = animalTokens[4];
                    return new Tiger(animalName, animalWeight, tigerLivingRegion, tigerBreed);
                default:
                    throw new System.ArgumentException("Invalid animal type!");
            }

        }
    }
}
